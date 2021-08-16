import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Genero } from '../models/Genero';

@Injectable({
  providedIn: 'root'
})
export class GeneroService {

  urlBase = `${environment.urlApi}/Genero`

constructor(private http: HttpClient) { }

  obterTodos() : Observable<Genero[]>{
    return this.http.get<Genero[]>(this.urlBase);
  }

  salvar(genero: Genero){
    return this.http.post(this.urlBase, genero);
  }

  editar(genero: Genero){
    return this.http.put(`${this.urlBase}/id=${genero.id}`, genero);
  }

  deletar(id: number){
    return this.http.delete(`${this.urlBase}/id=${id}`);
  }

}
