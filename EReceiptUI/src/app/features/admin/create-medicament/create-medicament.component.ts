import { CreateMedicamentManufacturerComponent } from './../create-medicament-manufacturer/create-medicament-manufacturer.component';
import { CreateMedicamentCategoryComponent } from './../create-medicament-category/create-medicament-category.component';
import { IMedicament } from './../../../core/interfaces/medicament.interface';
import { AdminService } from './../api/admin.service';
import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { FormGroup, FormGroupDirective, FormBuilder, Validators, FormControl } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatAccordion } from '@angular/material/expansion';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, Subject } from 'rxjs';
import { takeUntil, map, startWith } from 'rxjs/operators';
import { ISelectInfo } from 'src/app/core/interfaces/select-info.interface';
import { ElementRef } from '@angular/core';
import { InstructionComponent } from 'src/app/shared/components/instruction/instruction.component';


@Component({
  selector: 'app-create-medicament',
  templateUrl: './create-medicament.component.html',
  styleUrls: ['./create-medicament.component.scss']
})
export class CreateMedicamentComponent implements OnInit, OnDestroy {

  @ViewChild('audioFile')  audioFile: ElementRef;
  medicamentForm: FormGroup;
  file: any;

  @ViewChild(MatAccordion) accordion: MatAccordion;
  @ViewChild(FormGroupDirective) formDirective: FormGroupDirective;

  medicamentControl: FormControl = new FormControl(null);

  updateState = false;
  manufacturers: ISelectInfo[] = [];
  medicaments: IMedicament[] = [];
  medicamentCategories: ISelectInfo[] = [];
  manufacturerOptions: Observable<ISelectInfo[]>;
  medicamentsOptions: Observable<IMedicament[]>;
  medicamentCategoriesOptions: Observable<ISelectInfo[]>;

  private destroy$ = new Subject<void>();
  constructor(private formBuilder: FormBuilder,
              private router: Router,
              private route: ActivatedRoute,
              private adminService: AdminService,
              private toastr: ToastrService,
              public dialog: MatDialog) {
   }

  ngOnInit(): void {
    this.createForm();
    this.getMedicaments()
    this.getMedicamentCategories();
    this.getManufacturers();
  }

  getMedicamentCategories(): void {
    this.adminService.getMedicamentCategories()
      .pipe(takeUntil(this.destroy$))
      .subscribe(res => {
        this.medicamentCategories = res;
        this.initCategoryFilter();
      });
  }

  getManufacturers(): void {
    this.adminService.getManufacturers()
      .pipe(takeUntil(this.destroy$))
      .subscribe(res => {
        this.manufacturers = res;
        this.initManufacturerFilter();
      });
  }

