import { TextBox } from "@progress/kendo-react-inputs";
import { Label } from "@progress/kendo-react-labels";
import "../styles/ProductForm.css";

export const ProductNameField: React.FC = (props: any) => {
  const { value, onChange, validationMessage, visited } = props;
  const handleChange = (e: any) => onChange({ value: e.value });
  return (
    <>
      <div className="sfield-row">
        <Label
          className={`sfield-label ${
            visited && validationMessage ? "sfield-label-error" : ""
          }`}
        >
          Product Name:
        </Label>

        <TextBox
          value={props.value}
          onChange={handleChange}
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
