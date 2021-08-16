import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MenuDeCadastrosComponent } from './ComponentesDeCadastros/MenuDeCadastros/MenuDeCadastros.component';
import { AdministradorComponent } from './ComponentesDeCadastros/Administrador/Administrador.component';
import { AutorComponent } from './ComponentesDeCadastros/Autor/Autor.component';
import { ClassificacaoComponent } from './ComponentesDeCadastros/Classificacao/Classificacao.component';
import { ClienteComponent } from './ComponentesDeCadastros/Cliente/Cliente.component';
import { CLivroComponent } from './ComponentesDeCadastros/CLivro/CLivro.component';
import { CompraComponent } from './ComponentesDeCadastros/Compra/Compra.component';
import { CompraItemComponent } from './ComponentesDeCadastros/CompraItem/CompraItem.component';
import { GeneroComponent } from './ComponentesDeCadastros/Genero/Genero.component';
import { LivroComponent } from './Livro/Livro.component';
import { MarcaComponent } from './ComponentesDeCadastros/Marca/Marca.component';
import { PrincipalComponent } from './Principal/Principal.component';
import { SegmentoComponent } from './ComponentesDeCadastros/Segmento/Segmento.component';
import { LoginComponent } from './Login/Login.component';
import { RegistrarComponent } from './Registrar/Registrar.component';
import { CarrinhoComponent } from './Carrinho/Carrinho.component';

const routes: Routes = [
  { path: '', redirectTo: 'principal', pathMatch: 'full' },
  { path: 'livro', component: LivroComponent },
  { path: 'principal', component: PrincipalComponent },
  { path: 'menuDeCadastros', component: MenuDeCadastrosComponent },
  { path: 'autor', component: AutorComponent },
  { path: 'cliente', component: ClienteComponent },
  { path: 'administrador', component: AdministradorComponent },
  { path: 'classificacao', component: ClassificacaoComponent },
  { path: 'genero', component: GeneroComponent },
  { path: 'marca', component: MarcaComponent },
  { path: 'segmento', component: SegmentoComponent },
  { path: 'compra', component: CompraComponent },
  { path: 'clivro', component: CLivroComponent },
  { path: 'compraItem', component: CompraItemComponent },
  { path: 'login', component: LoginComponent },
  { path: 'registrar', component: RegistrarComponent },
  { path: 'carrinho', component: CarrinhoComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
