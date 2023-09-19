import { useEffect, useState } from "react"
import { useParams } from "react-router-dom"
import { GetReportById } from "../../services/ReportService"
import Report from "../../models/Report"

const notfoundContent = <>
<div className="h-[80px]"></div>
<div>Not Found</div>
</>

const Details = () => {
    const { id } = useParams<{ id: string }>()
    const [report, setReport] = useState<Report | null>(null);
    const [notFound, setNotFound] = useState<boolean>(true);
    
    const getReport = async () => {
      const report = await GetReportById(id!);
      if(report) {
        setReport(report);
        setNotFound(false);
      }
    }

    useEffect(() => {
      if(!id) {
        setNotFound(true);
        return;
      }
      getReport();
    }, [])


    const content : JSX.Element = <>
    <div className="h-[80px]"></div>
    <div className="flex flex-row">
      <div className="w-1/2" id="report-details">
        <div className="px-4 sm:px-0">
          <h3 className="text-base font-semibold leading-7 text-gray-900 dark:dark-mode-text-primary">{report?.title}</h3>
          <p className="mt-1 max-w-2xl text-sm leading-6 text-gray-500 dark:dark-mode-text-tertiary">{report && new Date(report.dateTime).toLocaleString()}</p>
        </div>
          <p>{report?.witness?.user?.firstName}{report?.witness?.user?.lastName}</p>
          <p>{report?.description}</p>
          <p>{report?.status}</p>
      </div>
      <div id="evidence-list">
      {report?.evidences.map((evidence) => {
                return <>
                <p key={evidence.id.value}>{evidence.title}</p>
                <p>{new Date(evidence.dateTime).toLocaleString()}</p>
                <p>{evidence.witness?.user?.firstName} {evidence.witness?.user?.lastName}</p>
                <p>{evidence.description}</p>
                <p>{evidence.status}</p>
                </>
            })}
      </div>
    </div>
    </>;
  return !notFound ? content: notfoundContent
}

export default Details