export interface DataWithPagination<T> {
    data: T[];
    totalCount: number;
    pageSize: number;
    currentPage: number;
    totalPages: number;
  }
  