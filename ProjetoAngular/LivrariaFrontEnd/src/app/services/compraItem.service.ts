import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CompraItem } from '../models/CompraItem';

@Injectable({
  providedIn: 'root'
})
export class CompraItemService {

  urlBase = `${environment.urlApi}/CompraItem`

constructor(private http: HttpClient) { }

  obterTodos() : Observable<CompraItem[]>{
    return this.http.get<CompraItem[]>(this.urlBase);
  }

  salvar(compraItem: CompraItem){
    return this.http.post(this.urlBase, compraItem);
  }

  editar(compraItem: CompraItem){
    return this.http.put(`${this.urlBase}/id=${compraItem.id}`, compraItem);
  }

  deletar(id: number){
    return this.http.delete(`${this.urlBase}/id=${id}`);
  }
}
