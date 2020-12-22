import { IMedicamentCategory } from './../../../core/interfaces/medicament-category.interface';
import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { FormBuilder, FormControl, FormGroup, FormGroupDirective, Validators } from '@angular/forms';
import { AdminService } from '../api/admin.service';
import { ToastrService } from 'ngx-toastr';
import { map, startWith, takeUntil } from 'rxjs/operators';


@Component({
  selector: 'app-create-medicament-category',
  templateUrl: './create-medicament-category.component.html',
  styleUrls: ['./create-medicament-category.component.scss']
})
export class CreateMedicamentCategoryComponent implements OnInit, OnDestroy {


  @ViewChild(FormGroupDirective) formDirective: FormGroupDirective;

  medicamentCategoryForm: FormGroup;
  medicamentCategoryControl: FormControl = new FormControl(null);

  updateState = false;
  medicamentCategories: IMedicamentCategory[] = [];
  medicamentCategoriesOptions: Observable<IMedicamentCategory[]>;

  private destroy$ = new Subject<void>();
  constructor(private formBuilder: FormBuilder,
              private adminService: AdminService,
              private toastr: ToastrService) {
   }

  ngOnInit(): void {
    this.createForm();
    this.getMedicamentCategories();
  }

  getMedicamentCategories(): void {
    this.adminService.getMedicamentCategories()
      .pipe(takeUntil(this.destroy$))
      .subscribe(res => {
        this.medicamentCategories = res;
        this.initCategoryFilter();
      });
  }

  createForm(): void {
    this.medicamentCategoryForm = this.formBuilder.group({
      id: 0,
      name: [null, Validators.required],
    });
  }

  displayFn(element: IMedicamentCategory): string {
    return element?.name;
  }

  clearForm(): void {
    this.updateState = false;
    this.formDirective.resetForm();
    this.medicamentCategoryControl.reset();
    this.medicamentCategoryForm.reset();
    this.medicamentCategoryForm.get('id').setValue(0);
  }

  onSubmit(): void {
    if (this.medicamentCategoryForm.valid) {
      this.updateState ? this.update() : this.create();
    } else {
      this.medicamentCategoryForm.markAllAsTouched();
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  create(): void {
    this.adminService.createMedicamentCategory(this.medicamentCategoryForm.value)
      .pipe(takeUntil(this.destroy$))
      .subscribe(res => {
        this.clearForm();
        this.getMedicamentCategories();
      });
  }

  update(): void {
    this.adminService.updateMedicamentCategory(this.medicamentCategoryForm.value)
      .pipe(takeUntil(this.destroy$))
      .subscribe(res => {
        this.clearForm();
        this.getMedicamentCategories();
      });
  }

  fillFormFoUpdate(category: IMedicamentCategory): void {
    this.clearForm();
    this.updateState = true;
    this.medicamentCategoryForm.get('id').setValue(category.id);
    this.medicamentCategoryForm.get('name').setValue(category.name);
  }

  private initCategoryFilter() {
    this.medicamentCategoriesOptions = this.medicamentCategoryControl.valueChanges
    .pipe(
      startWith(null),
      map(state => state ? this._filterCategory(state) : this.medicamentCategories.slice())
    );
  }


  private _filterCategory(value): any[] {
    const filterValue = value && value.name ? value.name.toLowerCase() : value?.toLowerCase();

    return this.medicamentCategories.filter(element => element.name.toLowerCase().includes(filterValue));
  }

}
