import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Marca } from '../models/Marca';

@Injectable({
  providedIn: 'root'
})
export class MarcaService {

  urlBase = `${environment.urlApi}/Marca`

constructor(private http: HttpClient) { }

  obterTodos() : Observable<Marca[]>{
    return this.http.get<Marca[]>(this.urlBase);
  }

  salvar(marca: Marca){
    return this.http.post(this.urlBase, marca);
  }

  editar(marca: Marca){
    return this.http.put(`${this.urlBase}/id=${marca.id}`, marca);
  }

  deletar(id: number){
    return this.http.delete(`${this.urlBase}/id=${id}`);
  }

}
