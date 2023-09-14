import Api from "../configurations/ApiConfiguration"
import Moderator, { ModeratorDto } from "../models/Moderator";
import Witness, { WitnessDto } from "../models/Witness"

const controller = '/api/Authentication';

export const CreateAccountForWitness = async (witness: WitnessDto) : Promise<Witness> => {
    const response = await Api.post<Witness>(`${controller}/Witness/SignUp`, witness);
    const createdWitness = response.data;
    return createdWitness;
}

export const CreateAccountForModerator = async (Moderator: ModeratorDto) : Promise<Moderator> => {
    const response = await Api.post<Moderator>(`${controller}/Moderator/SignUp`, Moderator);
    const createdModerator = response.data;
    return createdModerator;
}