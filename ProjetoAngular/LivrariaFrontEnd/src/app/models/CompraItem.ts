import { Compra } from "./Compra";
import { Livro } from "./Livro";

export class CompraItem {
    id: number;
    valorUnitario: number;
    quantidade: number;
    livroId: number;
    livro: Livro;
    compraId: number;
    compra: Compra;

    constructor() {
        
        this.id = 0;
        this.valorUnitario = 0;
        this.quantidade = 0;
        this.livroId = 0
        this.livro = new Livro();
        this.compraId = 0;
        this.compra = new Compra();
    }
}
