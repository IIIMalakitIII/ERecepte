import { DiseaseHistoryComponent } from './../../../shared/components/disease-history/disease-history.component';
import { element } from 'protractor';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { RecordStatus, SystemRecordStatus } from 'src/app/core/extension/record.enum';
import { toastrTitle } from 'src/app/core/helper';
import { IRecord } from 'src/app/core/interfaces/record.interface';
import { RecordService } from '../api/record.service';

@Component({
  selector: 'app-doctor-record',
  templateUrl: './doctor-record.component.html',
  styleUrls: ['./doctor-record.component.scss']
})
export class DoctorRecordComponent implements OnInit, OnDestroy {

  doctorRecords: IRecord[] = [];
  recordStatus = RecordStatus;
  recordStatusKeys: string[];

  private destroy$ = new Subject<void>();
  constructor(private formBuilder: FormBuilder,
              private router: Router,
              private recordService: RecordService,
              private toastr: ToastrService,
              public dialog: MatDialog) {

    this.recordStatusKeys = Object.keys(this.recordStatus);
  }

  ngOnInit(): void {
    this.getDoctorRecords();
  }

  getDoctorRecords(): void {
    this.recordService.getDoctorRecords()
      .pipe(takeUntil(this.destroy$))
      .subscribe(res => {
        res.forEach(x => x.recordingTime = new Date(x.recordingTime));
        this.doctorRecords = res;
      });
  }

  updateRecordLikeDoctor(element: IRecord): void {
    console.log(element);
    this.recordService.updateRecordLikeDoctor(element.recordStatus, element.id)
    .pipe(takeUntil(this.destroy$))
    .subscribe(
        () => {
          this.getDoctorRecords();
          this.toastr.success(`Record Canceled`, toastrTitle.Success);
        },
        () => {
          this.getDoctorRecords();
          this.toastr.error(`Something is wrong`, toastrTitle.Error);
        });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  openDiseaseHistory(patientId: number) {
    const config = new MatDialogConfig();
    config.panelClass = `modal-setting`;
    config.maxWidth = '100vw';
    config.maxHeight = '100vh';
    config.height = '100%';
    config.width = '100%';
    config.disableClose = true;
    config.data = patientId;
    const dialogRef = this.dialog.open(DiseaseHistoryComponent, config);
  }

  createReceipt(patientid: number, recordid: number): void {
    this.router.navigate(['/receipt/create-receipt/', patientid ]);
  }
}
