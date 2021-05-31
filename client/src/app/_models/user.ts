export interface User{
    id: number;
    userName: string;
    token: string;
    firstName: string;
    title: string;
    secondName: string;
    isPacientAccount: boolean;
    cnp: string;
    mainPhotoUrl: string;
    roles: string[];
}