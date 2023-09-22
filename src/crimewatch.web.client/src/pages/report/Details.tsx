import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { GetReportById } from "../../services/ReportService";
import Report from "../../models/Report";
import EvidenceListItem from "../../Components/EvidenceListItem";
import { UseAuthenticationContextProvider } from "../../providers/AuthenticationContextProvider";
import AddEvidenceModalType from "../../types/AddEvidenceModalType";
import AddEvidenceModal from "../../Components/AddEvidenceModal";
import Witness from "../../models/Witness";
import Evidence from "../../models/Evidence";
import { GetEvidenceForReport } from "../../services/EvidenceService";

const notfoundContent = (
  <>
    <div className="h-[80px]"></div>
    <div>Not Found</div>
  </>
);

const Details = () => {
  const { currentUser, currentUserLoading } =
    UseAuthenticationContextProvider();
  const { id } = useParams<{ id: string }>();
  const [report, setReport] = useState<Report | null>(null);
  const [evidences, setEvidences] = useState<Evidence[]>([]);
  const [notFound, setNotFound] = useState<boolean>(true);

  const CloseAddEvidenceModal = () => {
    setAddEvidenceModal(() => ({
      ...addEvidenceModal,
      isOpen: false,
    }));
  };
  const [addEvidenceModal, setAddEvidenceModal] =
    useState<AddEvidenceModalType>({
      isOpen: false,
      reportId: report?.id ?? null,
      witnessId: (currentUser as Witness)?.id ?? null,
      closeModal: CloseAddEvidenceModal,
      onSubmit: (evidence: Evidence) => {
        setEvidences(evidences => [...evidences, evidence]);
      },
    });

  const getReport = async () => {
    if (!id) return;
    const report = await GetReportById(id);
    if (!report) return;
    setReport(report);
    setNotFound(false);
  };

  const getEvidences = async () => {
    if (!id) return;
    const evidences = await GetEvidenceForReport({ value: id });
    if (!evidences) return;
    setEvidences(evidences);
  };

  useEffect(() => {
    getReport();
    getEvidences();
  }, [id]);

  const OpenAddEvidenceModal = () => {
    setAddEvidenceModal(() => ({
      ...addEvidenceModal,
      isOpen: true,
      reportId: report?.id ?? null,
      witnessId: (currentUser as Witness)?.id ?? null,
    }));
  };

  const RenderAddEvidenceControl = (): JSX.Element => {
    if (!currentUserLoading && !currentUser) {
      return (
        <>
          <Link to={"/Account/SignIn"}>
            <button className="px-3">SignIn to add Evidence</button>
          </Link>
        </>
      );
    }
    return (
      <>
        <button className="px-3" onClick={OpenAddEvidenceModal}>
          Add Evidence
        </button>
      </>
    );
  };

  const content: JSX.Element = (
    <>
      <div className="h-[80px]"></div>
      <div className="flex md:flex-row flex-col md:gap-4">
        <div
          className="basis-1/2 rounded-2xl border-gray-600/50 dark:border-gray-300/50 border-2 p-4 dark:dark-mode-text-primary"
          id="report-details">
          <div className="px-4 sm:px-0">
            <h3 className="text-base font-semibold leading-7 text-gray-900 dark:dark-mode-text-primary">
              {report?.title}
            </h3>
            <p>
              {report?.location.no}, {report?.location.street1},{" "}
              {report?.location.street2}, {report?.location.city},{" "}
              {report?.location.province}
            </p>
            <p className="mt-1 max-w-2xl text-sm leading-6 text-gray-500 dark:dark-mode-text-tertiary">
              {report && new Date(report.dateTime).toLocaleString()}
            </p>
          </div>
          <p>
            {report?.witness?.user?.firstName}
            {report?.witness?.user?.lastName}
          </p>
          <p>{report?.description}</p>
          <p>{report?.status}</p>
          <img
            className="w-full"
            src={report?.mediaItem?.url}
            alt={report?.title}
          />
          <p>
            Moderated By: {report?.moderator?.user?.firstName}{" "}
            {report?.moderator?.user?.lastName}
          </p>
        </div>
        <div
          className="rounded-2xl border-gray-600/50 dark:border-gray-300/50 border-2 p-4 basis-1/2"
          id="evidence-list">
          <div id="addEvidence-control">
            <h3 className="text-base font-semibold leading-7 text-gray-900 dark:dark-mode-text-primary flex gap-10 items-center">
              Evidences
              {RenderAddEvidenceControl()}
            </h3>
            <AddEvidenceModal modal={addEvidenceModal} />
          </div>
          {evidences.map(evidence => (
            <EvidenceListItem key={evidence.id.value} evidence={evidence} />
          ))}
        </div>
      </div>
    </>
  );

  return !notFound ? content : notfoundContent;
};

export default Details;
