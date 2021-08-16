import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { Autor } from '../../models/Autor';
import { Classificacao } from '../../models/Classificacao';
import { Genero } from '../../models/Genero';
import { Livro } from '../../models/Livro';
import { Marca } from '../../models/Marca';
import { Segmento } from '../../models/Segmento';
import { AutorService } from '../../services/autor.service';
import { ClassificacaoService } from '../../services/classificacao.service';
import { GeneroService } from '../../services/genero.service';
import { LivroService } from '../../services/livro.service';
import { MarcaService } from '../../services/marca.service';
import { SegmentoService } from '../../services/segmento.service';

@Component({
  selector: 'app-CLivro',
  templateUrl: './CLivro.component.html',
  styleUrls: ['./CLivro.component.css']
})
export class CLivroComponent implements OnInit {

  public titulo = 'Livros';
  public livroSelecionado: Livro = new Livro();
  public modalRef: BsModalRef = new BsModalRef();
  public livros: Livro[] = [];
  public segmentos: Segmento[] = [];
  public marcas: Marca[] = [];
  public autores: Autor[] = [];
  public classificacoes: Classificacao[] = [];
  public generos: Genero[] = [];
  livrosFiltrados: Livro[] = [];
  livro: Livro | any;
  registerForm: FormGroup | any;
  modoSalvar = 'post';
  bodyDeletarLivro = '';
  bodyDeletarMensagem = '';
  bodyDeletarCodigo = '';
  fileNameToUpdate = {} as File;
  dataAtual: string = '';
  _filtroLista: string = '';
  file = {} as File | any;
  livroId = {} as number;
  imagemURL = 'assets/img/upload.png';
  public larguraImagem = 50;
  public margemImagem = 2;
  public exibirImagem = true;

  constructor(private fb: FormBuilder,
    private modalService: BsModalService,
    private livroService: LivroService,
    private segmentoService: SegmentoService,
    private marcaService: MarcaService,
    private autorService: AutorService,
    private classificacaoService: ClassificacaoService,
    private generoService: GeneroService,
    private toastr: ToastrService) {
      this.createForm();
     }

  ngOnInit(): void {
      this.carregarLivros();
      this.carregarSegmentos();
      this.carregarMarcas();
      this.carregarAutores();
      this.carregarClassificacoes();
      this.carregarGeneros();
    }

  get f(): any {
      return this.livroForm.controls;
     }
  public cssValidator(campoForm: FormControl | AbstractControl): any {
       return { 'is-invalid': campoForm.errors && campoForm.touched };
     }

  get filtroLista(): string{
      return this._filtroLista;
    }
  set filtroLista(value: string){
      this._filtroLista = value;
      this.livrosFiltrados = this.filtroLista ? this.filtrarLivros(this.filtroLista) : this.livros;
    }

  filtrarLivros(filtrarPor: string): Livro[]{
      filtrarPor = filtrarPor.toLocaleLowerCase();
      return this.livros.filter(
        livro => livro.nome.toLocaleLowerCase().indexOf(filtrarPor) !== -1
      )
    }

  openModal(template: TemplateRef<any>) {
      this.modalRef = this.modalService.show(template);
    }

    public alterarImagem(): void {
      this.exibirImagem = !this.exibirImagem;
    }

    public mostraImagem(imagemURL: string): string {
      return (imagemURL !== '')
        ? `${environment.urlApi}resources/images/${imagemURL}`
        : 'assets/img/semImagem.jpeg';
    }

  livroForm = new FormGroup({
    id: new FormControl(''),
    nome: new FormControl(''),
    edicao: new FormControl(''),
    pagina: new FormControl(''),
    tipoCapa: new FormControl(''),
    dataPublicacao: new FormControl(''),
    segmentoId: new FormControl(''),
    marcaId: new FormControl(''),
    autorId: new FormControl(''),
    classificacaoId: new FormControl(''),
    generoId: new FormControl(''),
    imagemURL: new FormControl('')
  });

