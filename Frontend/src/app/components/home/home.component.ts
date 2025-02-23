import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import { OrderViewComponent } from '../order-view/order-view.component';
import { CreateOrderDialogComponent } from '../create-order-dialog/create-order-dialog.component';
import { OrderPrediction } from '../../models/OrderPrediction';
import { OrderService } from '../../services/order.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  displayedColumns: string[] = ['customerName', 'lastOrderDate', 'nextPredictedOrder', 'actions'];
  dataSource = new MatTableDataSource<OrderPrediction>([]);
  filteredDataSource = new MatTableDataSource<OrderPrediction>([]);
  customerNameFilter: string = '';

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private orderService: OrderService, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.fetchOrders();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.paginator.pageSize = 10;
    this.dataSource.sort = this.sort;
  }

  fetchOrders(): void {
    this.orderService.getPredictedOrders().subscribe(response => {
      if (response.success) {
        this.dataSource.data = response.data;
        this.filteredDataSource.data = response.data;
        this.filteredDataSource.paginator = this.paginator;
        this.filteredDataSource.sort = this.sort;
      }
    });
  }

  applyFilter(): void {
    this.dataSource.filter = this.customerNameFilter.trim().toLowerCase();
  }

  viewDetails(row: OrderPrediction): void {
    this.dialog.open(OrderViewComponent, {
      width: '1000px',
      data: { custid: row.custid }
    });
  }

  createOrder(row: OrderPrediction): void {
    this.dialog.open(CreateOrderDialogComponent, {
      width: '800px',
      data: { custid: row.custid }
    });
  }
}
