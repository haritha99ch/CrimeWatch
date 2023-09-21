import Evidence from "../models/Evidence";
import ReportId from "../valueObjects/ReportId";
import WitnessId from "../valueObjects/WitnessId";

type AddEvidenceModalType = {
    isOpen: boolean;
    reportId: ReportId | null;
    witnessId: WitnessId | null;
    closeModal: () => void;
    onSubmit: (evidence: Evidence) => void;
}

export default AddEvidenceModalType;