import { ISelectInfo } from './../../shared/components/controls-test/controls-test.component';
import { RecordStatus } from "../extension/record.enum";

export interface IRecord {
  id: number;
  recordingTime: Date;
  recordStatus: RecordStatus;
  description: string;
  doctorId: number;
  patientId: number;
  medicalInstitution: string;
  doctor: ISelectInfo;
  patient: ISelectInfo;
}
