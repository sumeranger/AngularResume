import { FnParam } from '@angular/compiler/src/output/output_ast';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css']
})
export class UserRegisterComponent implements OnInit {

  registerForm : FormGroup;
  constructor() { }

  ngOnInit(): void {
    this.registerForm = new FormGroup({
      username : new FormControl(null, Validators.required),
      email : new FormControl(null, [Validators.required, Validators.email]),
      password : new FormControl(null, [Validators.required, Validators.minLength(8)]),
      cpassword : new FormControl(null, Validators.required),
      mobile : new FormControl(null, [Validators.required, Validators.maxLength(10)])
    }, this.passwordMatchingValidator);
  }

  passwordMatchingValidator(fg:FormGroup) : Validators{
    return fg.get("password").value === fg.get("cpassword").value ? null : {notmatched: true}
  }

  get username(){
    return this.registerForm.get('username') as FormControl;
  }

  get email(){
    return this.registerForm.get('email') as FormControl;
  }

  get password(){
    return this.registerForm.get('password') as FormControl;
  }

  get cpassword(){
    return this.registerForm.get('cpassword') as FormControl;
  }

  get mobile(){
    return this.registerForm.get('mobile') as FormControl;
  }

  onSubmit(){
    console.log(this.registerForm);
  }

}
