export const amountValidator = (value: any) => {
  if (!value && value !== 0) return "Amount is required.";

  const s = String(value);
  // matches up to 10 digits before decimal + optional . + up to 2 decimals
  const regex = /^\d{1,10}(\.\d{1,2})?$/;
  if (!regex.test(s)) {
    return "Enter valid amount (max 12 digits total, 2 decimals).";
  }
  return "";
};