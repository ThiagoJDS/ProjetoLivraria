import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Autor } from '../../models/Autor';
import { AutorService } from '../../services/autor.service';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-Autor',
  templateUrl: './Autor.component.html',
  styleUrls: ['./Autor.component.css']
})
export class AutorComponent implements OnInit {

  public titulo = 'Autores';
  public autorSelecionado: Autor = new Autor();
  public modalRef: BsModalRef = new BsModalRef();
  public autores: Autor[] = [];
  autorForm!: FormGroup;
  autoresFiltrados: Autor[] = [];
  autor: Autor |  any;
  registerForm: FormGroup | any;
  modoSalvar = 'post';
  bodyDeletarAutor = '';
  bodyDeletarMensagem = '';
  bodyDeletarCodigo = '';
  fileNameToUpdate: string = '';
  _filtroLista: string = '';

  constructor(private fb: FormBuilder,
    private modalService: BsModalService,
    private autorService: AutorService,
    private toastr: ToastrService) {
      this.createForm();
    }

    ngOnInit(): void {
      this.carregarAutores();
    }

    get f(): any {
      return this.autorForm.controls;
    }
    public cssValidator(campoForm: FormControl | AbstractControl): any {
      return { 'is-invalid': campoForm.errors && campoForm.touched };
    }

    get filtroLista(): string{
      return this._filtroLista;
    }
    set filtroLista(value: string){
      this._filtroLista = value;
      this.autoresFiltrados = this.filtroLista ? this.filtrarAutores(this.filtroLista) : this.autores;
    }

    filtrarAutores(filtrarPor: string): Autor[]{
      filtrarPor = filtrarPor.toLocaleLowerCase();
      return this.autores.filter(
        autor => autor.nome.toLocaleLowerCase().indexOf(filtrarPor) !== -1

      )
    }

    openModal(template: TemplateRef<any>) {
      this.modalRef = this.modalService.show(template);
    }

     createForm() {
       this.autorForm = this.fb.group({
         id: [''],
         nome: ['', Validators.required],
       });
     }

  carregarAutores(){
    this.autorService.obterTodos().subscribe(
      (resultado: Autor[]) => {
        this.autores = resultado;
        this.autoresFiltrados = this.autores;
        console.log(this.autores);
      },
      error => { this.toastr.error(`Erro ao tentar Carregar autores: ${error}`); }
    );
  }

  autorSelect(autor: Autor, template: any){
    this.autorSelecionado = autor;
    this.autorForm.patchValue(autor);
    this.modoSalvar = 'put';
    this.openModal(template);
  }

  voltar(){
    this.autorSelecionado = new Autor();
  }

  novoAutor(template: any){
    this.autorSelecionado = new Autor();
    this.autorSelecionado.id = -1;
    this.autorForm.patchValue(this.autorSelecionado);
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  autorSubmit(){
    if(this.autorSelecionado.id === -1){
      this.salvarAutor(this.autorForm.value);
      this.modalRef.hide();
    }
    else{
      this.editarAutor(this.autorForm.value);
      this.modalRef.hide();
    }
  }

  salvarAutor(autor: Autor){
    autor.id = 0;
    this.autorService.salvar(autor).subscribe(
      (resultado: any) => {
        console.log(resultado);
        this.autorSelecionado = resultado;
        this.carregarAutores();
        this.toastr.success('Inserido com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Inserir: ${erro}`);
      }
    );
  }

  editarAutor(autor: Autor){
    this.autorService.editar(autor).subscribe(
      (resultado: any) => {
        console.log(resultado);
        this.autorSelecionado = resultado;
        this.carregarAutores();
        this.toastr.success('Editado com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Editar: ${erro}`);
      }
    );
  }

  excluirAutor(autor: Autor, template: any) {
    this.openModal(template);
    this.autor = autor;
    this.bodyDeletarAutor = `Autor(a): ${autor.nome}`;
    this.bodyDeletarMensagem = `Tem certeza que deseja excluir?`;
    this.bodyDeletarCodigo = `Código: ${autor.id}`;
  }

  confirmDeleteAutor(autor: Autor){
    this.autorService.deletar(autor.id).subscribe(
      () => {
        this.modalRef.hide();
        this.carregarAutores();
        this.toastr.success('Deletado com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Deletar: ${erro}`);
      }
    );
  }
}
