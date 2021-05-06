import { VaccineDto } from "./vaccineDto";

export interface UpdatePacientVaccinesDto{
    vaccines: VaccineDto[];
    pacientId: number;
}