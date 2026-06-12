export const discountValidator = (value: any) => {
  if (value === undefined || value === null || value === "") return ""; // optional
  const s = String(value);
  const regex = /^\d{1,3}$/;
  if (!regex.test(s)) return "Only numbers (max 3 digits).";
  const num = Number(s);
  if (isNaN(num)) return "Discount must be a number.";
  if (num > 100) return "Discount cannot be greater than 100.";
  return "";
};