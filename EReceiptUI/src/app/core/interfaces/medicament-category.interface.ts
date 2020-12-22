import { ISelectInfo } from "./select-info.interface";

export interface IMedicamentCategory {
  id: number;
  name: string;
  medicaments: ISelectInfo[];
}
