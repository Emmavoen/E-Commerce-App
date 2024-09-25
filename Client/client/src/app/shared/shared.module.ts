import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PagingHeaderComponent } from './paging-header/paging-header.component';
import { PagerComponent } from './pager/pager.component';// imported from angular bootstrap



@NgModule({
  declarations: [
    PagingHeaderComponent,
    PagerComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot() //loaded as singleton
  ],
  exports: [
    PaginationModule,
    PagingHeaderComponent,
    PagerComponent
  ]
})
export class SharedModule { }
