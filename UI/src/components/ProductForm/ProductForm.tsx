import React, { useState } from "react";
import { Field, Form, FormElement } from "@progress/kendo-react-form";
import { Button } from "@progress/kendo-react-buttons";

import { ProductNameField } from "./fields/ProductNameField";
import { CategoryField } from "./fields/CategoryField";
import { DescriptionField } from "./fields/DescriptionField";
import { AmountField } from "./fields/AmountField";
import { DiscountField } from "./fields/DiscountField";

import { productNameValidator } from "./validators/productNameValidator";
import { categoryValidator } from "./validators/categoryValidator";
import { descriptionValidator } from "./validators/descriptionValidator";
import { amountValidator } from "./validators/amountValidator";
import { discountValidator } from "./validators/discountValidator";
import { useSaveProductMutation } from "../../apiutils/product/productService";

export const ProductForm: React.FC = () => {
  const [saveProduct, { isLoading }] = useSaveProductMutation();
  const [notification, setNotification] = useState<{
    type: "success" | "error" | null;
    message: string;
  }>({ type: null, message: "" });

  const showNotification = (type: "success" | "error", message: string) => {
    setNotification({ type, message });

    setTimeout(() => {
      setNotification({ type: null, message: "" });
    }, 5000); // Auto hide after 5 seconds
  };
  const handleSubmit = async (dataItem: any) => {
    console.log("Form Submitted!", dataItem);
    const requestPayload = {
      name: dataItem.productName,
      categoryId: dataItem.category.id,
      amount: dataItem.amount,
      description: dataItem.description,
      baseDiscountInPercentage: dataItem.discount,
    };
    console.log("Form Submitted!", requestPayload);
    try {
      const result = await saveProduct(requestPayload).unwrap();

      showNotification("success", "Product saved successfully!");
    } catch (err: any) {
      showNotification("error", "Failed to save product!");
    }
  };

  return (
    <div className="max-w-4xl mx-auto bg-white rounded-xl shadow p-8">
      <h2 className="text-2xl font-semibold mb-6 text-gray-700">Add Product</h2>

      {notification.type && (
        <div
          className={`fixed top-5 right-5 px-4 py-3 rounded shadow text-white z-50 ${
            notification.type === "success" ? "bg-green-500" : "bg-red-500"
          }`}
        >
          {notification.message}
        </div>
      )}

      <Form
        onSubmit={handleSubmit}
        render={() => (
          <FormElement>
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
              <Field
                name="productName"
                component={ProductNameField}
                validator={productNameValidator}
              />

              <Field
                name="category"
                component={CategoryField}
                validator={categoryValidator}
              />

              <div className="md:col-span-2">
                <Field
                  name="description"
                  component={DescriptionField}
                  validator={descriptionValidator}
                />
              </div>

              <Field
                name="amount"
                component={AmountField}
                validator={amountValidator}
              />

              <Field
                name="discount"
                component={DiscountField}
                validator={discountValidator}
              />
            </div>

            <div className="mt-8 flex justify-end">
              <Button
                themeColor="primary"
                disabled={isLoading}
                className="px-6"
              >
                {isLoading ? "Saving..." : "Save Product"}
              </Button>
            </div>
          </FormElement>
        )}
      />
    </div>
  );
};
