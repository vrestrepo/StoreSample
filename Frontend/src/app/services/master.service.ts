import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Employee } from '../models/Employee';
import { Shipper } from '../models/Shipper';
import { Product } from '../models/Product';

@Injectable({
  providedIn: 'root'
})
export class MasterService {

  private apiUrl = environment.API_URL;

  constructor(private http: HttpClient) {}

  getEmployees(): Observable<{ success: boolean; data: Employee[] }> {
    return this.http.get<{ success: boolean; data: Employee[] }>(`${this.apiUrl}/Employees`);
  }

  getShippers(): Observable<{ success: boolean; data: Shipper[] }> {
    return this.http.get<{ success: boolean; data: Shipper[] }>(`${this.apiUrl}/Shippers`);
  }

  getProducts(): Observable<{ success: boolean; data: Product[] }> {
    return this.http.get<{ success: boolean; data: Product[] }>(`${this.apiUrl}/Products`);
  }
}
