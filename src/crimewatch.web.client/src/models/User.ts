import {Gender} from "../enums/Gender";
import UserId from "../valueObjects/UserId";

export default interface User {
    userId: UserId;
    firstName: string;
    lastName: string;
    gender: Gender;
    dateOfBirth: string;
    age: number;
    phoneNumber: string;
}