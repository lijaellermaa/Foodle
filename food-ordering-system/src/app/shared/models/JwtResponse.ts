export interface JwtResponse {
  jwt: string,
  refreshToken: string,
  id?: string,
  email?: string,
  firstName?: string,
  lastName?: string,
  address?: string,
  token?: string,
  isAdmin?: boolean
}
