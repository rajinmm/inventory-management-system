export interface ProductSaveRequest {
  name: string;
  categoryId: number;
  amount: number;
  description: string;
  baseDiscountInPercentage: number;
}

export interface ProductSaveResponse {
  id: number;
  success: boolean;
}
