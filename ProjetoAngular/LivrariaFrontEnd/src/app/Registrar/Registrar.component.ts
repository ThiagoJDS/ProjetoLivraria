import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-Registrar',
  templateUrl: './Registrar.component.html',
  styleUrls: ['./Registrar.component.css']
})
export class RegistrarComponent implements OnInit {

  form!: FormGroup;

  constructor(public fb: FormBuilder) { }

  get f(): any { return this.form.controls; }

  ngOnInit() {
    this.validation();
  }

  private validation(): void {

    this.form = this.fb.group({
      firstName: ['', Validators.required],
      email: ['',
        [Validators.required, Validators.email]
      ],
      userName: ['', Validators.required],
      senha: ['',
        [Validators.required, Validators.minLength(6)]
      ],
      confirmeSenha: ['', Validators.required]
    });
  }


}
