import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Administrador } from '../../models/Administrador';
import { AdministradorService } from '../../services/administrador.service';

@Component({
  selector: 'app-Administrador',
  templateUrl: './Administrador.component.html',
  styleUrls: ['./Administrador.component.css']
})
export class AdministradorComponent implements OnInit {

  public titulo = 'Administradores';
  public administradorSelecionado: Administrador = new Administrador();
  public modalRef: BsModalRef = new BsModalRef();
  public administradores: Administrador[] = [];
  administradoresFiltrados: Administrador[] = [];
  administrador = {} as Administrador;
  registerForm: FormGroup | any;
  modoSalvar = 'post';
  bodyDeletarAdministrador = '';
  bodyDeletarMensagem = '';
  bodyDeletarCodigo = '';
  fileNameToUpdate: string = '';
  _filtroLista: string = '';

  constructor(private fb: FormBuilder,
    private modalService: BsModalService,
    private administradorService: AdministradorService,
    private toastr: ToastrService) {
      this.createForm();
    }

  ngOnInit(): void {
      this.carregarAdministradores();
    }

  get f(): any {
      return this.administradorForm.controls;
    }
  public cssValidator(campoForm: FormControl | AbstractControl): any {
      return { 'is-invalid': campoForm.errors && campoForm.touched };
    }

  get filtroLista(): string{
      return this._filtroLista;
    }
  set filtroLista(value: string){
      this._filtroLista = value;
      this.administradoresFiltrados = this.filtroLista ? this.filtrarAdministradores(this.filtroLista) : this.administradores;
    }

  filtrarAdministradores(filtrarPor: string): Administrador[]{
      filtrarPor = filtrarPor.toLocaleLowerCase();
      return this.administradores.filter(
        administrador => administrador.nome.toLocaleLowerCase().indexOf(filtrarPor) !== -1
      )
    }

  openModal(template: TemplateRef<any>) {
      this.modalRef = this.modalService.show(template);
    }

  administradorForm = new FormGroup({
    id: new FormControl(''),
    nome: new FormControl(''),
    email: new FormControl(''),
    senha: new FormControl('')
  });

  createForm() {
      this.administradorForm = this.fb.group({
        id: [''],
        nome: ['', Validators.required],
        email: ['', Validators.required],
        senha: ['', Validators.required],
      });
    }

  carregarAdministradores(){
    this.administradorService.obterTodos().subscribe(
      (resultado: Administrador[]) => {
        this.administradores = resultado;
        this.administradoresFiltrados = this.administradores;
      },
      error => { this.toastr.error(`Erro ao tentar Carregar administradores: ${error}`); }
    );
  }

  administradorSelect(administrador: Administrador, template: any){
    this.administradorSelecionado = administrador;
    this.administradorForm.patchValue(administrador);
    this.modoSalvar = 'put';
    this.openModal(template);
  }

  voltar(){
    this.administradorSelecionado = new Administrador();
  }

  novoAdministrador(template: any){
    this.administradorSelecionado = new Administrador();
    this.administradorSelecionado.id = -1;
    this.administradorForm.patchValue(this.administradorSelecionado);
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  administradorSubmit(){
    if(this.administradorSelecionado.id === -1){
      this.salvarAdministrador(this.administradorForm.value);
      this.modalRef.hide();
    }
    else{
      this.editarAdministrador(this.administradorForm.value);
      this.modalRef.hide();
    }
  }

  salvarAdministrador(administrador: Administrador){
    administrador.id = 0;
    this.administradorService.salvar(administrador).subscribe(
      (resultado: any) => {
        console.log(resultado);
        this.administradorSelecionado = resultado;
        this.carregarAdministradores();
        this.toastr.success('Inserido com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Inserir: ${erro}`);
      }
    );
  }

  editarAdministrador(administrador: Administrador){
    this.administradorService.editar(administrador).subscribe(
      (resultado: any) => {
        console.log(resultado);
        this.administradorSelecionado = resultado;
        this.carregarAdministradores();
        this.toastr.success('Editado com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Editar: ${erro}`);
      }
    );
  }

  excluirAdministrador(administrador: Administrador, template: any){
    this.openModal(template);
    this.administrador = administrador;
    this.bodyDeletarAdministrador = `Administrador: ${administrador.nome}`;
    this.bodyDeletarMensagem = `Tem certeza que deseja excluir?`;
    this.bodyDeletarCodigo = `Código: ${administrador.id}`;
  }

  confirmDeleteAdministrador(administrador: Administrador){
    this.administradorService.deletar(administrador.id).subscribe(
      () => {
        this.modalRef.hide();
        this.carregarAdministradores();
        this.toastr.success('Deletado com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Deletar: ${erro}`);
      }
    );
  }
}
