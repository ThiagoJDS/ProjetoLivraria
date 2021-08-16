import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Classificacao } from '../../models/Classificacao';
import { ClassificacaoService } from '../../services/classificacao.service';

@Component({
  selector: 'app-Classificacao',
  templateUrl: './Classificacao.component.html',
  styleUrls: ['./Classificacao.component.css']
})
export class ClassificacaoComponent implements OnInit {

  public titulo = 'Classificações';
  public classificacaoSelecionado: Classificacao = new Classificacao();
  public modalRef: BsModalRef = new BsModalRef();
  public classificacoes: Classificacao[] = [];
  pag: number = 1;
  contador: number = 5;
  classificacoesFiltrados: Classificacao[] = [];
  classificacao: Classificacao | any;
  registerForm: FormGroup | any;
  modoSalvar = 'post';
  bodyDeletarClassificacao = '';
  bodyDeletarMensagem = '';
  bodyDeletarCodigo = '';
  fileNameToUpdate: string = '';
  _filtroLista: string = '';

  constructor(private fb: FormBuilder,
    private modalService: BsModalService,
    private classificacaoService: ClassificacaoService,
    private toastr: ToastrService) { }

  ngOnInit() : void {
      this.carregarClassificacoes();
    }

  get f(): any {
      return this.classificacaoForm.controls;
    }

  public cssValidator(campoForm: FormControl | AbstractControl): any {
      return { 'is-invalid': campoForm.errors && campoForm.touched };
    }

  get filtroLista(): string{
      return this._filtroLista;
    }
  set filtroLista(value: string){
      this._filtroLista = value;
      this.classificacoesFiltrados = this.filtroLista ? this.filtrarClassificacoes(this.filtroLista) : this.classificacoes;
    }

  filtrarClassificacoes(filtrarPor: string): Classificacao[]{
      filtrarPor = filtrarPor.toLocaleLowerCase();
      return this.classificacoes.filter(
        classificacao => classificacao.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== -1
      )
    }

  openModal(template: TemplateRef<any>) {
      this.modalRef = this.modalService.show(template);
    }

  createForm() {
      this.classificacaoForm = this.fb.group({
        id: [''],
        descricao: ['', Validators.required],
        anos: ['', Validators.required]
      });
    }

  classificacaoForm = new FormGroup({
    id: new FormControl(''),
    descricao: new FormControl(''),
    anos: new FormControl('')
  });

  carregarClassificacoes(){
    this.classificacaoService.obterTodos().subscribe(
      (resultado: Classificacao[]) => {
        this.classificacoes = resultado;
        this.classificacoesFiltrados = this.classificacoes;
        console.log(this.classificacoes);
      },
      error => { this.toastr.error(`Erro ao tentar Carregar classificações: ${error}`); }
    );
  }

  classificacaoSelect(classificacao: Classificacao, template: any){
    this.classificacaoSelecionado = classificacao;
    this.classificacaoForm.patchValue(classificacao);
    this.modoSalvar = 'put';
    this.openModal(template);
  }

  voltar(){
    this.classificacaoSelecionado = new Classificacao();
  }

  novoClassificacao(template: any){
    this.classificacaoSelecionado = new Classificacao();
    this.classificacaoSelecionado.id = -1;
    this.classificacaoForm.patchValue(this.classificacaoSelecionado);
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  classificacaoSubmit(){
    if(this.classificacaoSelecionado.id === -1){
      this.salvarClassificacao(this.classificacaoForm.value);
      this.modalRef.hide();
    }
    else{
      this.editarClassificacao(this.classificacaoForm.value);
      this.modalRef.hide();
    }

  }

  salvarClassificacao(classificacao: Classificacao){
    classificacao.id = 0;
    this.classificacaoService.salvar(classificacao).subscribe(
      (resultado: any) => {
        console.log(resultado);
        this.classificacaoSelecionado = resultado;
        this.carregarClassificacoes();
        this.toastr.success('Inserido com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Inserir: ${erro}`);
      }
    );
  }

  editarClassificacao(classificacao: Classificacao){
    this.classificacaoService.editar(classificacao).subscribe(
      (resultado: any) => {
        console.log(resultado);
        this.classificacaoSelecionado = resultado;
        this.carregarClassificacoes();
        this.toastr.success('Editado com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Editar: ${erro}`);
      }
    );
  }

  excluirClassificacao(classificacao: Classificacao, template: any){
    this.openModal(template);
    this.classificacao = classificacao;
    this.bodyDeletarClassificacao = `Classificação: ${classificacao.descricao}`;
    this.bodyDeletarMensagem = `Tem certeza que deseja excluir?`;
    this.bodyDeletarCodigo = `Código: ${classificacao.id}`;
  }

  confirmDeleteClassificacao(classificacao: Classificacao){
    this.classificacaoService.deletar(classificacao.id).subscribe(
      () => {
        this.modalRef.hide();
        this.carregarClassificacoes();
        this.toastr.success('Deletado com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Deletar: ${erro}`);
      }
    );
  }
}
