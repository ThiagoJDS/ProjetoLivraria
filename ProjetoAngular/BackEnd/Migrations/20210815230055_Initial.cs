using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrador",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "varchar(70)", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", nullable: false),
                    senha = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrador", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Autor",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "varchar(70)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autor", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Classificacao",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descricao = table.Column<string>(type: "varchar(100)", nullable: false),
                    anos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classificacao", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "varchar(70)", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", nullable: false),
                    senha = table.Column<string>(type: "varchar(20)", nullable: false),
                    cpf = table.Column<string>(type: "varchar(11)", nullable: false),
                    dataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sexo = table.Column<string>(type: "varchar(20)", nullable: false),
                    celular = table.Column<string>(type: "varchar(20)", nullable: false),
                    estado = table.Column<string>(type: "varchar(100)", nullable: false),
                    cidade = table.Column<string>(type: "varchar(100)", nullable: false),
                    endereco = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Genero",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descricao = table.Column<string>(type: "varchar(70)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Marca",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descricao = table.Column<string>(type: "varchar(70)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marca", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Segmento",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descricao = table.Column<string>(type: "varchar(70)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segmento", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clienteId = table.Column<int>(type: "int", nullable: false),
                    total = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compra", x => x.id);
                    table.ForeignKey(
                        name: "FK_Compra_Cliente_clienteId",
                        column: x => x.clienteId,
                        principalTable: "Cliente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Livro",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "varchar(70)", nullable: false),
                    edicao = table.Column<double>(type: "float", nullable: false),
                    pagina = table.Column<int>(type: "int", nullable: false),
                    tipoCapa = table.Column<string>(type: "varchar(20)", nullable: false),
                    dataPublicacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    segmentoId = table.Column<int>(type: "int", nullable: false),
                    marcaId = table.Column<int>(type: "int", nullable: false),
                    autorId = table.Column<int>(type: "int", nullable: false),
                    classificacaoId = table.Column<int>(type: "int", nullable: false),
                    generoId = table.Column<int>(type: "int", nullable: false),
                    imagemURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livro", x => x.id);
                    table.ForeignKey(
                        name: "FK_Livro_Autor_autorId",
                        column: x => x.autorId,
                        principalTable: "Autor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Livro_Classificacao_classificacaoId",
                        column: x => x.classificacaoId,
                        principalTable: "Classificacao",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Livro_Genero_generoId",
                        column: x => x.generoId,
                        principalTable: "Genero",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Livro_Marca_marcaId",
                        column: x => x.marcaId,
                        principalTable: "Marca",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Livro_Segmento_segmentoId",
                        column: x => x.segmentoId,
                        principalTable: "Segmento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompraItem",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    valorUnitario = table.Column<double>(type: "float", nullable: false),
                    quantidade = table.Column<int>(type: "int", nullable: false),
                    livroId = table.Column<int>(type: "int", nullable: false),
                    compraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraItem", x => x.id);
                    table.ForeignKey(
                        name: "FK_CompraItem_Compra_compraId",
                        column: x => x.compraId,
                        principalTable: "Compra",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompraItem_Livro_livroId",
                        column: x => x.livroId,
                        principalTable: "Livro",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Administrador",
                columns: new[] { "id", "email", "nome", "senha" },
                values: new object[,]
                {
                    { 1, "Thiago@hotmail.com", "Thiago Juliano da Silva", "123456" },
                    { 2, "Jaqueline@hotmail.com", "Jaqueline Maria da Silva", "234567" }
                });

            migrationBuilder.InsertData(
                table: "Autor",
                columns: new[] { "id", "nome" },
                values: new object[,]
                {
                    { 1, "Agatha Christie" },
                    { 2, "Stephen Edwin King" },
                    { 3, "Alexandre Dumas" },
                    { 4, "Victor Hugo" },
                    { 5, "Machado de Assis" }
                });

            migrationBuilder.InsertData(
                table: "Classificacao",
                columns: new[] { "id", "anos", "descricao" },
                values: new object[,]
                {
                    { 2, 18, "Conteúdo Adultos" },
                    { 1, 14, "Conteúdo para adolescentes" }
                });

            migrationBuilder.InsertData(
                table: "Cliente",
                columns: new[] { "id", "celular", "cidade", "cpf", "dataNascimento", "email", "endereco", "estado", "nome", "senha", "sexo" },
                values: new object[,]
                {
                    { 1, "(047)9 5555-7777", "Blumenau", "33344455533", new DateTime(1986, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Diego@hotmail.com", "Rua Legal Número 1720", "Santa Catarina", "Diego", "765432", "M" },
                    { 2, "(047)9 9999-3333", "São Paulo", "44455577744", new DateTime(2000, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anizio@Gmail.com", "Rua Bacana Número 1315", "São Paulo", "Anizio", "345678", "M" },
                    { 3, "(047)9 3333-2222", "Blumenau", "12332112323", new DateTime(1989, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lucian@hotmail.com", "Rua Incrível Número 1450", "Santa Catarina", "Lucian", "987543", "M" },
                    { 4, "(047)9 1111-4444", "Blumenau", "43223443254", new DateTime(1994, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maria@Gmail.com", "Rua Incrível Número 1453", "Santa Catarina", "Maria", "887744", "F" }
                });

            migrationBuilder.InsertData(
                table: "Genero",
                columns: new[] { "id", "descricao" },
                values: new object[,]
                {
                    { 7, "Suspense" },
                    { 9, "Fantasia" },
                    { 8, "Ficção" },
                    { 6, "Terror" },
                    { 2, "Romance" },
                    { 4, "Aventura" },
                    { 3, "Ação" },
                    { 1, "Comédia" },
                    { 5, "Sobrenatural" }
                });

            migrationBuilder.InsertData(
                table: "Marca",
                columns: new[] { "id", "descricao" },
                values: new object[,]
                {
                    { 1, "HarperCollins" },
                    { 2, "Suma" },
                    { 3, "Clássicos Zahar" },
                    { 4, "Martin Claret" }
                });

            migrationBuilder.InsertData(
                table: "Segmento",
                columns: new[] { "id", "descricao" },
                values: new object[,]
                {
                    { 2, "Mangás" },
                    { 1, "Livros" },
                    { 3, "HQs" }
                });

            migrationBuilder.InsertData(
                table: "Compra",
                columns: new[] { "id", "clienteId", "total" },
                values: new object[,]
                {
                    { 1, 1, 26.5 },
                    { 2, 1, 54.0 },
                    { 3, 2, 29.75 },
                    { 4, 2, 100.0 },
                    { 5, 3, 15.25 },
                    { 6, 3, 30.5 },
                    { 7, 4, 44.25 },
                    { 8, 4, 35.0 }
                });

            migrationBuilder.InsertData(
                table: "Livro",
                columns: new[] { "id", "autorId", "classificacaoId", "dataPublicacao", "edicao", "generoId", "imagemURL", "marcaId", "nome", "pagina", "segmentoId", "tipoCapa" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2020, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1.0, 7, "morteNoNilo.jpg", 1, "Morte no Nilo", 320, 1, "Dura" },
                    { 2, 2, 2, new DateTime(2017, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1.0, 6, "oIluminado.jpg", 2, "O Iluminado", 520, 1, "Dura" },
                    { 3, 3, 2, new DateTime(2020, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 3.0, 8, "oCondeDeMonteCristo.jpg", 3, "O Conde de Monte Cristo", 1376, 1, "Dura" },
                    { 4, 4, 2, new DateTime(2014, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1.0, 8, "osMiseraveis.jpg", 4, "Os Miseráveis", 1511, 1, "Dura" }
                });

            migrationBuilder.InsertData(
                table: "CompraItem",
                columns: new[] { "id", "compraId", "livroId", "quantidade", "valorUnitario" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 26.5 },
                    { 8, 8, 1, 1, 35.0 },
                    { 2, 2, 2, 1, 54.0 },
                    { 7, 7, 2, 1, 44.25 },
                    { 3, 3, 3, 1, 29.75 },
                    { 5, 5, 3, 1, 15.25 },
                    { 4, 4, 4, 1, 100.0 },
                    { 6, 6, 4, 1, 30.5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compra_clienteId",
                table: "Compra",
                column: "clienteId");

            migrationBuilder.CreateIndex(
                name: "IX_CompraItem_compraId",
                table: "CompraItem",
                column: "compraId");

            migrationBuilder.CreateIndex(
                name: "IX_CompraItem_livroId",
                table: "CompraItem",
                column: "livroId");

            migrationBuilder.CreateIndex(
                name: "IX_Livro_autorId",
                table: "Livro",
                column: "autorId");

            migrationBuilder.CreateIndex(
                name: "IX_Livro_classificacaoId",
                table: "Livro",
                column: "classificacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Livro_generoId",
                table: "Livro",
                column: "generoId");

            migrationBuilder.CreateIndex(
                name: "IX_Livro_marcaId",
                table: "Livro",
                column: "marcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Livro_segmentoId",
                table: "Livro",
                column: "segmentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrador");

            migrationBuilder.DropTable(
                name: "CompraItem");

            migrationBuilder.DropTable(
                name: "Compra");

            migrationBuilder.DropTable(
                name: "Livro");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Autor");

            migrationBuilder.DropTable(
                name: "Classificacao");

            migrationBuilder.DropTable(
                name: "Genero");

            migrationBuilder.DropTable(
                name: "Marca");

            migrationBuilder.DropTable(
                name: "Segmento");
        }
    }
}
