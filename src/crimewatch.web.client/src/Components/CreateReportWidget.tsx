import { AiFillFileAdd } from "react-icons/ai";
import { Link } from "react-router-dom";
import { UseAuthenticationContextProvider } from "../providers/AuthenticationContextProvider";

const CreateReportWidget = () => {
  const { currentUser } = UseAuthenticationContextProvider();

  const content: JSX.Element = (
    <>
      <Link to={currentUser ? "/Report/Create" : "/Account/SignIn"}>
        <button
          className="fixed top-24 right-5 bg-white h-[3rem] bg-opacity-80 backdrop-blur-[0.5rem] border border-white border-opacity-40 shadow-2xl rounded-full flex items-center px-5 justify-center hover:scale-[1.15] active:scale-105 transition-all dark:dark-mode-bg-secondary text-gray-900 dark:dark-mode-text-primary"
          type="button">
          <AiFillFileAdd className="text-2xl mr-2" />
          {currentUser ? "Create Report" : "SignIn to Create Report"}
        </button>
      </Link>
    </>
  );

  return content;
};

export default CreateReportWidget;
