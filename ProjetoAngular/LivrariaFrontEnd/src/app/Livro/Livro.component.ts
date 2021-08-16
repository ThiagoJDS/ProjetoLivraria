import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-Livro',
  templateUrl: './Livro.component.html',
  styleUrls: ['./Livro.component.css']
})
export class LivroComponent implements OnInit {

  public modalRef: BsModalRef = new BsModalRef();
  constructor(private modalService: BsModalService) {

  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  ngOnInit() {
  }

}
