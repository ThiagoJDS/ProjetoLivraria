import { Cliente } from "./Cliente";

export class Compra {
    id: number;
    clienteId: number;
    cliente: Cliente;
    total: number;

    constructor() {
        this.id = 0;
        this.clienteId = 0;
        this.cliente = new Cliente();
        this.total = 0;
    }
}
