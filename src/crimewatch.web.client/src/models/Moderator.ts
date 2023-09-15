import { Gender } from "../enums/Gender";
import AccountId from "../valueObjects/AccountId";
import ModeratorId from "../valueObjects/ModeratorId";
import UserId from "../valueObjects/UserId";
import Account from "./Account";
import User from "./User";

export interface UpdateModeratorDto {
    id: ModeratorId;
    firstName: string;
    lastName: string;
    gender: Gender;
    dateOfBirth: Date;
    phoneNumber: string;
    policeId: string;
    province: string;
    email: string;
    password: string;
}


export interface CreateModeratorDto {
    firstName: string;
    lastName: string;
    gender: Gender;
    birthDate: Date;
    phoneNumber: string;
    email: string;
    password: string;
    policeId: string;
    province: string;
}

export default interface Moderator{
    moderatorId: ModeratorId;
    policeId: string;
    userId: UserId;
    accountId: AccountId;
    province: string;
    user: User | null;
    account: Account | null;
}