import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReceiptRoutingModule } from './receipt-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { CreateReceiptComponent } from './create-receipt/create-receipt.component';
import { DoctorReceiptComponent } from './doctor-receipt/doctor-receipt.component';
import { PatientReceiptComponent } from './patient-receipt/patient-receipt.component';


@NgModule({
  declarations: [CreateReceiptComponent, DoctorReceiptComponent, PatientReceiptComponent],
  imports: [
    CommonModule,
    ReceiptRoutingModule,
    SharedModule
  ]
})
export class ReceiptModule { }
