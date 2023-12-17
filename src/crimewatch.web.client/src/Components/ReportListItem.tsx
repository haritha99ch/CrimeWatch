import {MdLocationPin} from "react-icons/md";
import Report from "../models/Report";
import {Status} from "../enums/Status";
import {Link} from "react-router-dom";

const ReportListItem = ({report}: { report: Report }) => {
    const statusElement: JSX.Element = (
        <>
            {report.status === Status.Pending && (
                <div className="bg-yellow-300 dark:bg-yellow-600 px-2 py-1 rounded-lg text-xs">
                    Pending
                </div>
            )}
            {report.status === Status.Approved && (
                <div className="bg-green-300 dark:bg-green-600 px-2 py-1 rounded-lg text-xs">
                    Approved
                </div>
            )}
            {report.status === Status.Declined && (
                <div className="bg-red-300 dark:bg-red-600 px-2 py-1 rounded-lg text-xs">
                    Declined
                </div>
            )}
            {report.status === Status.UnderReview && (
                <div className="bg-gray-300 dark:bg-gray-600 px-2 py-1 rounded-lg text-xs">
                    Under Review
                </div>
            )}
        </>
    );

    const content: JSX.Element = (
        <>
            <article
                className="group bg-gray-100 max-w-full border border-black/5 overflow-hidden max-h-[15rem] sm:pr-8 relative sm:h-[20rem] mb-3 sm:mb-8 last:mb-0 hover:bg-gray-50/5 transition rounded-lg dark:dark-mode-bg-secondary dark:dark-mode-text-primary">
                <Link to={`/Report/Details/${report.id.value}`}>
                    <div id="moderation-section">
                        <div className="flex justify-start">
                            {statusElement}{" "}
                            {report.moderator && (
                                <p>
                                    By {report.moderator?.user?.firstName}{" "}
                                    {report.moderator?.user?.lastName}
                                </p>
                            )}
                        </div>
                    </div>
                    <div className="pt-2 pb-7 px-5 sm:pl-10 sm:pr-2 sm:max-w-[50%] flex flex-col h-full">
                        <h3 className="text-2xl font-semibold z-50">{report.title}</h3>
                        <p className="mt-1 max-w-2xl text-sm leading-6 dark:dark-mode-text-tertiary z-50">
                            {new Date(report.dateTime).toLocaleString()}
                        </p>
                        <p className="text-sm leading-6 dark:dark-mode-text-tertiary flex items-center justify-start z-50">
                            {<MdLocationPin/>} {report.location.no},{" "}
                            {report.location.street1}, {report.location.street2},{" "}
                            {report.location.city}, {report.location.province}
                        </p>
                        <p className="mt-2 leading-relaxed sm:h-1/2 z-50">
                            {report.description}
                        </p>
                    </div>
                    <img
                        src={report.mediaItem?.url}
                        alt="dffd"
                        className="absolute top-8 -right-10 h-full group-hover:top-0 group-hover:-right-0 rounded-t-lg shadow-2xl -z-9 transition-all"
                    />
                </Link>
            </article>
        </>
    );
    return content;
};

export default ReportListItem;
