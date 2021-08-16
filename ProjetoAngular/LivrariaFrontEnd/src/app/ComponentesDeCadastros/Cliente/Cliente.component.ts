import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Cliente } from '../../models/Cliente';
import { ClienteService } from '../../services/cliente.service';

@Component({
  selector: 'app-Cliente',
  templateUrl: './Cliente.component.html',
  styleUrls: ['./Cliente.component.css']
})
export class ClienteComponent implements OnInit {

  public titulo = 'Clientes';
  public clienteSelecionado: Cliente = new Cliente();
  public modalRef: BsModalRef = new BsModalRef();
  public clientes: Cliente[] = [];
  clientesFiltrados: Cliente[] = [];
  cliente = {} as Cliente;
  registerForm: FormGroup | any;
  modoSalvar = 'post';
  bodyDeletarCliente = '';
  bodyDeletarMensagem = '';
  bodyDeletarCodigo = '';
  fileNameToUpdate: string = '';
  _filtroLista: string = '';

  constructor(private fb: FormBuilder,
    private modalService: BsModalService,
    private clienteService: ClienteService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private localeService: BsLocaleService
    ) {
      this.createForm();
      this.localeService.use('pt-br');
    }

  ngOnInit(): void {
      this.spinner.show();
      this.carregarClientes();
    }

  get bsConfig(): any {
      return {
        adaptivePosition: true,
        dateInputFormat: 'DD/MM/YYYY',
        containerClass: 'theme-default',
        showWeekNumbers: false,
      };
    }

  get f(): any {
      return this.clienteForm.controls;
    }
  public cssValidator(campoForm: FormControl | AbstractControl): any {
      return { 'is-invalid': campoForm.errors && campoForm.touched };
    }

  get filtroLista(): string{
      return this._filtroLista;
    }
  set filtroLista(value: string){
      this._filtroLista = value;
      this.clientesFiltrados = this.filtroLista ? this.filtrarClientes(this.filtroLista) : this.clientes;
    }

  filtrarClientes(filtrarPor: string): Cliente[]{
      filtrarPor = filtrarPor.toLocaleLowerCase();
      return this.clientes.filter(
        cliente => cliente.nome.toLocaleLowerCase().indexOf(filtrarPor) !== -1
      )
    }

  openModal(template: TemplateRef<any>) {
      this.modalRef = this.modalService.show(template);
    }

  clienteForm = new FormGroup({
    id: new FormControl(''),
    nome: new FormControl(''),
    email: new FormControl(''),
    senha: new FormControl(''),
    cpf: new FormControl(''),
    dataNascimento: new FormControl(''),
    sexo: new FormControl(''),
    celular: new FormControl(''),
    estado: new FormControl(''),
    cidade: new FormControl(''),
    endereco: new FormControl('')
  });

  createForm() {
      this.clienteForm = this.fb.group({
        id: [''],
        nome: ['', Validators.required],
        email: ['', Validators.required],
        senha: ['', Validators.required],
        cpf: ['', Validators.required],
        dataNascimento: ['', Validators.required],
        sexo: ['', Validators.required],
        celular: ['', Validators.required],
        estado: ['', Validators.required],
        cidade: ['', Validators.required],
        endereco: ['', Validators.required]
      });
    }

  carregarClientes(){
    this.clienteService.obterTodos().subscribe(
      (resultado: Cliente[]) => {
        this.clientes = resultado;
        this.clientesFiltrados = this.clientes;
        console.log(this.clientes);
      },
      error => { this.toastr.error(`Erro ao tentar Carregar clientes: ${error}`); },
    ).add(() => this.spinner.hide());
  }

  clienteSelect(cliente: Cliente, template: any){
    this.clienteSelecionado = cliente;
    this.clienteForm.patchValue(cliente);
    this.modoSalvar = 'put';
    this.openModal(template);
  }

  voltar(){
    this.clienteSelecionado = new Cliente();
  }

  novoCliente(template: any){
    this.clienteSelecionado = new Cliente();
    this.clienteSelecionado.id = -1;
    this.clienteForm.patchValue(this.clienteSelecionado);
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  clienteSubmit(){
    if(this.clienteSelecionado.id === -1){
      this.salvarCliente(this.clienteForm.value);
      this.modalRef.hide();
    }
    else{
      this.editarCliente(this.clienteForm.value);
      this.modalRef.hide();
    }
  }

  salvarCliente(cliente: Cliente){
    cliente.id = 0;
    this.clienteService.salvar(cliente).subscribe(
      (resultado: any) => {
        console.log(resultado);
        this.clienteSelecionado = resultado;
        this.carregarClientes();
        this.toastr.success('Inserido com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Inserir: ${erro}`);
      }
    );
  }

  editarCliente(cliente: Cliente){
    this.clienteService.editar(cliente).subscribe(
      (resultado: any) => {
        console.log(resultado);
        this.clienteSelecionado = resultado;
        this.carregarClientes();
        this.toastr.success('Editado com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Editar: ${erro}`);
      }
    );
  }

  excluirCliente(cliente: Cliente, template: any){
    this.openModal(template);
    this.cliente = cliente;
    this.bodyDeletarCliente = `Cliente: ${cliente.nome}`;
    this.bodyDeletarMensagem = `Tem certeza que deseja excluir?`;
    this.bodyDeletarCodigo = `Código: ${cliente.id}`;
  }

  confirmDeleteCliente(cliente: Cliente){
    this.clienteService.deletar(cliente.id).subscribe(
      () => {
        this.modalRef.hide();
        this.carregarClientes();
        this.toastr.success('Deletado com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Deletar: ${erro}`);
      }
    );
  }
}
