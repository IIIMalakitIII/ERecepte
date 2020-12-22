import { Component, EventEmitter, OnDestroy, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, Subject } from 'rxjs';
import { map, startWith, takeUntil } from 'rxjs/operators';
import { Role } from 'src/app/core/extension/role.enum';
import { configureToastr, toastrTitle } from 'src/app/core/helper';
import { ISelectInfo } from 'src/app/core/interfaces/select-info.interface';
import { IUser } from 'src/app/core/interfaces/user.interface';
import { AuthenticationService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent implements OnInit, OnDestroy {


  @Output() openSignIn = new EventEmitter<any>();

  selectedRole: FormControl = new FormControl(null, Validators.required);
  signUpForm: FormGroup;
  doctorForm: FormGroup;
  patientForm: FormGroup;
  pharmacyForm: FormGroup;

  filteredOptions: Observable<ISelectInfo[]>;
  loading = false;
  currentUser: IUser;
  roles = Role;
  rolesKeys: any;
  medicalInstitutions: ISelectInfo[] = [];

  private destroy$ = new Subject<void>();
  constructor(private formBuilder: FormBuilder,
              private router: Router,
              private authService: AuthenticationService,
              private toastr: ToastrService) {
    this.authService.currentUser.subscribe(x => this.currentUser = x);
    this.rolesKeys = Object.keys(this.roles);
  }

  ngOnInit(): void {
    if (this.currentUser){
      this.router.navigate(['/project']);
    }
    this.createForm();
    this.getMedicalInstitutions();
    configureToastr(this.toastr);
  }

  getMedicalInstitutions(): void {
    this.authService.getMedicalInstitutions()
      .pipe(takeUntil(this.destroy$))
      .subscribe(res => {
        this.medicalInstitutions = res;
        this.initFilter();
      });
  }

  onSubmit(): void {
    switch (this.selectedRole.value){
      case Role[Role.Patient]:
        if (this.signUpForm.valid && this.patientForm.valid) {
          this.createUserAccount(this.patientForm.value);
        } else {
          this.signUpForm.markAllAsTouched();
          this.patientForm.markAllAsTouched();
        }
        return;
      case Role[Role.Doctor] :
        if (this.signUpForm.valid && this.doctorForm.valid) {
          this.createUserAccount({
            medicalInstitutionId: this.doctorForm.get('medicalInstitutionId').value.id,
            recordingAvailable: true,
            firstName: this.doctorForm.get('firstName').value,
            lastName: this.doctorForm.get('lastName').value,
            license: this.doctorForm.get('license').value,
            phone: this.doctorForm.get('phone').value,
          });
        } else {
          this.signUpForm.markAllAsTouched();
          this.doctorForm.markAllAsTouched();
        }
        return;
      case Role[Role.Pharmacy] :
        if (this.signUpForm.valid && this.pharmacyForm.valid) {
          this.createUserAccount(this.pharmacyForm.value);
        } else {
          this.signUpForm.markAllAsTouched();
          this.pharmacyForm.markAllAsTouched();
        }
        return;
    }
  }

  displayFn(element: ISelectInfo): string {
    return element?.name;
  }

  createUserAccount(roleForm: any): void {
    this.authService.registration(
      {
        role: this.selectedRole.value,
        ...this.signUpForm.value,
        [this.selectedRole.value]: { ...roleForm }
      })
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        () => {
          this.clearForm();
          this.toastr.success(`Account created`, toastrTitle.Success);
          this.openSignIn.next(null);
        },
        () => {
          this.toastr.error(`Something is wrong`, toastrTitle.Error);
        }
      );
  }

  createForm(): void  {
    this.signUpForm = this.formBuilder.group({
      userName: [null, Validators.required],
      password: [null, Validators.required],
      email: [null, [Validators.required, Validators.email]],
    });

    this.pharmacyForm = this.formBuilder.group({
      address: [null, Validators.required],
      country: [null, Validators.required],
      city: [null, Validators.required],
    });

    this.doctorForm = this.formBuilder.group({
      medicalInstitutionId: [null, Validators.required],
      firstName: [null, Validators.required],
      lastName: [null, Validators.required],
      license: [null, Validators.required],
      phone: [null, Validators.required],
    });

    this.patientForm = this.formBuilder.group({
      firstName: [null, Validators.required],
      lastName: [null, Validators.required],
      country: [null, Validators.required],
      passport: [null, Validators.required],
      address: [null, Validators.required],
      phone: [null, Validators.required],
    });
  }

  private initFilter() {
    this.filteredOptions = this.doctorForm.get('medicalInstitutionId').valueChanges
    .pipe(
      startWith(null),
      map(state => state ? this._filterStates(state) : this.medicalInstitutions.slice())
    );
  }

  private _filterStates(value): any[] {
    const filterValue = value && value.name ? value.name.toLowerCase() : value?.toLowerCase();

    return this.medicalInstitutions.filter(element => element.name.toLowerCase().indexOf(filterValue) === 0);
  }

  clearForm(): void {
    this.selectedRole.reset();
    this.pharmacyForm.reset();
    this.doctorForm.reset();
    this.patientForm.reset();
    this.signUpForm.reset();
  }

  formValid(): boolean {
    switch (this.selectedRole.value){
      case Role[Role.Patient]:
        return this.signUpForm.valid && this.patientForm.valid;
      case Role[Role.Doctor] :
        return this.signUpForm.valid && this.doctorForm.valid;
      case Role[Role.Pharmacy] :
        return this.signUpForm.valid && this.pharmacyForm.valid;
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}

