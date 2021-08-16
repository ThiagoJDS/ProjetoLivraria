import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Administrador } from '../../models/Administrador';
import { Cliente } from '../../models/Cliente';
import { Compra } from '../../models/Compra';
import { AdministradorService } from '../../services/administrador.service';
import { ClienteService } from '../../services/cliente.service';
import { CompraService } from '../../services/compra.service';

@Component({
  selector: 'app-Compra',
  templateUrl: './Compra.component.html',
  styleUrls: ['./Compra.component.css']
})
export class CompraComponent implements OnInit {

  public titulo = 'Compras';
  public compraSelecionado: Compra = new Compra();
  public modalRef: BsModalRef = new BsModalRef();
  public compras: Compra[] = [];
  public clientes: Cliente[] = [];
  compraForm!: FormGroup;
  comprasFiltrados: Compra[] = [];
  compra = {} as Compra;
  registerForm: FormGroup | any;
  modoSalvar = 'post';
  bodyDeletarCompra = '';
  bodyDeletarMensagem = '';
  bodyDeletarCodigo = '';
  fileNameToUpdate: string = '';
  _filtroLista: string = '';

  constructor(private fb: FormBuilder,
    private modalService: BsModalService,
    private compraService: CompraService,
    private clienteService: ClienteService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.carregarCompras();
    this.carregarClientes();
    this.createForm();
  }

  get f(): any {
    return this.compraForm.controls;
   }
  public cssValidator(campoForm: FormControl | AbstractControl): any {
     return { 'is-invalid': campoForm.errors && campoForm.touched };
   }

  get filtroLista(): string{
    return this._filtroLista;
  }
  set filtroLista(value: string){
    this._filtroLista = value;
    this.comprasFiltrados = this.filtroLista ? this.filtrarCompras(this.filtroLista) : this.compras;
  }

  filtrarCompras(filtrarPor: string): Compra[]{
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.compras.filter(
      compra => compra.cliente.nome.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

   createForm() {
     this.compraForm = this.fb.group({
       id: [''],
       clienteId: ['', Validators.required],
       total: ['', Validators.required]
     });
   }

  carregarCompras(){
    this.compraService.obterTodos().subscribe(
      (resultado: Compra[]) => {
        this.compras = resultado;
        this.comprasFiltrados = this.compras;
        console.log(this.compras);
      },
      error => { this.toastr.error(`Erro ao tentar Carregar autores: ${error}`); }
    );
  }

  carregarClientes(){
    this.clienteService.obterTodos().subscribe(
      (resultado: Cliente[]) => {
        this.clientes = resultado;
      },
      error => { this.toastr.error(`Erro ao tentar Carregar autores: ${error}`); }
    );
  }

  compraSelect(compra: Compra, template: any){
    this.compraSelecionado = compra;
    this.compraForm.patchValue(compra);
    this.modoSalvar = 'put';
    this.openModal(template);
  }

  voltar(){
    this.compraSelecionado = new Compra();
  }

  novoCompra(template: any){
    this.compraSelecionado = new Compra();
    this.compraSelecionado.id = -1;
    this.compraForm.patchValue(this.compraSelecionado);
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  compraSubmit(){
    if(this.compraSelecionado.id === -1){
      this.salvarCompra(this.compraForm.value);
      this.modalRef.hide();
    }
    else{
      this.editarCompra(this.compraForm.value);
      this.modalRef.hide();
    }
  }

  salvarCompra(compra: Compra){
    compra.id = 0;
    this.compraService.salvar(compra).subscribe(
      (resultado: any) => {
        console.log(resultado);
        this.compraSelecionado = resultado;
        this.carregarCompras();
        this.toastr.success('Inserido com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Inserir: ${erro}`);
      }
    );
  }

  editarCompra(compra: Compra){
    this.compraService.editar(compra).subscribe(
      (resultado: any) => {
        console.log(resultado);
        this.compraSelecionado = resultado;
        this.carregarCompras();
        this.toastr.success('Editado com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Editar: ${erro}`);
      }
    );
  }

  excluirCompra(compra: Compra, template: any){
    this.openModal(template);
    this.compra = compra;
    this.bodyDeletarCompra = `Compra: ${compra.cliente.nome}`;
    this.bodyDeletarMensagem = `Tem certeza que deseja excluir?`;
    this.bodyDeletarCodigo = `Código: ${compra.id}`;
  }

  confirmDeleteCompra(compra: Compra){
    this.compraService.deletar(compra.id).subscribe(
      () => {
        this.modalRef.hide();
        this.carregarCompras();
        this.toastr.success('Deletado com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Deletar: ${erro}`);
      }
    );
  }
}
