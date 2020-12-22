import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { IRecord } from 'src/app/core/interfaces/record.interface';
import { IPatient } from './../../../core/interfaces/patient.interface';
import { Component, Inject, OnInit, OnDestroy } from '@angular/core';
import { IDiseaseHistory } from 'src/app/core/interfaces/disease-history.interface';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { RecordService } from 'src/app/features/record/api/record.service';
import { IReceipt } from 'src/app/core/interfaces/receip.interface';
import { RecordStatus } from 'src/app/core/extension/record.enum';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-disease-history',
  templateUrl: './disease-history.component.html',
  styleUrls: ['./disease-history.component.scss']
})
export class DiseaseHistoryComponent implements OnInit, OnDestroy {

  patient: IPatient;
  diseaseHistore: IDiseaseHistory;
  recordStatus = RecordStatus;
  patientRecords: IRecord[] = [];
  patientReceipt: IReceipt[] = [];
  private destroy$ = new Subject<void>();
  constructor(@Inject(MAT_DIALOG_DATA) public data: number, private recordService: RecordService) { }

  ngOnInit(): void {
    this.getPatientInfo();
    this.getDiseaseHistoryInfo();
    this.getpatientRecordsInfo();
    this.getPatientReceipts();
  }

  getPatientInfo(): void {
    this.recordService.getPatientInfo(this.data)
      .pipe(takeUntil(this.destroy$))
      .subscribe(res => {
        this.patient = res;
      });
  }

  getDiseaseHistoryInfo(): void {
    this.recordService.getDiseaseHistoryInfo(this.data)
      .pipe(takeUntil(this.destroy$))
      .subscribe(res => {
        if (res) {
          this.diseaseHistore = res;
        } else {
          this.diseaseHistore = {
            id: 0,
            patientId: this.data,
            description: '',
            patient: null
          };
        }
      });
  }

  getpatientRecordsInfo(): void {
    this.recordService.getPatientRecordsById(this.data)
      .pipe(takeUntil(this.destroy$))
      .subscribe(res => {
        res.forEach(x => x.recordingTime = new Date(x.recordingTime));
        this.patientRecords = res;
      });
  }

  getPatientReceipts(): void {
    this.recordService.getPatientReceiptsById(this.data)
    .pipe(takeUntil(this.destroy$))
    .subscribe(res => {
      res.forEach(x => {
        x.dateEnd = new Date(x.dateEnd);
        x.dateStart = new Date(x.dateStart);
      });
      this.patientReceipt = res;
    });
  }

  updateDiseaseHistory(): void {
    if (this.diseaseHistore.id === 0) {
      this.recordService.createDiseaseHistory(this.diseaseHistore)
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        this.getDiseaseHistoryInfo();
      });
    } else {
      this.recordService.updateDiseaseHistory(this.diseaseHistore)
      .pipe(takeUntil(this.destroy$))
      .subscribe();
    }

  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

}
