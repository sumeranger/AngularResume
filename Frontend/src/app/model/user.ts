export interface UserForRegister {
    username: string;
    email?: string;
    password: string;
    mobile?: number;
}

export interface UserLogin {
    username: string;
    password: string;
    token: string;
}