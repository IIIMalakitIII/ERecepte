import { IMedicament } from './../../../core/interfaces/medicament.interface';
import { ReceiptService } from './../api/record.service';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable, Subject } from 'rxjs';
import { map, startWith, takeUntil } from 'rxjs/operators';
import { RecordStatus } from 'src/app/core/extension/record.enum';
import { IPatient } from 'src/app/core/interfaces/patient.interface';
import { IRecord } from 'src/app/core/interfaces/record.interface';
import { FormGroup, FormGroupDirective, FormControl, FormBuilder, Validators } from '@angular/forms';
import { MatAccordion } from '@angular/material/expansion';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ISelectInfo } from 'src/app/core/interfaces/select-info.interface';

@Component({
  selector: 'app-create-receipt',
  templateUrl: './create-receipt.component.html',
  styleUrls: ['./create-receipt.component.scss']
})
export class CreateReceiptComponent implements OnInit, OnDestroy {

  patient: IPatient;
  patientId: number;
  minDate = new Date();
  selectedMedicament: IMedicament;

  receipForm: FormGroup;

  @ViewChild(MatAccordion) accordion: MatAccordion;
  @ViewChild(FormGroupDirective) formDirective: FormGroupDirective;

  medicamentCategoryControl: FormControl = new FormControl(null);
  medicamentControl: FormControl = new FormControl(null);

  accordingOpened = false;
  recordStatus = RecordStatus;
  updateState = false;

  medicaments: IMedicament[] = [];
  medicamentCategories: ISelectInfo[] = [];
  medicamentsOptions: Observable<IMedicament[]>;
  medicamentCategoriesOptions: Observable<ISelectInfo[]>;

  private destroy$ = new Subject<void>();
  constructor(private formBuilder: FormBuilder,
              private router: Router,
              private route: ActivatedRoute,
              private receipService: ReceiptService,
              private toastr: ToastrService,
              public dialog: MatDialog) {
    this.patientId = +this.route.snapshot.params.patientId;
   }

  ngOnInit(): void {
    this.getMedicamentCategories();
    this.getPatientInfo();
    this.createForm();
    this.medicamentCategoryControl.valueChanges
      .pipe(takeUntil(this.destroy$))
      .subscribe(res => res && res?.name
        ? this.getMedicamentsByCategory(res.id)
        : this.clearFilterControls());
  }

  getPatientInfo(): void {
    this.receipService.getPatientInfo(this.patientId)
      .pipe(takeUntil(this.destroy$))
      .subscribe(res => {
        this.patient = res;
      });
  }

  getMedicamentCategories(): void {
    this.receipService.getMedicamentCategories()
      .pipe(takeUntil(this.destroy$))
      .subscribe(res => {
        this.medicamentCategories = res;
        this.initCategoryFilter();
      });
  }

  getMedicamentsByCategory(id: number): void {
    this.receipForm.get('medicamentId').reset();
    this.receipService.getMedicamentsByCategory(id)
      .pipe(takeUntil(this.destroy$),
      map(data => {
        data.forEach(x => {
          if (x.pictureByte) {
            x.pictureByte = `data:${x.contentType};base64,` + x.pictureByte;
          }
        });
        return data;
      }))
      .subscribe(res => {
        this.medicaments = res;
        this.initMedicamentFilters();
      });
  }

  createForm(): void {
    this.receipForm = this.formBuilder.group({
      id: 0,
      medicamentId: [null, Validators.required],
      dateStart: [null, Validators.required],
      dateEnd: [null, Validators.required],
    });
  }

  displayFn(element: ISelectInfo): string {
    return element?.name;
  }

  clearFilterControls(): void {
    this.receipForm.get('medicamentId').reset();
    this.medicamentControl.reset();
  }

  onSubmit(): void {
    if (this.receipForm.valid) {
      this.receipService.createReceipt({
        patientId: this.patientId,
        doctorId: 0,
        ...this.receipForm.value
      })
        .pipe(takeUntil(this.destroy$))
        .subscribe(res => {
          this.router.navigate(['receipt']);
        });

    } else {
      this.receipForm.markAllAsTouched();
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  private initCategoryFilter() {
    this.medicamentCategoriesOptions = this.medicamentCategoryControl.valueChanges
    .pipe(
      startWith(null),
      map(state => state ? this._filterCategory(state) : this.medicamentCategories.slice())
    );
  }

  private initMedicamentFilters() {
    this.medicamentsOptions = this.medicamentControl.valueChanges
    .pipe(
      startWith(null),
      map(state => state ? this._filterMedicament(state) : this.medicaments.slice())
    );
  }

  private _filterMedicament(value): any[] {
    const filterValue = value && value.name ? value.name.toLowerCase() : value?.toLowerCase();

    return this.medicaments.filter(element => element.name.toLowerCase().includes(filterValue));
  }

  private _filterCategory(value): any[] {
    const filterValue = value && value.name ? value.name.toLowerCase() : value?.toLowerCase();

    return this.medicamentCategories.filter(element => element.name.toLowerCase().includes(filterValue));
  }
}
