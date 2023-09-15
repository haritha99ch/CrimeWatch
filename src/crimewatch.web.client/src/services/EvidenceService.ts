import Api from "../configurations/ApiConfiguration";
import Evidence, { CreateEvidenceDto, UpdateEvidenceDto } from "../models/Evidence";
import EvidenceId from "../valueObjects/EvidenceId";
import ModeratorId from "../valueObjects/ModeratorId";
import ReportId from "../valueObjects/ReportId";

const controller = '/api/Evidence';

export const CreateEvidence = async (evidence: CreateEvidenceDto) : Promise<Evidence> => {
    const response = await Api.post<Evidence>(`${controller}/Create`, evidence);
    const createdEvidence = response.data;
    return createdEvidence;
}

export const GetEvidenceForReport = async (reportId: ReportId) : Promise<Evidence[]> => {
    const response = await Api.get<Evidence[]>(`${controller}/GetAllForReport/${reportId.value}`);
    const evidence = response.data;
    return evidence;
}

export const UpdateEvidence = async (evidence: UpdateEvidenceDto) : Promise<Evidence> => {
    const response = await Api.put<Evidence>(`${controller}/Update`, evidence);
    const updatedEvidence = response.data;
    return updatedEvidence;
}

export const ModerateEvidence = async (evidenceId: EvidenceId, moderatorId: ModeratorId) : Promise<Evidence> => {
    const response = await Api.put<Evidence>(`${controller}/Moderate`, {evidenceId, moderatorId});
    const moderatedEvidence = response.data;
    return moderatedEvidence;
}

export const ApproveEvidence = async (evidenceId: EvidenceId) : Promise<Evidence> => {
    const response = await Api.patch<Evidence>(`${controller}/Approve`, {evidenceId});
    const approvedEvidence = response.data;
    return approvedEvidence;
}

export const DeclineEvidence = async (evidenceId: EvidenceId) : Promise<Evidence> => {
    const response = await Api.patch<Evidence>(`${controller}/Decline`, {evidenceId});
    const declinedEvidence = response.data;
    return declinedEvidence;
}

export const RevertEvidence = async (evidenceId: EvidenceId) : Promise<Evidence> => {
    const response = await Api.patch<Evidence>(`${controller}/Revert`, {evidenceId});
    const revertedEvidence = response.data;
    return revertedEvidence;
}

export const CommentEvidence = async (evidenceId: EvidenceId, comment: string) : Promise<Evidence> => {
    const response = await Api.post<Evidence>(`${controller}/Comment`, {evidenceId, comment});
    const commentedEvidence = response.data;
    return commentedEvidence;
}

export const DeleteEvidence = async (evidenceId: EvidenceId) : Promise<boolean> => {
    const response = await Api.delete<boolean>(`${controller}/Delete/${evidenceId.value}`);
    const deletedEvidence = response.data;
    return deletedEvidence;
}
