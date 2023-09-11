import { Status } from "../enums/Status";
import ModeratorId from "../valueObjects/ModeratorId";
import ReportId from "../valueObjects/ReportId";
import WitnessId from "../valueObjects/WitnessId";
import { Evidence } from "./Evidence";
import MediaItem from "./MediaItem";
import Moderator from "./Moderator";
import Witness from "./Witness";

export interface Report {
    reportId: ReportId;
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