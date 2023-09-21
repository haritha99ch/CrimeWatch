import { useEffect, useState } from "react";
import Report from "../../models/Report";
import { GetAllReports } from "../../services/ReportService";
import ReportListItem from "../../Components/ReportListItem";
import CreateReportWidget from "../../Components/CreateReportWidget";

const Index = () => {
  const [reports, setReports] = useState<Report[]>([]);
  useEffect(() => {
    GetReports();
  }, []);

  const content: JSX.Element = (
    <>
      <div className="h-[80px]"></div>
      <CreateReportWidget />
      <div className="dark:dark-mode-bg-primary">
        <div className="mx-auto p-0 md:p-4 w-full md:min-w-[42rem] md:w-1/2">
          {reports.map(report => {
            return <ReportListItem key={report.id.value} report={report} />;
          })}
        </div>
      </div>
    </>
  );
  const GetReports = async () => {
    const response = await GetAllReports();
    setReports(response);
  };

  return content;
};

export default Index;
