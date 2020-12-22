import { IManufacturer } from './../../../core/interfaces/manufacture.interface';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormGroupDirective, FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Observable, Subject } from 'rxjs';
import { takeUntil, startWith, map } from 'rxjs/operators';
import { IMedicamentCategory } from 'src/app/core/interfaces/medicament-category.interface';
import { AdminService } from '../api/admin.service';

@Component({
  selector: 'app-create-medicament-manufacturer',
  templateUrl: './create-medicament-manufacturer.component.html',
  styleUrls: ['./create-medicament-manufacturer.component.scss']
})
export class CreateMedicamentManufacturerComponent implements OnInit, OnDestroy {


  @ViewChild(FormGroupDirective) formDirective: FormGroupDirective;

  manufactureForm: FormGroup;
  manufacturerControl: FormControl = new FormControl(null);

  updateState = false;
  manufacturers: IManufacturer[] = [];
  manufacturerOptions: Observable<IManufacturer[]>;

  private destroy$ = new Subject<void>();
  constructor(private formBuilder: FormBuilder,
              private adminService: AdminService,
              private toastr: ToastrService) {
    }

  ngOnInit(): void {
    this.createForm();
    this.getManufactures();
  }

  getManufactures(): void {
    this.adminService.getManufacturerAll()
      .pipe(takeUntil(this.destroy$))
      .subscribe(res => {
        this.manufacturers = res;
        this.initManufacturerFilter();
      });
  }

  createForm(): void {
    this.manufactureForm = this.formBuilder.group({
      id: 0,
      name: [null, Validators.required],
      license: [null, Validators.required],
      description: null
    });
  }

  displayFn(element: IManufacturer): string {
    return element?.name;
  }

  clearForm(): void {
    this.updateState = false;
    this.formDirective.resetForm();
    this.manufacturerControl.reset();
    this.manufactureForm.reset();
    this.manufactureForm.get('id').setValue(0);
  }

  onSubmit(): void {
    if (this.manufactureForm.valid) {
      this.updateState ? this.update() : this.create();
    } else {
      this.manufactureForm.markAllAsTouched();
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  create(): void {
    this.adminService.createManufacturer(this.manufactureForm.value)
      .pipe(takeUntil(this.destroy$))
      .subscribe(res => {
        this.clearForm();
        this.getManufactures();
      });
  }

  update(): void {
    this.adminService.updateManufacturer(this.manufactureForm.value)
      .pipe(takeUntil(this.destroy$))
      .subscribe(res => {
        this.clearForm();
        this.getManufactures();
      });
  }

  fillFormFoUpdate(category: IManufacturer): void {
    this.clearForm();
    this.updateState = true;
    this.manufactureForm.get('id').setValue(category.id);
    this.manufactureForm.get('name').setValue(category.name);
    this.manufactureForm.get('license').setValue(category.license);
    this.manufactureForm.get('description').setValue(category.description);
  }

  private initManufacturerFilter() {
    this.manufacturerOptions = this.manufacturerControl.valueChanges
    .pipe(
      startWith(null),
      map(state => state ? this._filterCategory(state) : this.manufacturers.slice())
    );
  }


  private _filterCategory(value): any[] {
    const filterValue = value && value.name ? value.name.toLowerCase() : value?.toLowerCase();

    return this.manufacturers.filter(element => element.name.toLowerCase().includes(filterValue));
  }

}
