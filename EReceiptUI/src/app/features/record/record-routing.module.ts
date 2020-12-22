import { DoctorRecordComponent } from './doctor-record/doctor-record.component';
import { CreateRecordComponent } from './create-record/create-record.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule, Router } from '@angular/router';
import { Role } from 'src/app/core/extension/role.enum';
import { AuthGuard } from 'src/app/guard/auth.guard';
import { RoleGuard } from 'src/app/guard/role-guard';

const routes: Routes = [
  { path: '', redirectTo: 'patient-records' , pathMatch: 'prefix'},
  { path: 'doctor-records', component: DoctorRecordComponent,  data: {roles: [Role.Doctor] }, canActivate: [AuthGuard, RoleGuard]},
  { path: 'patient-records', component: CreateRecordComponent, data: { roles: [Role.Patient], tryToRedirect: 'record/doctor-records' }, canActivate: [AuthGuard, RoleGuard]},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RecordRoutingModule {
}
