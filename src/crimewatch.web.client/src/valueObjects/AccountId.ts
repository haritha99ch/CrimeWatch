﻿import IValueObject from "../contracts/IValueObject";

export default class AccountId implements IValueObject {
    public value: string;
    constructor(value: string) {
        this.value = value;
    }
}