import { Status } from "../enums/Status";
import EvidenceId from "../valueObjects/EvidenceId";
import ModeratorId from "../valueObjects/ModeratorId";
import ReportId from "../valueObjects/ReportId";
import WitnessId from "../valueObjects/WitnessId";
import MediaItem from "./MediaItem";
import Moderator from "./Moderator";
import Witness from "./Witness";

export interface Evidence {
    evidenceId: EvidenceId;
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