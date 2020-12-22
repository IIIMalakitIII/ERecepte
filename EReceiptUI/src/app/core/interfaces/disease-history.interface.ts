import { ISelectInfo } from 'src/app/core/interfaces/select-info.interface';
export interface IDiseaseHistory {
  id: number;
  patientId: number;
  description: string;
  patient: ISelectInfo;
}
