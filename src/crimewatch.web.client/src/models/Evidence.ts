import { Status } from "../enums/Status";
import EvidenceId from "../valueObjects/EvidenceId";
import ModeratorId from "../valueObjects/ModeratorId";
import ReportId from "../valueObjects/ReportId";
import WitnessId from "../valueObjects/WitnessId";
import MediaItem from "./MediaItem";
import Moderator from "./Moderator";
import Witness from "./Witness";
import Location from "./Location";

export interface UpdateEvidenceDto {
    id: EvidenceId;
    title: string;
    description: string;
    location: Location;
    mediaItems?: string;
    newMediaItems?: File[];
}

export interface CreateEvidenceDto {
    witnessId: WitnessId;
    reportId: ReportId;
    caption: string;
    description: string;
    location: Location;
    mediaItems: File[];
}

export default interface Evidence {
    id: EvidenceId;
    witnessId: WitnessId;
    moderatorId: ModeratorId | null;
    reportId: ReportId;
    title: string;
    description: string;
    dateTime: string;
    location: Location;
    status: Status;
    moderatorComment: string;
    moderator: Moderator | null;
    witness: Witness | null;
    mediaItems: MediaItem[];
}