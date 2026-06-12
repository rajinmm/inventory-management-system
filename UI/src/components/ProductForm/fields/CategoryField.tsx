import { DropDownList } from "@progress/kendo-react-dropdowns";
import { Label } from "@progress/kendo-react-labels";
import React from "react";
import "../styles/ProductForm.css";

const categories = [
  { id: 1, text: "Grocery" },
  { id: 2, text: "Fashion" },
];

export const CategoryField: React.FC = (props: any) => {
  const { value, onChange, validationMessage, visited } = props;
  const handleChange = (e: any) => {
    onChange({ value: e.value });
  };
  return (
    <>
      <div className="sfield-row">
        <Label
          className={`sfield-label ${
            visited && validationMessage ? "sfield-label-error" : ""
          }`}
        >
          Category :
        </Label>
        <DropDownList
          data={categories}
          textField="text"
          dataItemKey="id"
          onChange={handleChange}
          className={visited && validationMessage ? "k-invalid" : ""}
          fillMode="outline"
        ></DropDownList>
      </div>
      {visited && validationMessage && (
        <div className="sfield-error-text">{validationMessage}</div>
      )}
    </>
  );
};