  createForm() {
    this.livroForm = this.fb.group({
        id: [''],
        nome: ['', Validators.required],
        edicao: ['', Validators.required],
        pagina: ['', Validators.required],
        tipoCapa: ['', Validators.required],
        dataPublicacao: ['', Validators.required],
        segmentoId: ['', Validators.required],
        marcaId: ['', Validators.required],
        autorId: ['', Validators.required],
        classificacaoId: ['', Validators.required],
        generoId: ['', Validators.required],
        imagemURL: ['', Validators.required]
       });
     }

  carregarLivros(){
    this.livroService.obterTodos().subscribe(
      (resultado: Livro[]) => {
        this.livros = resultado;
        this.livrosFiltrados = this.livros;
        console.log(this.livros);
      },
      error => { this.toastr.error(`Erro ao tentar Carregar livros: ${error}`); }
    );
  }

  carregarSegmentos(){
    this.segmentoService.obterTodos().subscribe(
      (resultado: Segmento[]) => {
        this.segmentos = resultado;
      },
      error => { this.toastr.error(`Erro ao tentar Carregar segmentos: ${error}`); }
    );
  }

  carregarMarcas(){
    this.marcaService.obterTodos().subscribe(
      (resultado: Marca[]) => {
        this.marcas = resultado;
      },
      error => { this.toastr.error(`Erro ao tentar Carregar marcas: ${error}`); }
    );
  }

  carregarAutores(){
    this.autorService.obterTodos().subscribe(
      (resultado: Autor[]) => {
        this.autores = resultado;
      },
      error => { this.toastr.error(`Erro ao tentar Carregar autores: ${error}`); }
    );
  }

  carregarClassificacoes(){
    this.classificacaoService.obterTodos().subscribe(
      (resultado: Classificacao[]) => {
        this.classificacoes = resultado;
      },
      error => { this.toastr.error(`Erro ao tentar Carregar classificações: ${error}`); }
    );
  }

  carregarGeneros(){
    this.generoService.obterTodos().subscribe(
      (resultado: Genero[]) => {
        this.generos = resultado;
      },
      error => { this.toastr.error(`Erro ao tentar Carregar gêneros: ${error}`); }
    );
  }

  livroSelect(livro: Livro, template: any){
    this.livroSelecionado = livro;
    this.livroForm.patchValue(livro);
    this.modoSalvar = 'put';
    this.openModal(template);
  }

  voltar(){
    this.livroSelecionado = new Livro();
  }

  novoLivro(template: any){
    this.livroSelecionado = new Livro();
    this.livroSelecionado.id = -1;
    this.livroForm.patchValue(this.livroSelecionado);
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  livroSubmit(){
    if(this.livroSelecionado.id === -1){
      this.salvarLivro(this.livroForm.value);
      this.modalRef.hide();
    }
    else{
      this.editarLivro(this.livroForm.value);
      this.modalRef.hide();
    }
  }

  salvarLivro(livro: Livro){
    livro.id = 0;
    this.livroService.salvar(livro).subscribe(
      (resultado: any) => {
        console.log(resultado);
        this.livroSelecionado = resultado;
        this.carregarLivros();
        this.toastr.success('Inserido com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Inserir: ${erro}`);
      }
    );
  }

  editarLivro(livro: Livro){
    this.livroService.editar(livro).subscribe(
      (resultado: any) => {
        console.log(resultado);
        this.livroSelecionado = resultado;
        this.carregarLivros();
        this.toastr.success('Editado com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Editar: ${erro}`);
      }
    );
  }

  excluirLivro(livro:Livro, template: any){
    this.openModal(template);
    this.livro = livro;
    this.bodyDeletarLivro = `Livro: ${livro.nome}`;
    this.bodyDeletarMensagem = `Tem certeza que deseja excluir?`;
    this.bodyDeletarCodigo = `Código: ${livro.id}`;
  }

  confirmDeleteLivro(livro: Livro){
    this.livroService.deletar(livro.id).subscribe(
      () => {
        this.modalRef.hide();
        this.carregarLivros();
        this.toastr.success('Deletado com Sucesso!');
      },
      (erro: any) => {
        console.log(erro);
        this.toastr.success(`Erro ao Deletar: ${erro}`);
      }
    );
  }
}
