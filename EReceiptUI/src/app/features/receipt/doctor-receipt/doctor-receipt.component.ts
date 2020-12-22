import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ReceiptStatus, ReceiptStatusSystem } from 'src/app/core/extension/receipt-status.enum';
import { IReceipt } from 'src/app/core/interfaces/receip.interface';
import { InstructionComponent } from 'src/app/shared/components/instruction/instruction.component';
import { ReceiptService } from '../api/record.service';

@Component({
  selector: 'app-doctor-receipt',
  templateUrl: './doctor-receipt.component.html',
  styleUrls: ['./doctor-receipt.component.scss']
})
export class DoctorReceiptComponent implements OnInit, OnDestroy {
  patientReceipt: IReceipt[] = [];
  receiptStatus = ReceiptStatus;
  private destroy$ = new Subject<void>();
  constructor(private router: Router,
              private receipService: ReceiptService,
              private toastr: ToastrService,
              public dialog: MatDialog) {
   }

  ngOnInit(): void {
    this.getDoctorReceipts();
  }

  getDoctorReceipts(): void {
    this.receipService.getMyReceipts()
    .pipe(takeUntil(this.destroy$))
    .subscribe(res => {
      res.forEach(x => {
        x.dateEnd = new Date(x.dateEnd);
        x.dateStart = new Date(x.dateStart);
        if (x.medicament.pictureByte) {
          x.medicament.pictureByte = `data:${x.medicament.contentType};base64,` + x.medicament.pictureByte;
        }
      });
      this.patientReceipt = res;
    });
  }

  openCreateReceipt(patientid: number): void {
    this.router.navigate(['/receipt/create-receipt/', patientid ]);
  }

  blockReceipt(receiptId: number): void {
    this.receipService.updateReceiptStatus(receiptId, ReceiptStatusSystem.Blocked)
    .pipe(takeUntil(this.destroy$))
    .subscribe(() => this.getDoctorReceipts());
  }


  openInstruction(instruction: string): void {
    const config = new MatDialogConfig();
    config.panelClass = `modal-setting`;
    config.maxWidth = '50vw';
    config.maxHeight = '75vh';
    config.height = '100%';
    config.width = '100%';
    config.data = instruction;
    const dialogRef = this.dialog.open(InstructionComponent, config);
  }


  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
