import { ISelectInfo } from "./select-info.interface";

export interface IManufacturer {
  id: number;
  name: string;
  license: string;
  description: string;
  medicaments: ISelectInfo[];
}
