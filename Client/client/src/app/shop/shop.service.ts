import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from '../shared/models/pagination';
import { Product } from '../shared/models/product';
import { Observable } from 'rxjs';
import { Brand } from '../shared/models/brand';
import { Type } from '../shared/models/type';
import { ShopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:6601/api/';

  constructor(private http: HttpClient) { }
 
  getProducts(shopParams: ShopParams): Observable<Pagination<Product[]>> {
    let params = new HttpParams();

    if (shopParams.brandId > 0) params = params.append('brandId', shopParams.brandId);
    if (shopParams.typeId ) params = params.append('typeId', shopParams.typeId );

    // Check for ascending or descending sorting
    let sortBy = 'Name';
    let ascending = true;

    if (shopParams.sort === 'PriceAsc') {
      sortBy = 'Price';
      ascending = true;
    } else if (shopParams.sort === 'PriceDesc') {
      sortBy = 'Price';
      ascending = false;
    } else if (shopParams.sort === 'Name') {
      sortBy = 'Name';
      ascending = true;
    }

    params = params.append('sortBy', sortBy);

    params = params.append('pageSize', shopParams.pageSize);
    params = params.append('pageNumber', shopParams.pageNumber);
    params = params.append('ascending', ascending.toString());

    if (shopParams.searchTerm) {
      params = params.append('searchTerm', shopParams.searchTerm);
    } 
    //if(sort) params = params.append('sort', sort);

    return this.http.get<Pagination<Product[]>>(this.baseUrl + 'Products/GellAllProducts', { params });
  }

  getBrands() {
    return this.http.get<Brand[]>(this.baseUrl + 'ProductBrand/GetAllBrand')
  }

  getTypes() {
    return this.http.get<Type[]>(this.baseUrl + 'ProductType/GetAllProductType')
  }
}
