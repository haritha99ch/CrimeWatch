import {AxiosRequestConfig} from "axios";
import Api from "../configurations/ApiConfiguration";
import Report, {CreateReportDto, UpdateReportDto} from "../models/Report";
import ModeratorId from "../valueObjects/ModeratorId";
import ReportId from "../valueObjects/ReportId";
import {getBearerToken, getToken} from "./AuthenticationService";

const controller = '/api/Report';

export const CreateReport = async (report: CreateReportDto): Promise<Report | null> => {
    const config: AxiosRequestConfig = {
        headers: {
            'Content-Type': 'multipart/form-data',
            Authorization: `bearer ${getToken()}`
        }
    };
    const response = await Api.post<Report>(`${controller}/Create`, report, config).catch(() => null);
    if (!response) return null;
    const createdReport: Report = response.data;
    return createdReport;
}

export const GetAllReports = async (): Promise<Report[]> => {
    const response = await Api.get<Report[]>(`${controller}/Get`, getBearerToken());
    const reports: Report[] = response.data;
    return reports;
}

export const GetReportById = async (id: string): Promise<Report> => {
    const response = await Api.get<Report>(`${controller}/Get/${id}`, getBearerToken());
    const report: Report = response.data;
    return report;
}

export const UpdateReport = async (report: UpdateReportDto): Promise<Report> => {
    const response = await Api.patch<Report>(`${controller}/Update`, report, getBearerToken());
    const updatedReport: Report = response.data;
    return updatedReport;
}

export const ModerateReport = async (reportId: ReportId, moderatorId: ModeratorId): Promise<Report> => {
    const response = await Api.patch<Report>(`${controller}/Moderate`, {reportId, moderatorId}, getBearerToken());
    const moderatedReport: Report = response.data;
    return moderatedReport;
}

export const ApproveReport = async (reportId: ReportId): Promise<Report> => {
    const response = await Api.patch<Report>(`${controller}/Approve`, {reportId}, getBearerToken());
    const approvedReport: Report = response.data;
    return approvedReport;
}

export const DeclineReport = async (reportId: ReportId): Promise<Report> => {
    const response = await Api.patch<Report>(`${controller}/Decline`, {reportId}, getBearerToken());
    const declinedReport: Report = response.data;
    return declinedReport;
}

export const RevertReport = async (reportId: ReportId): Promise<Report> => {
    const response = await Api.patch<Report>(`${controller}/Revert`, {reportId}, getBearerToken());
    const revertedReport: Report = response.data;
    return revertedReport;
}

export const AddCommentToReport = async (reportId: ReportId, comment: string): Promise<Report> => {
    const response = await Api.post<Report>(`${controller}/Comment`, {reportId, comment}, getBearerToken());
    const commentedReport: Report = response.data;
    return commentedReport;
}

export const DeleteReport = async (reportId: ReportId): Promise<boolean> => {
    const response = await Api.delete<boolean>(`${controller}/Delete/${reportId.value}`, getBearerToken());
    const deletedReport: boolean = response.data;
    return deletedReport;
}