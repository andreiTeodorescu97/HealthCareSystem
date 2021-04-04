export interface RegisterDto{
    username: string;
    password: string;
    isPacientAccount: boolean;
    pacientDto: PacientRegisterDto;
    doctorDto: DoctorRegisterDto;
}

export interface PacientRegisterDto{
    firstName: string;
    secondName: string;
    email: string;
    gender: string;
    series: string;
    identityNumber: string;
    cnp: string;
    dateOfBirth: Date;
}

export interface DoctorRegisterDto{
    firstName: string;
    secondName: string;
    email: string;
    dateOfBirth: Date;
}