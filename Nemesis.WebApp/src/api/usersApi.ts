import { get, post } from "./apiUtiles"

export interface User {
    login: string
}

const baseUrl = `${process.env.REACT_APP_API_URL}v1/users`

export function getMe(): Promise<User> {
    return get(`${baseUrl}/me`).then(x => x.json());
}

export interface LoginRequest {
    login: string,
    password: string
}

export interface LoginResponse {
    sessionId: string
}

export function logIn(request: LoginRequest): Promise<LoginResponse> {
    return post(`${baseUrl}/login`, request).then(x => x.json());
}

export interface RegisterRequest {
    login: string,
    password: string
}

export function register(request: RegisterRequest): Promise<Response> {
    return post(`${baseUrl}/register`, request);
}