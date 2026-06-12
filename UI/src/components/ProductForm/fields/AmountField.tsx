import React, { useRef } from "react";
import { Label } from "@progress/kendo-react-labels";
import { setCursorRight } from "../../../utils/setCursorRight";
import { TextBox } from "@progress/kendo-react-inputs";
import "../styles/ProductForm.css";

export const AmountField: React.FC = (props: any) => {
  const { value, onChange, validationMessage, visited } = props;
  const inputRef = useRef<any>(null);

  const handleChange = (e: any) => {
    let v: string = String(e.value ?? "");
    v = v.replace(/[^0-9.]/g, ""); // only digits and dot

    const parts = v.split(".");

    // allow only a single dot
    if (parts.length > 2) {
      v = parts[0] + "." + parts.slice(1).join("");
    }

    // restrict decimals to 2 digits
    if (parts[1]?.length > 2) {
      v = parts[0] + "." + parts[1].slice(0, 2);
    }

    // max 12 digits (excluding dot)
    const rawDigits = v.replace(".", "");
    if (rawDigits.length > 12) {
      v = rawDigits.slice(0, 12);

      // simple reinsertion of dot if decimals existed
      if (parts.length > 1) {
        const before = v.slice(0, v.length - Math.min(2, v.length));
        const after = v.slice(v.length - Math.min(2, v.length));
        v = before + (after ? "." + after : "");
      }
    }

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
          Amount:
        </Label>

        <TextBox
          ref={inputRef}
          value={value ?? ""}
          onChange={handleChange}
          onFocus={handleFocus}
          className={visited && validationMessage ? "k-invalid" : ""}
          fillMode="outline"
        />
      </div>

      {visited && validationMessage && (
        <div className="sfield-error-text">{validationMessage}</div>
      )}
    </>
  );
};
