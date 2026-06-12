
export const productNameValidator = (value : any) => {
    if(!value|| String(value).trim() === "") return "Product name is required.";
    if (String(value).length > 50) return "Max 50 characters allowed.";
    return "";
};