import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RecordRoutingModule } from './record-routing.module';
import { CreateRecordComponent } from './create-record/create-record.component';
import { DoctorRecordComponent } from './doctor-record/doctor-record.component';


@NgModule({
  declarations: [CreateRecordComponent, DoctorRecordComponent],
  imports: [
    CommonModule,
    RecordRoutingModule,
    SharedModule
  ]
})
export class RecordModule { }
