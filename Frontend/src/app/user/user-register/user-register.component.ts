import { FnParam } from '@angular/compiler/src/output/output_ast';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AlertifyService } from 'src/app/services/alertify.service';
import { UserForRegister } from 'src/app/model/user';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css']
})
export class UserRegisterComponent implements OnInit {

  registerationForm: FormGroup;
  user: any = {};
  userSubmitted: boolean;
  constructor(private fb: FormBuilder,
    private authService: AuthService,
    private alertify: AlertifyService) { }

  ngOnInit(): void {
    // this.registerationForm = new FormGroup({
    //   username : new FormControl(null, Validators.required),
    //   email : new FormControl(null, [Validators.required, Validators.email]),
    //   password : new FormControl(null, [Validators.required, Validators.minLength(8)]),
    //   cpassword : new FormControl(null, Validators.required),
    //   mobile : new FormControl(null, [Validators.required, Validators.maxLength(10)])
    // }, this.passwordMatchingValidator);
    this.createRegisterationForm();
  }

  createRegisterationForm() {
    this.registerationForm = this.fb.group({
      username: [null, Validators.required],
      email: [null, [Validators.required, Validators.email]],
      password: [null, [Validators.required, Validators.minLength(8)]],
      cpassword: [null, Validators.required],
      mobile: [null, [Validators.required, Validators.maxLength(10)]]
    }, { validators: this.passwordMatchingValidator })
  }

  passwordMatchingValidator(fg: FormGroup): Validators {
    return fg.get("password").value === fg.get("cpassword").value ? null : { notmatched: true }
  }

  onSubmit() {
    this.userSubmitted = true;
    if (this.registerationForm.valid) {
      // this.user = Object.assign(this.user, this.registerationForm.value);
      this.authService.registerUser(this.userData()).subscribe(
        () => {
          this.registerationForm.reset();
          this.userSubmitted = false;
          this.alertify.success("Congrats, you are successfully registered");
        }
      );
    } else {
      this.alertify.error("Kindly provide the required fields");
    }
  }

  onReset() {
    this.userSubmitted = false;
    this.registerationForm.reset();
  }

  userData(): UserForRegister {
    return this.user = {
      username: this.username.value,
      email: this.email.value,
      password: this.password.value,
      mobile: this.mobile.value
    };
  }

  get username() {
    return this.registerationForm.get('username') as FormControl;
  }

  get email() {
    return this.registerationForm.get('email') as FormControl;
  }

  get password() {
    return this.registerationForm.get('password') as FormControl;
  }

  get cpassword() {
    return this.registerationForm.get('cpassword') as FormControl;
  }

  get mobile() {
    return this.registerationForm.get('mobile') as FormControl;
  }
}
