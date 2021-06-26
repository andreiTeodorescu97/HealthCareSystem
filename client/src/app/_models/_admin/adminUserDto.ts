export interface AdminUserDto {
    id: number;
    firstName: string;
    secondName: string;
    email: string;
    username: string;
    roles: string[];
    isAccountLocked: boolean;
}