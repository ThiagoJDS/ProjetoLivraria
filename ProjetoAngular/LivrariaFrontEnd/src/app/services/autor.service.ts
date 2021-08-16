import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Autor } from '../models/Autor';

@Injectable({
  providedIn: 'root'
})
export class AutorService {

  urlBase = `${environment.urlApi}/Autor`

constructor(private http: HttpClient) { }


  obterTodos() : Observable<Autor[]>{
    return this.http.get<Autor[]>(this.urlBase);
  }

  ObterPeloAutorId(id: number): Observable<Autor> {
    return this.http.get<Autor>(`${this.urlBase}/${id}`);
  }

  salvar(autor: Autor){
    return this.http.post(this.urlBase, autor);
  }

  editar(autor: Autor){
    return this.http.put(`${this.urlBase}/id=${autor.id}`, autor);
  }

  deletar(id: number){
    return this.http.delete(`${this.urlBase}/${id}`);
  }
}
