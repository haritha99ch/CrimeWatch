import AccountId from "../valueObjects/AccountId";

export default interface Account {
    accountId: AccountId
    email: string;
    password: string;
    isModerator: boolean;
}

export interface SignInDto{
    email: string,
    password: string
}