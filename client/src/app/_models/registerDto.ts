export interface RegisterDto{
    username: string;
    password: string;
    isPacientAccount: boolean;
    pacientDto: PacientDto;
}

export interface PacientDto{
    firstName: string;
    secondName: string;
    email: string;
    gender: string;
    series: string;
    identityNumber: string;
    cnp: string;
    dateOfBirth: Date;
}