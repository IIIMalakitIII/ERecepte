import { SharedModule } from 'src/app/shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { CreateMedicamentComponent } from './create-medicament/create-medicament.component';
import { MedicalInstituationComponent } from './medical-instituation/medical-instituation.component';
import { CreateMedicamentCategoryComponent } from './create-medicament-category/create-medicament-category.component';
import { CreateMedicamentManufacturerComponent } from './create-medicament-manufacturer/create-medicament-manufacturer.component';


@NgModule({
  declarations: [CreateMedicamentComponent, MedicalInstituationComponent, CreateMedicamentCategoryComponent, CreateMedicamentManufacturerComponent],
  imports: [
    CommonModule,
    AdminRoutingModule,
    SharedModule
  ]
})
export class AdminModule { }
