import Api from "../configurations/ApiConfiguration";
import Evidence, { CreateEvidenceDto, UpdateEvidenceDto } from "../models/Evidence";
import EvidenceId from "../valueObjects/EvidenceId";
import ModeratorId from "../valueObjects/ModeratorId";
import ReportId from "../valueObjects/ReportId";
import { getBearerToken } from "./AuthenticationService";

const controller = '/api/Evidence';

export const CreateEvidence = async (evidence: CreateEvidenceDto) : Promise<Evidence> => {
    const response = await Api.post<Evidence>(`${controller}/Create`, evidence, await getBearerToken());
    const createdEvidence = response.data;
    return createdEvidence;
}

export const GetEvidenceForReport = async (reportId: ReportId) : Promise<Evidence[]> => {
    const response = await Api.get<Evidence[]>(`${controller}/GetAllForReport/${reportId.value}`, await getBearerToken());
    const evidence = response.data;
    return evidence;
}

export const UpdateEvidence = async (evidence: UpdateEvidenceDto) : Promise<Evidence> => {
    const response = await Api.put<Evidence>(`${controller}/Update`, evidence, await getBearerToken());
    const updatedEvidence = response.data;
    return updatedEvidence;
}

export const ModerateEvidence = async (evidenceId: EvidenceId, moderatorId: ModeratorId) : Promise<Evidence> => {
    const response = await Api.put<Evidence>(`${controller}/Moderate`, {evidenceId, moderatorId}, await getBearerToken());
    const moderatedEvidence = response.data;
    return moderatedEvidence;
}

export const ApproveEvidence = async (evidenceId: EvidenceId) : Promise<Evidence> => {
    const response = await Api.patch<Evidence>(`${controller}/Approve`, {evidenceId}, await getBearerToken());
    const approvedEvidence = response.data;
    return approvedEvidence;
}

export const DeclineEvidence = async (evidenceId: EvidenceId) : Promise<Evidence> => {
    const response = await Api.patch<Evidence>(`${controller}/Decline`, {evidenceId}, await getBearerToken());
    const declinedEvidence = response.data;
    return declinedEvidence;
}

export const RevertEvidence = async (evidenceId: EvidenceId) : Promise<Evidence> => {
    const response = await Api.patch<Evidence>(`${controller}/Revert`, {evidenceId}, await getBearerToken());
    const revertedEvidence = response.data;
    return revertedEvidence;
}

export const CommentEvidence = async (evidenceId: EvidenceId, comment: string) : Promise<Evidence> => {
    const response = await Api.post<Evidence>(`${controller}/Comment`, {evidenceId, comment}, await getBearerToken());
    const commentedEvidence = response.data;
    return commentedEvidence;
}

export const DeleteEvidence = async (evidenceId: EvidenceId) : Promise<boolean> => {
    const response = await Api.delete<boolean>(`${controller}/Delete/${evidenceId.value}`, await getBearerToken());
    const deletedEvidence = response.data;
    return deletedEvidence;
}
