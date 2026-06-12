import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { ProductSaveResponse } from "./types";

export interface productRecordState {
  productSaveData: ProductSaveResponse | null;
}

export const initialState: productRecordState = {
  productSaveData: null,
};

const productSlice = createSlice({
  name: "productRecord",
  initialState,
  reducers: {
    setSelectedProduct(
      state,
      action: PayloadAction<ProductSaveResponse | null>,
    ) {
      state.productSaveData = action.payload;
    },
  },
  selectors: {
    selectedProductRecord: (state: productRecordState) => state,
  },
});

export const { selectedProductRecord } = productSlice.selectors;
export const { setSelectedProduct } = productSlice.actions;
export default productSlice.reducer;
