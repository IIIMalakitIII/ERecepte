import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { finalize, first } from 'rxjs/operators';
import { configureToastr, toastrTitle } from 'src/app/core/helper';
import { AuthenticationService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent implements OnInit, OnDestroy {

  loginForm: FormGroup;
  loading = false;
  returnUrl: string;

  private destroy$ = new Subject<void>();
  constructor(private formBuilder: FormBuilder,
              private route: ActivatedRoute,
              private router: Router,
              private authenticationService: AuthenticationService,
              public toastr: ToastrService) {
    if (this.authenticationService.currentUserValue) {
      this.router.navigate(['/']);
    }
  }

  ngOnInit() {
    configureToastr(this.toastr);
    this.generateForm();
    this.returnUrl = this.route.snapshot.queryParams.returnUrl || '/';
  }

  generateForm() {
    this.loginForm = this.formBuilder.group(
      {
        email: ['', [Validators.required, Validators.email]],
        password: ['', Validators.required]
      }
    );
  }

  clearForm(): void {
    this.loginForm.reset();
  }

  get fields() { return this.loginForm.controls; }

  onSubmit() {
    if (this.loginForm.valid) {
    this.loading = true;
    this.authenticationService.login(this.loginForm)
    .pipe(
      finalize(() => {
        this.loading = false;
      }),
      first())
    .subscribe(
        () => {
          this.toastr.success(`You are logged in!`, toastrTitle.Success);
          this.router.navigate(['/']);
        },
        () => {
          this.toastr.error(`Something is wrong`, toastrTitle.Error);
        });
    } else {
      this.loginForm.markAllAsTouched();
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
