import { IDoctor } from "./doctor.interface";

export interface IMedicalInstitution {
  id: number;
  name: string;
  country: string;
  city: string;
  address: string;
  doctors: IDoctor[];
}
