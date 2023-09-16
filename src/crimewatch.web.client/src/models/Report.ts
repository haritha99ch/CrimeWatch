import { Status } from "../enums/Status";
import ModeratorId from "../valueObjects/ModeratorId";
import ReportId from "../valueObjects/ReportId";
import WitnessId from "../valueObjects/WitnessId";
import Evidence from "./Evidence";
import MediaItem from "./MediaItem";
import Moderator from "./Moderator";
import Witness from "./Witness";
import Location from "./Location";

export interface CreateReportDto {
    witnessId: WitnessId;
    title: string;
    description: string;
    location: Location;
    mediaItem?: File;
}

export interface UpdateReportDto {
    id: ReportId;
    title: string;
    description: string;
    location: Location;
    mediaItem?: MediaItem;
    newMediaItem?: File;
}

export default interface Report {
    id: ReportId;
    witnessId: WitnessId;
    moderatorId: ModeratorId | null;
    title: string;
    description: string;
    dateTime: string;
    location: Location;
    status: Status;
    staredBy: WitnessId[];
    moderatorComment: string;
    witness: Witness | null;
    moderator: Moderator | null;
    mediaItem: MediaItem | null;
    evidences: Evidence[];
}