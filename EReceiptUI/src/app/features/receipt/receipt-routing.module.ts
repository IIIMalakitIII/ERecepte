import { CreateReceiptComponent } from './create-receipt/create-receipt.component';
import { PatientReceiptComponent } from './patient-receipt/patient-receipt.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Role } from 'src/app/core/extension/role.enum';
import { AuthGuard } from 'src/app/guard/auth.guard';
import { RoleGuard } from 'src/app/guard/role-guard';
import { DoctorReceiptComponent } from './doctor-receipt/doctor-receipt.component';


const routes: Routes = [
  { path: '', redirectTo: 'patient-receipts' , pathMatch: 'prefix'},
  { path: 'doctor-receipts', component: DoctorReceiptComponent,  data: {roles: [Role.Doctor] }, canActivate: [AuthGuard, RoleGuard]},
  { path: 'patient-receipts', component: PatientReceiptComponent,
      data: { roles: [Role.Patient], tryToRedirect: 'receipt/doctor-receipts' }, canActivate: [AuthGuard, RoleGuard]},
  { path: 'create-receipt/:patientId', component: CreateReceiptComponent,
      data: {roles: [Role.Doctor] }, canActivate: [AuthGuard, RoleGuard]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReceiptRoutingModule { }
