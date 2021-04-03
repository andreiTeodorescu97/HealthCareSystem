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
    /* pacientContact: PacientContactDto; */
}

export interface DoctorDto{
    firstName: string;
    secondName: string;
    email: string;
    dateOfBirth: Date;
}


export interface PacientContactDto{
    street: string;
    streetNumber: number;
    firstPhone: string;
    secondPhone: string;
    city: string;
    region: string;
}