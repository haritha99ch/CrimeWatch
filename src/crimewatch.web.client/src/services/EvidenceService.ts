import { AxiosRequestConfig } from "axios";
import Api from "../configurations/ApiConfiguration";
import Evidence, { CreateEvidenceDto, UpdateEvidenceDto } from "../models/Evidence";
import EvidenceId from "../valueObjects/EvidenceId";
import ModeratorId from "../valueObjects/ModeratorId";
import ReportId from "../valueObjects/ReportId";
import { getBearerToken, getToken } from "./AuthenticationService";

const controller = '/api/Evidence';

export const CreateEvidence = async (evidence: CreateEvidenceDto) : Promise<Evidence> => {
    const formData = new FormData();
    formData.append('caption', evidence.caption);
    formData.append('description', evidence.description);
    formData.append('Location.No', evidence.location.no);
    formData.append('Location.Street1', evidence.location.street1);
    formData.append('Location.Street2', evidence.location.street2);
    formData.append('Location.City', evidence.location.city);
    formData.append('WitnessId.Value', evidence.witnessId.value);
    formData.append('ReportId.Value', evidence.reportId.value);
    for (const mediaItem of evidence.mediaItems) {
        formData.append(`MediaItems`, mediaItem);
    }  
    
    console.log(evidence.mediaItems);
    
    
    const config: AxiosRequestConfig = {headers: {'Content-Type': 'multipart/form-data', Authorization: `bearer ${getToken()}`}};
    const response = await Api.post<Evidence>(`${controller}/Create`, formData, config);
    const createdEvidence = response.data;
    return createdEvidence;
}

export const GetEvidenceForReport = async (reportId: ReportId) : Promise<Evidence[]> => {
    const response = await Api.get<Evidence[]>(`${controller}/GetAllForReport/${reportId.value}`, getBearerToken());
    const evidence = response.data;
    return evidence;
}

export const UpdateEvidence = async (evidence: UpdateEvidenceDto) : Promise<Evidence> => {
    const response = await Api.put<Evidence>(`${controller}/Update`, evidence, getBearerToken());
    const updatedEvidence = response.data;
    return updatedEvidence;
}

export const ModerateEvidence = async (evidenceId: EvidenceId, moderatorId: ModeratorId) : Promise<Evidence> => {
    const response = await Api.put<Evidence>(`${controller}/Moderate`, {evidenceId, moderatorId}, getBearerToken());
    const moderatedEvidence = response.data;
    return moderatedEvidence;
}

export const ApproveEvidence = async (evidenceId: EvidenceId) : Promise<Evidence> => {
    const response = await Api.patch<Evidence>(`${controller}/Approve`, {evidenceId}, getBearerToken());
    const approvedEvidence = response.data;
    return approvedEvidence;
}

export const DeclineEvidence = async (evidenceId: EvidenceId) : Promise<Evidence> => {
    const response = await Api.patch<Evidence>(`${controller}/Decline`, {evidenceId}, getBearerToken());
    const declinedEvidence = response.data;
    return declinedEvidence;
}

export const RevertEvidence = async (evidenceId: EvidenceId) : Promise<Evidence> => {
    const response = await Api.patch<Evidence>(`${controller}/Revert`, {evidenceId}, getBearerToken());
    const revertedEvidence = response.data;
    return revertedEvidence;
}

export const CommentEvidence = async (evidenceId: EvidenceId, comment: string) : Promise<Evidence> => {
    const response = await Api.post<Evidence>(`${controller}/Comment`, {evidenceId, comment}, getBearerToken());
    const commentedEvidence = response.data;
    return commentedEvidence;
}

export const DeleteEvidence = async (evidenceId: EvidenceId) : Promise<boolean> => {
    const response = await Api.delete<boolean>(`${controller}/Delete/${evidenceId.value}`, getBearerToken());
    const deletedEvidence = response.data;
    return deletedEvidence;
}
