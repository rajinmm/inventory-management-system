import React, { useRef } from "react";
import { setCursorRight } from "../../../utils/setCursorRight";
import { Label } from "@progress/kendo-react-labels";
import { TextBox } from "@progress/kendo-react-inputs";
import "../styles/ProductForm.css";

export const DiscountField: React.FC = (props: any) => {
  const { value, onChange, validationMessage, visited } = props;
  const inputRef = useRef<any>(null);
  const handleChange = (e: any) => {
    let v: string = String(e.value ?? "");
    v = v.replace(/[^0-9]/g, "");
    if (v.length > 3) v = v.slice(0, 3);
    onChange({ value: v });
  };

  const handleFocus = () => {
    const kendoElement = inputRef.current?.element;

    // REAL <input> element returned here (your screenshot confirms this)
    const native =
      kendoElement instanceof HTMLInputElement ? kendoElement : null;

    if (native) {
      native.style.textAlign = "right"; // align text
      setCursorRight(native); // move cursor
    }
  };

  return (
    <>
      <div className="sfield-row">
        <Label
          className={`sfield-label ${
            visited && validationMessage ? "sfield-label-error" : ""
          }`}
        >
          Discount:
        </Label>
        <TextBox
          ref={inputRef}
          value={value ?? ""}
          onChange={handleChange}
          onFocus={handleFocus}
          fillMode="outline"
        />
      </div>
      {visited && validationMessage && (
        <div className="sfield-error-text">{validationMessage}</div>
      )}
    </>
  );
};
