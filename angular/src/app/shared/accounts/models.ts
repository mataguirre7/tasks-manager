export interface AccountLoginDto {
  email?: string
  password?: string
  rememberMe?: boolean
}

export interface AccountRegisterDto {
  userName?: string
  fullName?:string
  email?: string
  password?: string
  phone?: string
  birthDate?: Date
}
