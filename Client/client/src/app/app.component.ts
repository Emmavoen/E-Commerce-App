import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Product } from './models/product';
import { Pagination } from './models/pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'App';  
  products: Product[] = [];

  constructor(private http: HttpClient,  private cdr: ChangeDetectorRef) {}
  ngOnInit(): void {
    this.http.get<Pagination<Product[]>>('https://localhost:5001/api/Products/GellAllProducts?ascending=true&pageSize=18').subscribe({
      next: (response) => {
        console.log(response); // Check the structure of the response
        this.products = response.data;
        this.cdr.detectChanges();},
      error: error => console.log(error),
      complete: () => {
        console.log('request completed');
        console.log('extra statement');
      }
    })
  }
  
}
