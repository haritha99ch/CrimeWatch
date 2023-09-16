import Api from "../configurations/ApiConfiguration"
import Moderator, { CreateModeratorDto } from "../models/Moderator";
import Witness, { CreateWitnessDto } from "../models/Witness"
import { deleteToken, getBearerToken, saveToken } from "./AuthenticationService";

const controller = '/api/Authentication';

export const CreateAccountForWitness = async (witness: CreateWitnessDto) : Promise<Witness> => {
    const response = await Api.post<Witness>(`${controller}/Witness/SignUp`, witness);
    const createdWitness = response.data;
    return createdWitness;
}

export const CreateAccountForModerator = async (Moderator: CreateModeratorDto) : Promise<Moderator> => {
    const response = await Api.post<Moderator>(`${controller}/Moderator/SignUp`, {Moderator});
    const createdModerator = response.data;
    return createdModerator;
}

export const SingIn = async (email: string, password: string) : Promise<string> => {
    const response = await Api.post<string>(`${controller}/SignIn`, {email, password});
    const token = response.data;
    saveToken(token);
    return token;
}

export const GetCurrentUser = async () : Promise<Moderator | Witness> => {
    const response = await Api.get<Moderator | Witness>(`${controller}/GetCurrentUser`, getBearerToken())
    const user = response.data;
    return user;
}

export const SignOut = () : void => {
    deleteToken();
}