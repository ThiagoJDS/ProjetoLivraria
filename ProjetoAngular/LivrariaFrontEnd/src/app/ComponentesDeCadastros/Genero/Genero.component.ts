import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Genero } from '../../models/Genero';
import { GeneroService } from '../../services/genero.service';

@Component({
  selector: 'app-Genero',
  templateUrl: './Genero.component.html',
  styleUrls: ['./Genero.component.css']
})
export class GeneroComponent implements OnInit {

  public titulo = 'Gêneros';
  public generoSelecionado: Genero = new Genero();
  public modalRef: BsModalRef = new BsModalRef();
  public generos: Genero[] = [];
  generosFiltrados: Genero[] = [];
  genero: Genero | any;
  registerForm: FormGroup | any;
  modoSalvar = 'post';
  bodyDeletarGenero = '';
  bodyDeletarMensagem = '';
  bodyDeletarCodigo = '';
  fileNameToUpdate: string = '';
  _filtroLista: string = '';

  constructor(private fb: FormBuilder,
    private modalService: BsModalService,
    private generoService: GeneroService,
    private toastr: ToastrService) {
      this.createForm();
    }

  ngOnInit() : void {
      this.carregarGeneros();
    }

  get f(): any {
      return this.generoForm.controls;
    }
  public cssValidator(campoForm: FormControl | AbstractControl): any {
      return { 'is-invalid': campoForm.errors && campoForm.touched };
    }

  get filtroLista(): string{
      return this._filtroLista;
    }
  set filtroLista(value: string){
      this._filtroLista = value;
      this.generosFiltrados = this.filtroLista ? this.filtrarGeneros(this.filtroLista) : this.generos;
    }

  filtrarGeneros(filtrarPor: string): Genero[]{
      filtrarPor = filtrarPor.toLocaleLowerCase();
      return this.generos.filter(
        genero => genero.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== -1
      )
    }

  openModal(template: TemplateRef<any>) {
      this.modalRef = this.modalService.show(template);
    }

  generoForm = new FormGroup({
    id: new FormControl(''),
    descricao: new FormControl('')
  });

  createForm() {
       this.generoForm = this.fb.group({
         id: [''],
         descricao: ['', Validators.required],
       });
     }

  carregarGeneros(){
    this.generoService.obterTodos().subscribe(
      (resultado: Genero[]) => {
        this.generos = resultado;
        this.generosFiltrados = this.generos;
        console.log(this.generos);
      },
      error => { this.toastr.error(`Erro ao tentar Carregar gêneros: ${error}`); }
    );
  }

  generoSelect(genero: Genero, template: any){
    this.generoSelecionado = genero;
    this.generoForm.patchValue(genero);
    this.modoSalvar = 'put';
    this.openModal(template);
  }

  voltar(){
    this.generoSelecionado = new Genero();
  }

  novoGenero(template: any){
    this.generoSelecionado = new Genero();
    this.generoSelecionado.id = -1;
    this.generoForm.patchValue(this.generoSelecionado);
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  generoSubmit(){
    if(this.generoSelecionado.id === -1){
      this.salvarGenero(this.generoForm.value);
      this.modalRef.hide();
    }
    else{
      this.editarGenero(this.generoForm.value);
      this.modalRef.hide();
    }
  }

  salvarGenero(genero: Genero){
    genero.id = 0;
    this.generoService.salvar(genero).subscribe(
      (resultado: any) => {
        console.log(resultado);
        this.generoSelecionado = resultado;
        this.carregarGeneros();
        this.toastr.success('Inserido com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Inserir: ${erro}`);
      }
    );
  }

  editarGenero(genero: Genero){
    this.generoService.editar(genero).subscribe(
      (resultado: any) => {
        console.log(resultado);
        this.generoSelecionado = resultado;
        this.carregarGeneros();
        this.toastr.success('Editado com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Editar: ${erro}`);
      }
    );
  }

  excluirGenero(genero: Genero, template: any){
    this.openModal(template);
    this.genero = genero;
    this.bodyDeletarGenero = `Gênero: ${genero.descricao}`;
    this.bodyDeletarMensagem = `Tem certeza que deseja excluir?`;
    this.bodyDeletarCodigo = `Código: ${genero.id}`;
  }

  confirmDeleteGenero(genero: Genero){
    this.generoService.deletar(genero.id).subscribe(
      () => {
        this.modalRef.hide();
        this.carregarGeneros();
        this.toastr.success('Deletado com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Deletar: ${erro}`);
      }
    );
  }
}
