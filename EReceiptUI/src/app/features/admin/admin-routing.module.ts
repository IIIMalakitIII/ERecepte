import { CreateMedicamentComponent } from './create-medicament/create-medicament.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  { path: '', redirectTo: 'create-medicament' , pathMatch: 'prefix'},
  { path: 'create-medicament', component: CreateMedicamentComponent},
  { path: 'doctor-list', component: CreateMedicamentComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
