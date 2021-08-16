import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Administrador } from '../models/Administrador';

@Injectable({
  providedIn: 'root'
})
export class AdministradorService {

  urlBase = `${environment.urlApi}/Administrador`

constructor(private http: HttpClient) { }

  obterTodos() : Observable<Administrador[]>{
    return this.http.get<Administrador[]>(this.urlBase);
  }

  salvar(administrador: Administrador){
    return this.http.post(this.urlBase, administrador);
  }

  editar(administrador: Administrador){
    return this.http.put(`${this.urlBase}/id=${administrador.id}`, administrador);
  }

  deletar(id: number){
    return this.http.delete(`${this.urlBase}/id=${id}`);
  }

}
