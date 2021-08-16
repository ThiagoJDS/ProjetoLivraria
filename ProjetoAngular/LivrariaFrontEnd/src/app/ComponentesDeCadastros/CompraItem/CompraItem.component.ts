import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Compra } from '../../models/Compra';
import { CompraItem } from '../../models/CompraItem';
import { Livro } from '../../models/Livro';
import { CompraService } from '../../services/compra.service';
import { CompraItemService } from '../../services/compraItem.service';
import { LivroService } from '../../services/livro.service';

@Component({
  selector: 'app-CompraItem',
  templateUrl: './CompraItem.component.html',
  styleUrls: ['./CompraItem.component.css']
})
export class CompraItemComponent implements OnInit {

  public titulo = 'CompraItems';
  public compraItemSelecionado: CompraItem = new CompraItem();
  public modalRef: BsModalRef = new BsModalRef();
  public compraItems: CompraItem[] = [];
  public livros: Livro[] = [];
  public compras: Compra[] = [];
  pag: number = 1;
  contador: number = 5;
  compraItemForm!: FormGroup;
  compraItemsFiltrados: CompraItem[] = [];
  compraItem: CompraItem | any;
  registerForm: FormGroup | any;
  modoSalvar = 'post';
  bodyDeletarCompraItem = '';
  bodyDeletarMensagem = '';
  bodyDeletarCodigo = '';
  fileNameToUpdate: string = '';
  _filtroLista: string = '';

  constructor(private fb: FormBuilder,
    private modalService: BsModalService,
    private compraItemService: CompraItemService,
    private livroService: LivroService,
    private compraService: CompraService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.carregarCompraItems();
    this.carregarLivros();
    this.carregarCompras();
    this.createForm()
  }

  get f(): any {
    return this.compraItemForm.controls;
   }
  public cssValidator(campoForm: FormControl | AbstractControl): any {
     return { 'is-invalid': campoForm.errors && campoForm.touched };
   }

  get filtroLista(): string{
    return this._filtroLista;
  }
  set filtroLista(value: string){
    this._filtroLista = value;
    this.compraItemsFiltrados = this.filtroLista ? this.filtrarCompraItems(this.filtroLista) : this.compraItems;
  }

  filtrarCompraItems(filtrarPor: string): CompraItem[]{
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.compraItems.filter(
      compraItem => compraItem.livro.nome.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

   createForm() {
     this.compraItemForm = this.fb.group({
       id: [''],
       valorUnitario: ['', Validators.required],
       quantidade: ['', Validators.required],
       livroId: ['', Validators.required],
       compraId: ['', Validators.required]
     });
   }

  carregarCompraItems(){
    this.compraItemService.obterTodos().subscribe(
      (resultado: CompraItem[]) => {
        this.compraItems = resultado;
        this.compraItemsFiltrados = this.compraItems;
        console.log(this.compraItems);
      },
      error => { this.toastr.error(`Erro ao tentar Carregar compraItems: ${error}`); }
    );
  }

  carregarLivros(){
    this.livroService.obterTodos().subscribe(
      (resultado: Livro[]) => {
        this.livros = resultado;
      },
      error => { this.toastr.error(`Erro ao tentar Carregar livros: ${error}`); }
    );
  }

  carregarCompras(){
    this.compraService.obterTodos().subscribe(
      (resultado: Compra[]) => {
        this.compras = resultado;
      },
      error => { this.toastr.error(`Erro ao tentar Carregar compra: ${error}`); }
    );
  }

  compraItemSelect(compraItem: CompraItem, template: any){
    this.compraItemSelecionado = compraItem;
    this.compraItemForm.patchValue(compraItem);
    this.modoSalvar = 'put';
    this.openModal(template);
  }

  voltar(){
    this.compraItemSelecionado = new CompraItem();
  }

  novoCompraItem(template: any){
    this.compraItemSelecionado = new CompraItem();
    this.compraItemSelecionado.id = -1;
    this.compraItemForm.patchValue(this.compraItemSelecionado);
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  compraItemSubmit(){
    if(this.compraItemSelecionado.id === -1){
      this.salvarCompraItem(this.compraItemForm.value);
      this.modalRef.hide();
    }
    else{
      this.editarCompraItem(this.compraItemForm.value);
      this.modalRef.hide();
    }
  }

  salvarCompraItem(compraItem: CompraItem){
    compraItem.id = 0;
    this.compraItemService.salvar(compraItem).subscribe(
      (resultado: any) => {
        console.log(resultado);
        this.compraItemSelecionado = resultado;
        this.carregarCompraItems();
        this.toastr.success('Inserido com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Inserir: ${erro}`);
      }
    );
  }

  editarCompraItem(compraItem: CompraItem){
    this.compraItemService.editar(compraItem).subscribe(
      (resultado: any) => {
        console.log(resultado);
        this.compraItemSelecionado = resultado;
        this.carregarCompraItems();
        this.toastr.success('Editado com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Editar: ${erro}`);
      }
    );
  }

  excluirCompraItem(compraItem: CompraItem, template: any){
    this.openModal(template);
    this.compraItem = compraItem;
    this.bodyDeletarCompraItem = `CompraItem: ${compraItem.livro.nome}`;
    this.bodyDeletarMensagem = `Tem certeza que deseja excluir?`;
    this.bodyDeletarCodigo = `Código: ${compraItem.id}`;
  }

  confirmDeleteCompraItem(compraItem: CompraItem){
    this.compraItemService.deletar(compraItem.id).subscribe(
      () => {
        this.modalRef.hide();
        this.carregarCompraItems();
        this.toastr.success('Deletado com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Deletar: ${erro}`);
      }
    );
  }
}
