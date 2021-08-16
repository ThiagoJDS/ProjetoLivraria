import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Segmento } from '../models/Segmento';

@Injectable({
  providedIn: 'root'
})
export class SegmentoService {

  urlBase = `${environment.urlApi}/Segmento`

constructor(private http: HttpClient) { }

  obterTodos() : Observable<Segmento[]>{
    return this.http.get<Segmento[]>(this.urlBase);
  }

  salvar(segmento: Segmento){
    return this.http.post(this.urlBase, segmento);
  }

  editar(segmento: Segmento){
    return this.http.put(`${this.urlBase}/id=${segmento.id}`, segmento);
  }

  deletar(id: number){
    return this.http.delete(`${this.urlBase}/id=${id}`);
  }

}
