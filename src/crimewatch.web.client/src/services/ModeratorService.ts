import Api from "../configurations/ApiConfiguration";
import Moderator, {UpdateModeratorDto} from "../models/Moderator";
import ModeratorId from "../valueObjects/ModeratorId";
import {getBearerToken} from "./AuthenticationService";

const controller = '/api/Moderator';

export const GetModerator = async (moderatorId: ModeratorId): Promise<Moderator> => {
    const response = await Api.get<Moderator>(`${controller}/Get/${moderatorId.value}`, getBearerToken());
    const moderator = response.data;
    return moderator;
}

export const GetModeratorReports = async (moderatorId: ModeratorId): Promise<Report[]> => {
    const response = await Api.get<Report[]>(`${controller}/GetReports/${moderatorId.value}`, getBearerToken());
    const reports = response.data;
    return reports;
}

export const UpdateModerator = async (moderator: UpdateModeratorDto): Promise<Moderator> => {
    const response = await Api.patch<Moderator>(`${controller}/Update`, moderator, getBearerToken());
    const updatedModerator = response.data;
    return updatedModerator;
}

export const DeleteModerator = async (moderatorId: ModeratorId): Promise<boolean> => {
    const response = await Api.delete<boolean>(`${controller}/Delete/${moderatorId.value}`, getBearerToken());
    const result = response.data;
    return result;
}
