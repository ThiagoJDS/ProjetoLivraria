import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Cliente } from '../models/Cliente';
@Injectable({
  providedIn: 'root'
})
export class ClienteService {

  urlBase = `${environment.urlApi}/Cliente`

constructor(private http: HttpClient) { }

  obterTodos() : Observable<Cliente[]>{
    return this.http.get<Cliente[]>(this.urlBase);
  }

  salvar(cliente: Cliente){
    return this.http.post(this.urlBase, cliente);
  }

  editar(cliente: Cliente){
    return this.http.put(`${this.urlBase}/id=${cliente.id}`, cliente);
  }

  deletar(id: number){
    return this.http.delete(`${this.urlBase}/id=${id}`);
  }
}
