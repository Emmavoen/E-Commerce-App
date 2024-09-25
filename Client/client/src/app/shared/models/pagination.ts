export interface Pagination<T>{
    pageIndex: number;
    totalPages: number;
    totalCount: number;
    items: T;
}