import Api from "../configurations/ApiConfiguration";
import Witness, {UpdateWitnessDto} from "../models/Witness";
import WitnessId from "../valueObjects/WitnessId";
import {getBearerToken} from "./AuthenticationService";

const controller = '/api/Witness';

export const GetWitness = async (witnessId: WitnessId): Promise<Witness> => {
    const response = await Api.get<Witness>(`${controller}/Get/${witnessId.value}`, getBearerToken());
    const witness = response.data;
    return witness;
}

export const GetWitnessReports = async (witnessId: WitnessId): Promise<Report[]> => {
    const response = await Api.get<Report[]>(`${controller}/GetReports/${witnessId.value}`, getBearerToken());
    const reports = response.data;
    return reports;
}

export const UpdateWitness = async (witness: UpdateWitnessDto): Promise<Witness> => {
    const response = await Api.patch<Witness>(`${controller}/Update`, witness, getBearerToken());
    const updatedWitness = response.data;
    return updatedWitness;
}

export const DeleteWitness = async (witnessId: WitnessId): Promise<boolean> => {
    const response = await Api.delete<boolean>(`${controller}/Delete/${witnessId.value}`, getBearerToken());
    const result = response.data;
    return result;
}
