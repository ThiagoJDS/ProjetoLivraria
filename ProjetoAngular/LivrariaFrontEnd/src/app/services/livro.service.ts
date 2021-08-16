import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Livro } from '../models/Livro';

@Injectable({
  providedIn: 'root'
})
export class LivroService {

  urlBase = `${environment.urlApi}/Livro`

constructor(private http: HttpClient) { }

  obterTodos() : Observable<Livro[]>{
    return this.http.get<Livro[]>(this.urlBase);
  }

  salvar(livro: Livro){
    return this.http.post(this.urlBase, livro);
  }

  editar(livro: Livro){
    return this.http.put(`${this.urlBase}/id=${livro.id}`, livro);
  }

  deletar(id: number){
    return this.http.delete(`${this.urlBase}/id=${id}`);
  }
}
