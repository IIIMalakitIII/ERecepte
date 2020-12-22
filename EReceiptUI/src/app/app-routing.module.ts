import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './guard/auth.guard';
import { ControlsTestComponent } from './shared/components/controls-test/controls-test.component';


const routes: Routes = [
  {
    path: '',
    redirectTo: 'control',
    pathMatch: 'full'
  },
  {
    path: 'auth',
    loadChildren: () =>
      import('./features/auth/auth.module').then(m => m.AuthModule)
  },
  {
    path: 'record',
    loadChildren: () =>
      import('./features/record/record.module').then(m => m.RecordModule)
  },
  {
    path: 'receipt',
    loadChildren: () =>
      import('./features/receipt/receipt.module').then(m => m.ReceiptModule)
  },
  {
    path: 'admin',
    loadChildren: () =>
      import('./features/admin/admin.module').then(m => m.AdminModule)
  },
  {
    path: 'control', component: ControlsTestComponent,
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
