import { Gender } from "../enums/Gender";
import AccountId from "../valueObjects/AccountId";
import UserId from "../valueObjects/UserId";
import WitnessId from "../valueObjects/WitnessId";
import Account from "./Account";
import User from "./User";

export interface UpdateWitnessDto {
    id: WitnessId;
    firstName: string;
    lastName: string;
    gender: Gender;
    dateOfBirth: Date;
    phoneNumber: string;
    email: string;
    Password: string;
}

export interface CreateWitnessDto {
    firstName: string;
    lastName: string;
    gender: Gender;
    birthDate: Date;
    phoneNumber: string;
    email: string;
    password: string;
}

export default interface Witness {
    witnessId: WitnessId;
    userId: UserId;
    accountId: AccountId;
    account: Account | null;
    user: User | null;
}