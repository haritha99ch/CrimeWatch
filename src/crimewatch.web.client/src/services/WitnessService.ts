import Api from "../configurations/ApiConfiguration";
import { UpdateWitnessDto } from "../models/Witness";
import WitnessId from "../valueObjects/WitnessId";

const controller = '/api/Witness';

export const GetWitness = async (witnessId : WitnessId) => {
    const response = await Api.get(`${controller}/Get/${witnessId.value}`);
    const witness = response.data;
    return witness;
}

export const GetWitnessReports = async (witnessId : WitnessId) => {
    const response = await Api.get(`${controller}/GetReports/${witnessId.value}`);
    const reports = response.data;
    return reports;
}

export const UpdateWitness = async (witness : UpdateWitnessDto) => {
    const response = await Api.patch(`${controller}/Update`, {witness});
    const updatedWitness = response.data;
    return updatedWitness;
}

export const DeleteWitness = async (witnessId : WitnessId) => {
    const response = await Api.delete(`${controller}/Delete/${witnessId.value}`);
    const result = response.data;
    return result;
}