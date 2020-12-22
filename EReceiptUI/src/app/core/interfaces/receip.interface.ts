import { IMedicament } from './medicament.interface';
import { ReceiptStatus } from './../extension/receipt-status.enum';
import { ISelectInfo } from 'src/app/core/interfaces/select-info.interface';

export interface IReceipt {
  id: number;
  doctorId: number;
  patientId: number;
  medicamentId: number;
  dateStart: Date;
  dateEnd: Date;
  receiptStatus?: ReceiptStatus;
  doctor?: ISelectInfo;
  patient?: ISelectInfo;
  medicament?: IMedicament;
  medicamentCategory?: ISelectInfo;
  medicalInstitution?: string;
}
