import { Autor } from "./Autor";
import { Classificacao } from "./Classificacao";
import { Genero } from "./Genero";
import { Marca } from "./Marca";
import { Segmento } from "./Segmento";

export class Livro {
    id: number;
    nome: string;
    edicao: number;
    pagina: number;
    tipoCapa: string;
    dataPublicacao: Date;
    segmentoId: number;
    segmento: Segmento;
    marcaId: number;
    marca: Marca;
    autorId: number;
    autor: Autor;
    classificacaoId: number;
    classificacao: Classificacao;
    generoId: number;
    genero: Genero;
    imagemURL: string;


    constructor() {
        this.id = 0;
        this.nome = '';
        this.edicao = 0;
        this.pagina = 0;
        this.tipoCapa = '';
        this.dataPublicacao = new Date();
        this.segmentoId = 0;
        this.segmento = new Segmento();
        this.marcaId = 0;
        this.marca = new Marca();
        this.autorId = 0;
        this.autor = new Autor();
        this.classificacaoId = 0;
        this.classificacao = new Classificacao();
        this.generoId = 0;
        this.genero = new Genero();
        this.imagemURL = '';
    }
}
