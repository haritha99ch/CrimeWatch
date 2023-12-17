import {useNavigate} from "react-router-dom";
import {UseAuthenticationContextProvider} from "../../providers/AuthenticationContextProvider";

const Index = () => {
    const {currentUser, currentUserLoading} =
        UseAuthenticationContextProvider();
    const navigate = useNavigate();
    if (!currentUserLoading && !currentUser) {
        navigate("/Account/SignIn");
    }

    if (!currentUser) {
        return (
            <>
                <div className="h-[80px]"></div>
                <div>Loading...</div>
                ;
            </>
        );
    }

    const content: JSX.Element = (
        <>
            <div className="h-[80px]"></div>
            <div className="mx-auto w-full p-0 md:p-4 md:mx-auto md:w-1/2 ">
                <div className="md:container md:mx-auto container mx-auto">
                    <div className="px-4 sm:px-0">
                        <h3 className="text-base font-semibold leading-7 dark:dark-mode-text-primary">
                            Account Information
                        </h3>
                        <p className="mt-1 max-w-2xl text-sm leading-6 text-gray-500 dark:dark-mode-text-tertiary">
                            Personal details.
                        </p>
                    </div>
                    <div className="mt-6 border-t border-gray-100">
                        <dl className="divide-y divide-gray-100">
                            <div className="description-list-group">
                                <dt className="description">Full name</dt>
                                <dd className="description-value">
                                    {currentUser.user?.firstName} {currentUser.user?.firstName}
                                </dd>
                            </div>
                            <div className="description-list-group">
                                <dt className="description">Gender</dt>
                                <dd className="description-value">
                                    {currentUser.user?.gender}
                                </dd>
                            </div>
                            <div className="description-list-group">
                                <dt className="description">Age</dt>
                                <dd className="description-value">{currentUser.user?.age}</dd>
                            </div>
                            <div className="description-list-group">
                                <dt className="description">Date Of Birth</dt>
                                <dd className="description-value">
                                    {currentUser.user?.dateOfBirth &&
                                        new Date(
                                            currentUser.user?.dateOfBirth
                                        ).toLocaleDateString()}
                                </dd>
                            </div>
                            <div className="description-list-group">
                                <dt className="description">Email</dt>
                                <dd className="description-value">
                                    {currentUser.account?.email}
                                </dd>
                            </div>
                            <div className="description-list-group">
                                <dt className="description">Phone Number</dt>
                                <dd className="description-value">
                                    {currentUser.user?.phoneNumber}
                                </dd>
                            </div>
                        </dl>
                    </div>
                </div>
            </div>
        </>
    );
    return content;
};
export default Index;
