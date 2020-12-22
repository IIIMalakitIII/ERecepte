import { IRecord } from "./record.interface";

export interface IPatient {
  id: number;
  userId: string;
  firstName: string;
  lastName: string;
  country: string;
  passport: string;
  address: string;
  cardNumber: string;
  diseaseHistoryId: number | null;
  phone: string;
  records: IRecord[];
  //receipts: Receipt[];
  //confidants: Confidant[];
  //user: User;
  // diseaseHistory: DiseaseHistory;
}
