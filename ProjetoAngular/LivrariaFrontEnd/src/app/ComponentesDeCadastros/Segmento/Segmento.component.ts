import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Segmento } from '../../models/Segmento';
import { SegmentoService } from '../../services/segmento.service';

@Component({
  selector: 'app-Segmento',
  templateUrl: './Segmento.component.html',
  styleUrls: ['./Segmento.component.css']
})
export class SegmentoComponent implements OnInit {

  public titulo = 'Segmentos';
  public segmentoSelecionado: Segmento = new Segmento();
  public modalRef: BsModalRef = new BsModalRef();
  public segmentos: Segmento[] = [];
  segmentosFiltrados: Segmento[] = [];
  segmento: Segmento | any;
  registerForm: FormGroup | any;
  modoSalvar = 'post';
  bodyDeletarSegmento = '';
  bodyDeletarMensagem = '';
  bodyDeletarCodigo = '';
  fileNameToUpdate: string = '';
  _filtroLista: string = '';

  constructor(private fb: FormBuilder,
    private modalService: BsModalService,
    private segmentoService: SegmentoService,
    private toastr: ToastrService) {
      this.createForm();
  }

  ngOnInit(): void {
    this.carregarSegmentos();
  }

  get f(): any {
    return this.segmentoForm.controls;
  }
  public cssValidator(campoForm: FormControl | AbstractControl): any {
    return { 'is-invalid': campoForm.errors && campoForm.touched };
  }

  get filtroLista(): string{
    return this._filtroLista;
  }
  set filtroLista(value: string){
    this._filtroLista = value;
    this.segmentosFiltrados = this.filtroLista ? this.filtrarSegmentos(this.filtroLista) : this.segmentos;
  }

  filtrarSegmentos(filtrarPor: string): Segmento[]{
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.segmentos.filter(
      segmento => segmento.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  segmentoForm = new FormGroup({
    id: new FormControl(''),
    descricao: new FormControl('')
  });

  createForm() {
     this.segmentoForm = this.fb.group({
       id: [''],
       descricao: ['', Validators.required],
     });
   }

  carregarSegmentos(){
    this.segmentoService.obterTodos().subscribe(
      (resultado: Segmento[]) => {
        this.segmentos = resultado;
        this.segmentosFiltrados = this.segmentos;
      },
      error => { this.toastr.error(`Erro ao tentar Carregar segmentos: ${error}`); }
    );
  }

  segmentoSelect(segmento: Segmento, template: any){
    this.segmentoSelecionado = segmento;
    this.segmentoForm.patchValue(segmento);
    this.modoSalvar = 'put';
    this.openModal(template);
  }

  voltar(){
    this.segmentoSelecionado = new Segmento();
  }

  novoSegmento(template: any){
    this.segmentoSelecionado = new Segmento();
    this.segmentoSelecionado.id = -1;
    this.segmentoForm.patchValue(this.segmentoSelecionado);
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  segmentoSubmit(){
    if(this.segmentoSelecionado.id === -1){
      this.salvarSegmento(this.segmentoForm.value);
      this.modalRef.hide();
    }
    else{
      this.editarSegmento(this.segmentoForm.value);
      this.modalRef.hide();
    }
  }

  salvarSegmento(segmento: Segmento){
    segmento.id = 0;
    this.segmentoService.salvar(segmento).subscribe(
      (resultado: any) => {
        console.log(resultado);
        this.segmentoSelecionado = resultado;
        this.carregarSegmentos();
        this.toastr.success('Inserido com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Inserir: ${erro}`);
      }
    );
  }

  editarSegmento(segmento: Segmento){
    this.segmentoService.editar(segmento).subscribe(
      (resultado: any) => {
        console.log(resultado);
        this.segmentoSelecionado = resultado;
        this.carregarSegmentos();
        this.toastr.success('Editado com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Editar: ${erro}`);
      }
    );
  }

  excluirSegmento(segmento: Segmento, template: any) {
    this.openModal(template);
    this.segmento = segmento;
    this.bodyDeletarSegmento = `Segmento: ${segmento.descricao}`;
    this.bodyDeletarMensagem =  `Tem certeza que deseja excluir?`;
    this.bodyDeletarCodigo = `Código: ${segmento.id}`;
  }

  confirmDeleteSegmento(segmento: Segmento){
    this.segmentoService.deletar(segmento.id).subscribe(
      () => {
        this.modalRef.hide();
        this.carregarSegmentos();
        this.toastr.success('Deletado com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Deletar: ${erro}`);
      }
    );
  }
}
