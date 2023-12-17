import IValueObject from "../contracts/IValueObject";

export default class ModeratorId implements IValueObject {
    public value: string;

    constructor(value: string) {
        this.value = value;
    }
}