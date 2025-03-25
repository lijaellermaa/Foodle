export type UserPayload = {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  address: string;
}

export type ChangeProfileRequest = {
  firstName: string;
  lastName: string;
  email?: string;
  address: string;
}

export type AuthResponse = {
  token: string;
  refreshToken: string;
  expiresAt: Date;
  user: UserPayload;
  roles: string[];
}

export type RegisterRequest = {
  password: string;
  firstName: string;
  lastName: string;
  address: string;
  email: string;
}

export type LoginRequest = {
  email: string;
  password: string;
}

export type LogoutRequest = {
  refreshToken: string;
}

export type RefreshTokenRequest = {
  token: string;
  refreshToken: string;
}
