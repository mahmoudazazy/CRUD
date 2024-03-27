export interface SuccessPagedResult<T> {
  data: PagedResult<T>[];
  succeeded: boolean;
  message?: string;
  errors?: string;
}

export interface PagedResult<T> {
  data: T[];
  pageIndex: number;
  totalItems: number;
}


export interface Employee {
  id?: number;
  firstName?: string;
  lastName?: string;
  emailAddress?: string;
  salary: number;
}
