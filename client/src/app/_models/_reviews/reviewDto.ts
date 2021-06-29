export interface ReviewDto {
    id: number;
    rating: number;
    content: string;
    dateAdded: Date;
    pacientId: number;
    pacientFirstName: string;
    pacientSecondName: string;
    doctorId: number;
    doctorFirstName: string;
    doctorSecondName: string;
    doctorProfilePicture: string;
}