  getMedicaments(): void {
    this.adminService.getMedicaments()
    .pipe(
      takeUntil(this.destroy$),
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
    this.medicamentForm = this.formBuilder.group({
      id: 0,
      name: [null, Validators.required],
      categoryId: [null, Validators.required],
      manufacturerId: [null, Validators.required],
      instruction: [null, Validators.required],
      description: null,
      picture: null
    });
  }

  onUpload(inputFiles): void {
    this.file = inputFiles.target.files[0];
  }

  displayFn(element: ISelectInfo): string {
    return element?.name;
  }

  clearFilterControls(): void {
    this.medicamentForm.reset();
  }

  clearForm(): void {
    this.updateState = false;
    this.audioFile.nativeElement.value = null;
    this.formDirective.resetForm();
    this.medicamentControl.reset();
    this.medicamentForm.reset();
    this.file = null;
    this.medicamentForm.get('id').setValue(0);
  }

  onSubmit(): void {
    if (this.medicamentForm.valid) {
      this.updateState ? this.update() : this.create();
    } else {
      this.medicamentForm.markAllAsTouched();
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  create(): void {
    const createProject = new FormData();
    createProject.append('categoryId', this.medicamentForm.get('categoryId').value.id);
    createProject.append('manufacturerId', this.medicamentForm.get('manufacturerId').value.id);
    createProject.append('id', this.medicamentForm.get('id').value);
    createProject.append('name', this.medicamentForm.get('name').value);
    createProject.append('instruction', this.medicamentForm.get('instruction').value);
    createProject.append('description', this.medicamentForm.get('description').value);
    createProject.append('picture', this.file);
    this.adminService.createManufacture(createProject)
      .pipe(takeUntil(this.destroy$))
      .subscribe(res => {
        this.clearForm();
        this.getMedicaments();
      });
  }

  update(): void {
    const createProject = new FormData();
    createProject.append('categoryId', this.medicamentForm.get('categoryId').value.id);
    createProject.append('manufacturerId', this.medicamentForm.get('manufacturerId').value.id);
    createProject.append('id', this.medicamentForm.get('id').value);
    createProject.append('name', this.medicamentForm.get('name').value);
    createProject.append('instruction', this.medicamentForm.get('instruction').value);
    createProject.append('description', this.medicamentForm.get('description').value);
    createProject.append('picture', this.file);
    this.adminService.updateManufacture(createProject)
      .pipe(takeUntil(this.destroy$))
      .subscribe(res => {
        this.clearForm();
        this.getMedicaments();
      });
  }

  fillFormFoUpdate(medicament: IMedicament): void {
    console.log(medicament);
    this.clearForm();
    this.updateState = true;
    this.medicamentForm.get('id').setValue(medicament.id);
    this.medicamentForm.get('name').setValue(medicament.name);
    this.medicamentForm.get('categoryId').setValue(medicament.medicamentCategory, { onlySelf: true , emitEvent: true });
    this.medicamentForm.get('manufacturerId').setValue({id: medicament.manufacturerId, name: medicament.manufacturerName}, { onlySelf: true , emitEvent: true });
    this.medicamentForm.get('instruction').setValue(medicament.instruction);
    this.medicamentForm.get('description').setValue(medicament.description);
  }

  openInstruction(instruction: string): void {
    const config = new MatDialogConfig();
    config.panelClass = `modal-setting`;
    config.maxWidth = '50vw';
    config.maxHeight = '75vh';
    config.height = '100%';
    config.width = '100%';
    config.data = instruction;
    const dialogRef = this.dialog.open(InstructionComponent, config);
  }

  openCreateCategory(): void {
    const config = new MatDialogConfig();
    config.panelClass = `modal-setting`;
    config.maxWidth = '50vw';
    config.maxHeight = '75vh';
    config.height = '100%';
    config.width = '100%';
    const dialogRef = this.dialog.open(CreateMedicamentCategoryComponent, config);

    dialogRef.afterClosed()
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        this.medicamentForm.get('categoryId').reset();
        this.getMedicamentCategories()
      });
  }

  openCreateManufacturer(): void {
    const config = new MatDialogConfig();
    config.panelClass = `modal-setting`;
    config.maxWidth = '75vw';
    config.maxHeight = '75vh';
    config.height = '100%';
    config.width = '100%';
    const dialogRef = this.dialog.open(CreateMedicamentManufacturerComponent, config);

    dialogRef.afterClosed()
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        this.medicamentForm.get('manufacturerId').reset();
        this.getManufacturers();
      });
  }

  private initCategoryFilter() {
    this.medicamentCategoriesOptions = this.medicamentForm.get('categoryId').valueChanges
    .pipe(
      startWith(null),
      map(state => state ? this._filterCategory(state) : this.medicamentCategories.slice())
    );
  }

  private initManufacturerFilter() {
    this.manufacturerOptions = this.medicamentForm.get('manufacturerId').valueChanges
    .pipe(
      startWith(null),
      map(state => state ? this._filterManufacture(state) : this.manufacturers.slice())
    );
  }

  private _filterManufacture(value): any[] {
    const filterValue = value && value.name ? value.name.toLowerCase() : value?.toLowerCase();

    return this.manufacturers.filter(element => element.name.toLowerCase().includes(filterValue));
  }

  private _filterCategory(value): any[] {
    const filterValue = value && value.name ? value.name.toLowerCase() : value?.toLowerCase();

    return this.medicamentCategories.filter(element => element.name.toLowerCase().includes(filterValue));
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
}
