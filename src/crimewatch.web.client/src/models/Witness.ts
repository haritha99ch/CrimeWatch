import AccountId from "../valueObjects/AccountId";
import UserId from "../valueObjects/UserId";
import WitnessId from "../valueObjects/WitnessId";
import Account from "./Account";
import User from "./User";

export default interface Witness {
    witnessId: WitnessId;
    userId: UserId;
    accountId: AccountId;
    account: Account | null;
    user: User | null;
}