import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Autor } from '../models/Autor';
import { Classificacao } from '../models/Classificacao';
import { Genero } from '../models/Genero';
import { Livro } from '../models/Livro';
import { Marca } from '../models/Marca';
import { Segmento } from '../models/Segmento';
import { AutorService } from '../services/autor.service';
import { ClassificacaoService } from '../services/classificacao.service';
import { GeneroService } from '../services/genero.service';
import { LivroService } from '../services/livro.service';
import { MarcaService } from '../services/marca.service';
import { SegmentoService } from '../services/segmento.service';

@Component({
  selector: 'app-Lancamento',
  templateUrl: './Lancamento.component.html',
  styleUrls: ['./Lancamento.component.css']
})
export class LancamentoComponent implements OnInit {

  public modalRef: BsModalRef = new BsModalRef();

  public imagens = [] as any;
  public livros: Livro[] = [];
  public livrosFiltrados: Livro[] = [];
  pag: number = 1;
  contador: number = 1;
  modoSalvar = 'post';
  public livroSelecionado: Livro = new Livro();
  public segmentos: Segmento[] = [];
  public marcas: Marca[] = [];
  public autores: Autor[] = [];
  public classificacoes: Classificacao[] = [];
  public generos: Genero[] = [];

  constructor(private livroService: LivroService,
              private modalService: BsModalService,
              private segmentoService: SegmentoService,
              private marcaService: MarcaService,
              private autorService: AutorService,
              private classificacaoService: ClassificacaoService,
              private generoService: GeneroService,
              private toastr: ToastrService) { }


  ngOnInit(): void {
      this.carregarEventos();
      this.carregarLivros();
      this.carregarSegmentos();
      this.carregarMarcas();
      this.carregarAutores();
      this.carregarClassificacoes();
      this.carregarGeneros();
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

  openModal(template: TemplateRef<any>) {
                this.modalRef = this.modalService.show(template);
              }
  public carregarEventos(): void {
    this.livroService.obterTodos().subscribe({
      next: (livros: Livro[]) => {
        this.livros = livros;
        this.livrosFiltrados = this.livros;
      },
      error: (error: any) => {
       // this.spinner.hide();
        //this.toastr.error('Erro ao Carregar os Eventos', 'Erro!');
        console.error(error);
      },
     // complete: () => this.spinner.hide(),
    });
  }

  livroSelect(livro: Livro, template: any){
    this.livroSelecionado = livro;
    this.livroForm.patchValue(livro);
    this.modoSalvar = 'put';
    this.openModal(template);
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

}
