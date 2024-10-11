export interface DataWithPagination<T> {
  items: T[];
  paginationMetaData: PaginationMetaData;
}

export interface PaginationMetaData {
  totalPageCount: number;
  pageSize: number;
  currentPage: number;
  totalItemCount: number;
}
