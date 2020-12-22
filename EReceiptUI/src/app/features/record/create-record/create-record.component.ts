import { element } from 'protractor';
import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl, FormGroupDirective } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatAccordion } from '@angular/material/expansion';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { startWith, map } from 'rxjs/operators';
import { RecordStatus, SystemRecordStatus } from 'src/app/core/extension/record.enum';
import { configureToastr, toastrTitle } from 'src/app/core/helper';
import { IRecord } from 'src/app/core/interfaces/record.interface';
import { ISelectInfo } from 'src/app/core/interfaces/select-info.interface';
import { RecordService } from '../api/record.service';

@Component({
  selector: 'app-create-record',
  templateUrl: './create-record.component.html',
  styleUrls: ['./create-record.component.scss']
})
export class CreateRecordComponent implements OnInit, OnDestroy {


  recordForm: FormGroup;
  @ViewChild(MatAccordion) accordion: MatAccordion;
  @ViewChild(FormGroupDirective) formDirective: FormGroupDirective;

  medicalInstitutionControl: FormControl = new FormControl(null);
  medicalInstitutions: ISelectInfo[] = [];
  doctors: ISelectInfo[] = [];
  patientRecords: IRecord[] = [];
  accordingOpened = false;
  recordStatus = RecordStatus;
  updateState = false;

  medicalInstitutionOptions: Observable<ISelectInfo[]>;
  doctorsOptions: Observable<ISelectInfo[]>;

  private destroy$ = new Subject<void>();
  constructor(private formBuilder: FormBuilder,
              private router: Router,
              private recordService: RecordService,
              private toastr: ToastrService,
              public dialog: MatDialog) { }

  ngOnInit(): void {
    this.createForm();
    this.getMedicalInstitutions();
    this.getPatientRecords();
    configureToastr(this.toastr);
    this.medicalInstitutionControl.valueChanges
      .pipe(takeUntil(this.destroy$))
      .subscribe(res => res && res?.name ? this.getDoctorByMedicalInstitution(res.id) : this.recordForm.get('doctorId').reset());
  }

  getMedicalInstitutions(): void {
    this.recordService.getMedicalInstitutions()
      .pipe(takeUntil(this.destroy$))
      .subscribe(res => {
        this.medicalInstitutions = res;
        this.initFilter();
      });
  }

  getDoctorByMedicalInstitution(id: number): void {
    this.recordForm.get('doctorId').reset();
    this.recordService.getDoctorByMedicalInstitution(id)
      .pipe(takeUntil(this.destroy$))
      .subscribe(res => {
        this.doctors = res;
        this.initDoctorFilters();
      });
  }

  getPatientRecords(): void {
    this.recordService.getPatientRecords()
      .pipe(takeUntil(this.destroy$))
      .subscribe(res => {
        res.forEach(x => x.recordingTime = new Date(x.recordingTime));
        this.patientRecords = res;
      });
  }

  onSubmit(): void {
    if (this.recordForm.valid) {
      this.updateState
      ? this.update()
      : this.create();
    } else {
      this.recordForm.markAllAsTouched();
    }
  }

  update(): void {
    this.recordService.updateRecord({
      id: this.recordForm.get('id').value,
      doctorId:  this.recordForm.get('doctorId').value.id,
      description: this.recordForm.get('description').value,
    })
    .pipe(takeUntil(this.destroy$))
    .subscribe(
      () => {
        this.clearForm();
        this.getPatientRecords();
        this.toastr.success(`Record Created`, toastrTitle.Success);
      },
      () => {
        this.getPatientRecords();
        this.toastr.error(`Something is wrong`, toastrTitle.Error);
      });
  }

  create(): void {
    this.recordService.createRecord({
      doctorId:  this.recordForm.get('doctorId').value.id,
      description: this.recordForm.get('description').value,
    })
    .pipe(takeUntil(this.destroy$))
    .subscribe(
      () => {
        this.clearForm();
        this.getPatientRecords();
        this.toastr.success(`Record Created`, toastrTitle.Success);
      },
      () => {
        this.getPatientRecords();
        this.toastr.error(`Something is wrong`, toastrTitle.Error);
      });
  }

  createForm(): void  {
    this.recordForm = this.formBuilder.group({
      id: null,
      doctorId: [null, Validators.required],
      description: [null, Validators.required],
    });
  }

  displayFn(element: ISelectInfo): string {
    return element?.name;
  }

  updateRecord(element: IRecord): void {
    this.updateState = true;
    const findValue = this.medicalInstitutions.find(x => x.name === element.medicalInstitution);
    this.medicalInstitutionControl.setValue(findValue, { onlySelf: true , emitEvent: true });
    this.recordForm.get('description').setValue(element.description);
    this.recordForm.get('doctorId').setValue(element.doctor);
    this.recordForm.get('id').setValue(element.id);
    window.scrollTo({top: 0, behavior: 'smooth'});
  }

  clearForm(): void {
    this.updateState = false;
    this.formDirective.resetForm();
    this.recordForm.reset();
    this.medicalInstitutionControl.reset();
  }

  cancelRecordLikePatient(id: number): void {
    this.recordService.cancelRecordLikePatient(SystemRecordStatus.RejectedByPatient, id)
    .pipe(takeUntil(this.destroy$))
    .subscribe(
        () => {
          this.getPatientRecords();
          this.toastr.success(`Record Canceled`, toastrTitle.Success);
        },
        () => {
          this.getPatientRecords();
          this.toastr.error(`Something is wrong`, toastrTitle.Error);
        });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }


  private initFilter() {
    this.medicalInstitutionOptions = this.medicalInstitutionControl.valueChanges
    .pipe(
      startWith(null),
      map(state => state ? this._filterStates(state) : this.medicalInstitutions.slice())
    );
  }

  private initDoctorFilters() {
    this.doctorsOptions = this.recordForm.get('doctorId').valueChanges
    .pipe(
      startWith(null),
      map(state => state ? this._filterDoctors(state) : this.doctors.slice())
    );
  }

  private _filterDoctors(value): any[] {
    const filterValue = value && value.name ? value.name.toLowerCase() : value?.toLowerCase();

    return this.doctors.filter(element => element.name.toLowerCase().indexOf(filterValue) === 0);
  }

  private _filterStates(value): any[] {
    const filterValue = value && value.name ? value.name.toLowerCase() : value?.toLowerCase();

    return this.medicalInstitutions.filter(element => element.name.toLowerCase().indexOf(filterValue) === 0);
  }

}
