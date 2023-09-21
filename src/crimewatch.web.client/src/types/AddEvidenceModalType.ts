import Evidence from "../models/Evidence";
import ReportId from "../valueObjects/ReportId";

type AddEvidenceModalType = {
    isOpen: boolean;
    reportId: ReportId | null;
    newEvidence: Evidence | null;
    closeModal: () => void;
}

export default AddEvidenceModalType;