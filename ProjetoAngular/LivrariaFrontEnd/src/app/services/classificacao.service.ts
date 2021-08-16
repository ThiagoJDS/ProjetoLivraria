import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Classificacao } from '../models/Classificacao';
@Injectable({
  providedIn: 'root'
})
export class ClassificacaoService {

  urlBase = `${environment.urlApi}/Classificacao`

constructor(private http: HttpClient) { }

  obterTodos() : Observable<Classificacao[]>{
    return this.http.get<Classificacao[]>(this.urlBase);
  }

  salvar(classificacao: Classificacao){
    return this.http.post(this.urlBase, classificacao);
  }

  editar(classificacao: Classificacao){
    return this.http.put(`${this.urlBase}/id=${classificacao.id}`, classificacao);
  }

  deletar(id: number){
    return this.http.delete(`${this.urlBase}/id=${id}`);
  }
}
