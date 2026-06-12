export interface LoginRequest {
  userLogin: string;
  password: string;
}

export interface LoginResponse {
  userId: number;
  userLogin: string;
  name: string;
  token: string;
  tokenType: string;
  expiresIn: number;
  issuedAt: string;
  expiresAt: string;
}
