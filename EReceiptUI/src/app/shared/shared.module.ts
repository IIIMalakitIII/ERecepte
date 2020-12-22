import { ControlsTestComponent } from './components/controls-test/controls-test.component';
import { ModuleWithProviders, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MaterialModule } from './material.module';
import { ToastrModule } from 'ngx-toastr';
import { DiseaseHistoryComponent } from './components/disease-history/disease-history.component';
import { InstructionComponent } from './components/instruction/instruction.component';
import { TranslateModule } from '@ngx-translate/core';


@NgModule({
  declarations: [ControlsTestComponent, DiseaseHistoryComponent, InstructionComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    ToastrModule.forRoot(),
    MaterialModule,
    TranslateModule
  ],
  exports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    MaterialModule,
    TranslateModule
  ]
})
export class SharedModule {
  static forRoot(): ModuleWithProviders<SharedModule> {
    return {
      ngModule: SharedModule,
      providers: []
    };
  }
}
