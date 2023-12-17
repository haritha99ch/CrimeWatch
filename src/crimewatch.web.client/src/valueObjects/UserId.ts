import IValueObject from "../contracts/IValueObject";

export default class UserId implements IValueObject {
    public value: string;

    constructor(value: string) {
        this.value = value;
    }
}