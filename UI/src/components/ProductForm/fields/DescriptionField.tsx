import { TextArea } from "@progress/kendo-react-inputs";
import { Label } from "@progress/kendo-react-labels";
import "../styles/ProductForm.css";

export const DescriptionField: React.FC = (props: any) => {
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
          Description:
        </Label>
        <TextArea
          onChange={handleChange}
          maxLength={250}
          className={visited && validationMessage ? "k-invalid" : ""}
          fillMode="outline"
        ></TextArea>
      </div>
      {visited && validationMessage && (
        <div className="sfield-error-text">{validationMessage}</div>
      )}
    </>
  );
};
