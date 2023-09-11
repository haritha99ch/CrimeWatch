import IValueObject from "../contracts/IValueObject";

export default class WitnessId implements IValueObject {
    public value: string;
    constructor(value: string) {
        this.value = value;
    }
}