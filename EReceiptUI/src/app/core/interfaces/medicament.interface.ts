import { IManufacturer } from "./manufacture.interface";
import { ISelectInfo } from "./select-info.interface";

export interface IMedicament {
  id: number;
  name: string;
  categoryId: number;
  manufacturerId: number;
  instruction: string;
  description: string;
  manufacturerName: string;
  manufacturerLicense: string;
  picture: any;
  pictureByte: any;
  contentType: string;
  medicamentCategory: ISelectInfo;
  pharmacies: ISelectInfo[];
  receipts: ISelectInfo[];
}
