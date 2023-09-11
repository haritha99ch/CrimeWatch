import AccountId from "../valueObjects/AccountId";
import ModeratorId from "../valueObjects/ModeratorId";
import UserId from "../valueObjects/UserId";
import Account from "./Account";
import User from "./User";

export default interface Moderator{
    moderatorId: ModeratorId;
    policeId: string;
    userId: UserId;
    accountId: AccountId;
    province: string;
    user: User | null;
    account: Account | null;
}