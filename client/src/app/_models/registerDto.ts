export interface RegisterDto{
    username: string;
    password: string;
    isPacientAccount: boolean;
    pacientDto: PacientDto;
    doctorDto: DoctorDto;
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

export interface DoctorDto{
    firstName: string;
    secondName: string;
    email: string;
    dateOfBirth: Date;
}