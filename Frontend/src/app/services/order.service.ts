import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { OrderPrediction } from '../models/OrderPrediction';
import { environment } from '../../environments/environment';
import { Order } from '../models/Order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private apiUrl = environment.API_URL;

  constructor(private http: HttpClient) {}

  getPredictedOrders(): Observable<{ success: boolean; data: OrderPrediction[] }> {
    return this.http.get<{ success: boolean; data: OrderPrediction[] }>(`${this.apiUrl}/Orders/predicted-orders`);
  }

  getClientOrders(custid: any): Observable<{ success: boolean; data: Order[] }> {
    return this.http.get<{ success: boolean; data: Order[] }>(`${this.apiUrl}/Orders/client-orders/${custid}`);
  }

  createOrder(orderData: any): Observable<{ success: boolean; data: any }> {
    return this.http.post<{ success: boolean; data: any }>(`${this.apiUrl}/Orders/create-order`, orderData);
  }
}
