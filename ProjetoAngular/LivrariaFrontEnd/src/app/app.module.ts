import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import { NgxMaskModule } from 'ngx-mask';
import { NgxCurrencyModule } from "ngx-currency";
import { NgxPaginationModule } from 'ngx-pagination';
import { TooltipModule } from 'ngx-bootstrap/tooltip';

import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';

import { NgxSpinnerModule } from 'ngx-spinner';

import { PaginationModule } from 'ngx-bootstrap/pagination';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PrincipalComponent } from './Principal/Principal.component';
import { NavComponent } from './Nav/Nav.component';
import { CardComponent } from './Card/Card.component';
import { LivroComponent } from './Livro/Livro.component';
import { LancamentoComponent } from './Lancamento/Lancamento.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';
import { HttpClientModule } from '@angular/common/http';
import { MenuDeCadastrosComponent } from './ComponentesDeCadastros/MenuDeCadastros/MenuDeCadastros.component';
import { AutorComponent } from './ComponentesDeCadastros/Autor/Autor.component';
import { TituloComponent } from './titulo/titulo.component';
import { ClienteComponent } from './ComponentesDeCadastros/Cliente/Cliente.component';
import { AdministradorComponent } from './ComponentesDeCadastros/Administrador/Administrador.component';
import { ClassificacaoComponent } from './ComponentesDeCadastros/Classificacao/Classificacao.component';
import { GeneroComponent } from './ComponentesDeCadastros/Genero/Genero.component';
import { MarcaComponent } from './ComponentesDeCadastros/Marca/Marca.component';
import { SegmentoComponent } from './ComponentesDeCadastros/Segmento/Segmento.component';
import { CompraComponent } from './ComponentesDeCadastros/Compra/Compra.component';
import { CLivroComponent } from './ComponentesDeCadastros/CLivro/CLivro.component';
import { CompraItemComponent } from './ComponentesDeCadastros/CompraItem/CompraItem.component';
import { LoginComponent } from './Login/Login.component';
import { RegistrarComponent } from './Registrar/Registrar.component';
import { CarrinhoComponent } from './Carrinho/Carrinho.component';
import { DateTimeFormatPipe } from './helpers/DateTimeFormat.pipe';


defineLocale('pt-br', ptBrLocale);

@NgModule({
  declarations: [
    AppComponent,
      PrincipalComponent,
      NavComponent,
      CardComponent,
      LivroComponent,
      LancamentoComponent,
      AutorComponent,
      TituloComponent,
      ClienteComponent,
      AdministradorComponent,
      ClassificacaoComponent,
      GeneroComponent,
      MarcaComponent,
      SegmentoComponent,
      CompraComponent,
      CLivroComponent,
      CompraItemComponent,
      LoginComponent,
      RegistrarComponent,
      CarrinhoComponent,
      DateTimeFormatPipe,
      MenuDeCadastrosComponent
   ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
    ModalModule.forRoot(),
    ToastrModule.forRoot({
      timeOut: 3000,
      preventDuplicates: true,
      progressBar: true
    }),
    HttpClientModule,
    NgxMaskModule.forRoot(),
    NgxPaginationModule,
    NgxCurrencyModule,
    TooltipModule.forRoot(),
    BsDatepickerModule.forRoot(),
    NgxSpinnerModule,
    PaginationModule.forRoot(),
  ],
  providers: [],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
