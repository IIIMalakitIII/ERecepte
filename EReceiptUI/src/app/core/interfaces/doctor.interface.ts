import { IMedicalInstitution } from "./medical-Institution.interface";
import { IRecord } from "./record.interface";

export interface IDoctor {
  id: number;
  userId: string;
  medicalInstitutionId: number;
  recordingAvailable: boolean;
  firstName: string;
  lastName: string;
  license: string;
  phone: string;
  records: IRecord[];
  // receipts: Receipt[];
  medicalInstitution: IMedicalInstitution;
}
