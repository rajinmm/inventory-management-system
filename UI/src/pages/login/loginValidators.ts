export const usernameValidator = (value: string) => {
    value ? "" : "Username is required";
}

export const passwordValidator = (value: string) => {
    value ? "" : "Password is required";
}