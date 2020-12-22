import { IManufacturer } from './../../../core/interfaces/manufacture.interface';
import { IMedicament } from './../../../core/interfaces/medicament.interface';
import { ISelectInfo } from './../../../shared/components/controls-test/controls-test.component';
import { IPatient } from '../../../core/interfaces/patient.interface';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IRecord } from 'src/app/core/interfaces/record.interface';
import { environment } from 'src/environments/environment';
import { IDiseaseHistory } from 'src/app/core/interfaces/disease-history.interface';
import { IReceipt } from 'src/app/core/interfaces/receip.interface';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient) {}

  getMedicalInstitutions(): Observable<ISelectInfo[]> {
    return this.http.get<ISelectInfo[]>(environment.apiUrl +  'MedicalInstituation/autocomplete');
  }

  getDoctorByMedicalInstitution(id: number): Observable<ISelectInfo[]> {
    return this.http.get<ISelectInfo[]>(environment.apiUrl +  'Doctor/doctors-by-instituation-autocomplete/' + id);
  }

  createRecord(form): Observable<any> {
    return this.http.post(environment.apiUrl +  'Record', form);
  }

  getPatientRecords(): Observable<IRecord[]> {
    return this.http.get<IRecord[]>(environment.apiUrl +  'Record/patient-records');
  }

  getRecordById(id: number): Observable<IRecord> {
    return this.http.get<IRecord>(environment.apiUrl +  'Record/get-record-by-id/' + id);
  }

  getPatientRecordsById(patientId: number): Observable<IRecord[]> {
    return this.http.get<IRecord[]>(environment.apiUrl +  'Record/patient-records-by-id/' + patientId);
  }

  getPatientReceiptsById(patientId: number): Observable<IReceipt[]> {
    return this.http.get<IReceipt[]>(environment.apiUrl +  'Receipt/receipt-by-patient-id/' + patientId);
  }

  getDoctorRecords(): Observable<IRecord[]> {
    return this.http.get<IRecord[]>(environment.apiUrl +  'Record/doctor-records');
  }

  cancelRecordLikePatient(status: any, recordId: number): Observable<any> {
    return this.http.put(environment.apiUrl +  'Record/update-status-like-patient?status=' + status + '&recordId=' + recordId, {});
  }

  updateRecord(form): Observable<any> {
    return this.http.put(environment.apiUrl +  'Record/update-record', form);
  }

  updateRecordLikeDoctor(status: any, recordId: number): Observable<any> {
    return this.http.put(environment.apiUrl +  'Record/update-status-like-doctor?status=' + status + '&recordId=' + recordId, {});
  }

  getPatientInfo(patientId: number): Observable<IPatient> {
    return this.http.get<IPatient>(environment.apiUrl +  'Patient/get-by-id/' + patientId);
  }

  getDiseaseHistoryInfo(patientId: number): Observable<IDiseaseHistory> {
    return this.http.get<IDiseaseHistory>(environment.apiUrl +  'DiseaseHistory/disease-history-by-patient-id/' + patientId);
  }

  updateDiseaseHistory(form: IDiseaseHistory): Observable<any> {
    return this.http.put(environment.apiUrl +  'DiseaseHistory', form);
  }

  createDiseaseHistory(form: IDiseaseHistory): Observable<any> {
    return this.http.post(environment.apiUrl +  'DiseaseHistory', form);
  }

  getMedicamentCategories(): Observable<any[]> {
    return this.http.get<any[]>(environment.apiUrl +  'MedicamentCategory');
  }

  getMedicamentsByCategory(id: number): Observable<IMedicament[]> {
    return this.http.get<IMedicament[]>(environment.apiUrl +  'Medicament/medicament-by-category-id/' + id);
  }

  createReceipt(form: IReceipt): Observable<any> {
    return this.http.post(environment.apiUrl + 'Receipt', form);
  }

  getMyReceipts(): Observable<IReceipt[]> {
    return this.http.get<IReceipt[]>(environment.apiUrl +  'Receipt/get-my-receipts');
  }

  updateReceiptStatus(receiptId: number, status: any): Observable<any> {
    return this.http.put(environment.apiUrl +  'Receipt/update-receipt-status?status=' + status + '&id=' + receiptId, {});
  }

  getManufacturers(): Observable<ISelectInfo[]> {
    return this.http.get<ISelectInfo[]>(environment.apiUrl +  'Manufacturer/autocomplete');
  }

  createManufacture(form: any): Observable<any> {
    return this.http.post(environment.apiUrl +  'Medicament', form);
  }

  updateManufacture(form: any): Observable<any> {
    return this.http.put(environment.apiUrl +  'Medicament', form);
  }

  getMedicaments(): Observable<IMedicament[]> {
    return this.http.get<IMedicament[]>(environment.apiUrl +  'Medicament');
  }

  createMedicamentCategory(form): Observable<any> {
    return this.http.post(environment.apiUrl +  'MedicamentCategory', form);
  }

  updateMedicamentCategory(form): Observable<any> {
    return this.http.put(environment.apiUrl +  'MedicamentCategory', form);
  }

  getManufacturerAll(): Observable<IManufacturer[]> {
    return this.http.get<IManufacturer[]>(environment.apiUrl +  'Manufacturer');
  }

  createManufacturer(form): Observable<any> {
    return this.http.post(environment.apiUrl +  'Manufacturer', form);
  }

  updateManufacturer(form): Observable<any> {
    return this.http.put(environment.apiUrl +  'Manufacturer', form);
  }

}
