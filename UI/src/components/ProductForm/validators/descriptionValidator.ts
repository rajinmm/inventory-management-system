
export const descriptionValidator = (value: any) => {
  if (!value || String(value).trim() === "") return "Description is required.";
  if (String(value).length > 200) return "Max 200 characters allowed.";
  return "";
};