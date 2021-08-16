import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Marca } from '../../models/Marca';
import { MarcaService } from '../../services/marca.service';

@Component({
  selector: 'app-Marca',
  templateUrl: './Marca.component.html',
  styleUrls: ['./Marca.component.css']
})
export class MarcaComponent implements OnInit {

  public titulo = 'Marcas';
  public marcaSelecionado: Marca = new Marca();
  public modalRef: BsModalRef = new BsModalRef();
  public marcas: Marca[] = [];
  pag: number = 1;
  contador: number = 5;
  marcasFiltrados: Marca[] = [];
  marca: Marca | any;
  registerForm: FormGroup | any;
  modoSalvar = 'post';
  bodyDeletarMarca = '';
  bodyDeletarMensagem = '';
  bodyDeletarCodigo = '';
  fileNameToUpdate: string = '';
  _filtroLista: string = '';

  constructor(private fb: FormBuilder,
    private modalService: BsModalService,
    private marcaService: MarcaService,
    private toastr: ToastrService) {
      this.createForm();
    }

  ngOnInit(): void {
      this.carregarMarcas();
    }

  get f(): any {
      return this.marcaForm.controls;
    }
  public cssValidator(campoForm: FormControl | AbstractControl): any {
      return { 'is-invalid': campoForm.errors && campoForm.touched };
    }
  get filtroLista(): string{
      return this._filtroLista;
    }
  set filtroLista(value: string){
      this._filtroLista = value;
      this.marcasFiltrados = this.filtroLista ? this.filtrarMarcas(this.filtroLista) : this.marcas;
    }

  filtrarMarcas(filtrarPor: string): Marca[]{
      filtrarPor = filtrarPor.toLocaleLowerCase();
      return this.marcas.filter(
        marca => marca.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== -1
      )
    }

  openModal(template: TemplateRef<any>) {
      this.modalRef = this.modalService.show(template);
    }

  marcaForm = new FormGroup({
    id: new FormControl(''),
    descricao: new FormControl('')
  });

  createForm() {
       this.marcaForm = this.fb.group({
         id: [''],
         descricao: ['', Validators.required],
      });
    }

  carregarMarcas(){
    this.marcaService.obterTodos().subscribe(
      (resultado: Marca[]) => {
        this.marcas = resultado;
        this.marcasFiltrados = this.marcas;
        console.log(this.marcas);
      },
      error => { this.toastr.error(`Erro ao tentar Carregar marcas: ${error}`); }
    );
  }

  marcaSelect(marca: Marca, template: any){
    this.marcaSelecionado = marca;
    this.marcaForm.patchValue(marca);
    this.modoSalvar = 'put';
    this.openModal(template);
  }

  voltar(){
    this.marcaSelecionado = new Marca();
  }

  novoMarca(template: any){
    this.marcaSelecionado = new Marca();
    this.marcaSelecionado.id = -1;
    this.marcaForm.patchValue(this.marcaSelecionado);
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  marcaSubmit(){
    if(this.marcaSelecionado.id === -1){
      this.salvarMarca(this.marcaForm.value);
      this.modalRef.hide();
    }
    else{
      this.editarMarca(this.marcaForm.value);
      this.modalRef.hide();
    }
  }

  salvarMarca(marca: Marca){
    marca.id = 0;
    this.marcaService.salvar(marca).subscribe(
      (resultado: any) => {
        console.log(resultado);
        this.marcaSelecionado = resultado;
        this.carregarMarcas();
        this.toastr.success('Inserido com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Inserir: ${erro}`);
      }
    );
  }

  editarMarca(marca: Marca){
    this.marcaService.editar(marca).subscribe(
      (resultado: any) => {
        console.log(resultado);
        this.marcaSelecionado = resultado;
        this.carregarMarcas();
        this.toastr.success('Editado com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Editar: ${erro}`);
      }
    );
  }

  excluirMarca(marca: Marca, template: any){
    this.openModal(template);
    this.marca = marca;
    this.bodyDeletarMarca = `Marca: ${marca.descricao}`;
    this.bodyDeletarMensagem = `Tem certeza que deseja excluir?`;
    this.bodyDeletarCodigo = `Código: ${marca.id}`;
  }

  confirmDeleteMarca(marca: Marca){
    this.marcaService.deletar(marca.id).subscribe(
      () => {
        this.modalRef.hide();
        this.carregarMarcas();
        this.toastr.success('Deletado com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Deletar: ${erro}`);
      }
    );
  }
}
