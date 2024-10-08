import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Product } from '../shared/models/product';
import { ShopService } from './shop.service';
import { Brand } from '../shared/models/brand';
import { Type } from '../shared/models/type';
import { ShopParams } from '../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit{
  @ViewChild('search') searchTerm?: ElementRef; 
  products: Product[] = [];
  brands: Brand[] = [];
  types: Type[] = [];
  shopParams = new ShopParams();
  sortOptions = [
    {name:"Alphabetical", value: "name"},
    {name:"Price:low to high", value: "PriceAsc"},
    {name:"Price:high to low", value: "PriceDesc"},
  ];
  totalCount = 0;

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe(
      {
        next: response => {this.products = response.items;
          this.shopParams.pageNumber = response.pageIndex;
          this.shopParams.pageSize = 6;
          this.totalCount = response.totalCount;
        console.log(response);},
        error: error => console.log(error)
      }) 
  }

  getBrands() {
    this.shopService.getBrands().subscribe(
      {
        next: response => {this.brands = [{id: 0,name: 'All'}, ...response ],
        console.log(response);},
        error: error => console.log(error)
      }) 
  }

  getTypes() {
    this.shopService.getTypes().subscribe(
      {
        next: response => {this.types = [{id: 0,name: 'All'}, ...response ];
        console.log(response);},
        error: error => console.log(error)
      }) 
  }

  onBrandSelected(brandId: number){
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onTypeSelected(typeId: number){
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onSortSelected(event: any){ 
    this.shopParams.sort = event.target.value;
    this.getProducts();
  }

  onPageChanged(event: any){
    if(this.shopParams.pageNumber != event){
      this.shopParams.pageNumber = event;
      this.getProducts();
    }

  }

  onSearch(){
    this.shopParams.searchTerm = this.searchTerm?.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onReset() {
    if (this.searchTerm) this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }

}
