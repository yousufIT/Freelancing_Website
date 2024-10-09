export interface DataWithPagination<T> {
    items: T[];
    PaginationMetaData: PaginationMetaData;
  }

export interface PaginationMetaData {
  TotalPageCount: number;
  PageSize: number;
  CurrentPage: number;
  TotalItemCount: number;
}