import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Compra } from '../models/Compra';

@Injectable({
  providedIn: 'root'
})
export class CompraService {

  urlBase = `${environment.urlApi}/Compra`

constructor(private http: HttpClient) { }

  obterTodos() : Observable<Compra[]>{
    return this.http.get<Compra[]>(this.urlBase);
  }

  salvar(compra: Compra){
    return this.http.post(this.urlBase, compra);
  }

  editar(compra: Compra){
    return this.http.put(`${this.urlBase}/id=${compra.id}`, compra);
  }

  deletar(id: number){
    return this.http.delete(`${this.urlBase}/id=${id}`);
  }
}
