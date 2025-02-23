import { Component, Inject, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Employee } from '../../models/Employee';
import { Shipper } from '../../models/Shipper';
import { Product } from '../../models/Product';
import { OrderService } from '../../services/order.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MasterService } from '../../services/master.service';

@Component({
  selector: 'app-create-order-dialog',
  templateUrl: './create-order-dialog.component.html',
  styleUrls: ['./create-order-dialog.component.scss']
})
export class CreateOrderDialogComponent implements OnInit {
  orderForm!: FormGroup;
  employees: Employee[] = [];
  shippers: Shipper[] = [];
  products: Product[] = [];

  constructor(
    private fb: FormBuilder,
    private orderService: OrderService,
    private masterService: MasterService,
    private snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<CreateOrderDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  ngOnInit(): void {
    this.orderForm = this.fb.group({
      empId: [null, Validators.required],
      shipperId: [null, Validators.required],
      shipName: ['', Validators.required],
      shipAddress: ['', Validators.required],
      shipCity: ['', Validators.required],
      orderDate: [null, Validators.required],
      requiredDate: [null, Validators.required],
      shippedDate: [null],
      freight: [null, [Validators.required, Validators.min(0)]],
      shipCountry: ['', Validators.required],
      orderDetails: this.fb.array([])
    });

    this.masterService.getEmployees().subscribe(response => this.employees = response.data);
    this.masterService.getShippers().subscribe(response => this.shippers = response.data);
    this.masterService.getProducts().subscribe(response => this.products = response.data);

    this.addOrderDetail();
  }

  submitOrder(): void {
    if (this.orderForm.valid) {
      const formValue = this.orderForm.value;
      const orderData = {
        ...formValue,
        custid: this.data.custid,
        orderDate: formValue.orderDate ? new Date(formValue.orderDate).toISOString() : null,
        requiredDate: formValue.requiredDate ? new Date(formValue.requiredDate).toISOString() : null,
        shippedDate: formValue.shippedDate ? new Date(formValue.shippedDate).toISOString() : null,
        orderDetails: formValue.orderDetails.map((detail: any) => ({
          ...detail,
          unitPrice: parseFloat(detail.unitPrice),
          qty: parseInt(detail.qty, 10),
          discount: parseFloat(detail.discount)
        }))
      };

      this.orderService.createOrder(orderData).subscribe({
        next: (response) => {
          const message = response.success ? 'Orden creada exitosamente' : 'Error al crear la orden';
          this.showNotification(message);
          this.dialogRef.close(true);
        },
        error: (err) => {
          this.showNotification('Ocurrio un error al crear la orden');
          console.warn('Error', err);
        }
      });
    }
  }

  get orderDetails(): FormArray {
    return this.orderForm.get('orderDetails') as FormArray;
  }

  private addOrderDetail() {
    const orderDetail = this.fb.group({
      productId: [null, Validators.required],
      unitPrice: [0, [Validators.required, Validators.min(0)]],
      qty: [1, [Validators.required, Validators.min(1)]],
      discount: [0, [Validators.min(0), Validators.max(1)]]
    });
    this.orderDetails.push(orderDetail);
  }

  removeOrderDetail(index: number) {
    this.orderDetails.removeAt(index);
  }

  closeDialog(): void {
    this.dialogRef.close();
  }

  showNotification(message: string): void {
    this.snackBar.open(message, 'Cerrar', {
      duration: 2000,
      horizontalPosition: 'right',
      verticalPosition: 'top',
      panelClass: ['custom-snackbar']
    });
  }
}
