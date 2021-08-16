export class Cliente {
    id: number;
    nome: string;
    email: string;
    senha: string;
    cpf: string;
    dataNascimento: Date;
    sexo: string;
    celular: string;
    estado: string;
    cidade: string;
    endereco: string;



    constructor() {
        this.id = 0;
        this.nome = '';
        this.email = '';
        this.senha = '';
        this.cpf = '';
        this.dataNascimento = new Date();
        this.sexo = '';
        this.celular = '';
        this.estado = '';
        this.cidade = '';
        this.endereco = '';
    }
}
