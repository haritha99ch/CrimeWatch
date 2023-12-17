import {useContext, useEffect, useState} from "react";
import AuthenticationContext from "../contexts/AuthenticationContext";
import Witness from "../models/Witness";
import Moderator from "../models/Moderator";
import {GetCurrentUser as GetCurrentUserFromApi} from "../services/AccountService";

const AuthenticationContextProvider = ({
                                           children,
                                       }: {
    children: React.ReactNode;
}) => {
    const [currentUser, setCurrentUser] = useState<Witness | Moderator | null>(
        null
    );
    const [currentUserLoading, setCurrentUserLoading] = useState<boolean>(true);

    const GetCurrentUser = async () => {
        const user = await GetCurrentUserFromApi();
        setCurrentUser(user);
        setCurrentUserLoading(false);
    };

    useEffect(() => {
        GetCurrentUser();
    }, []);

    const provider: JSX.Element = (
        <>
            <AuthenticationContext.Provider value={{currentUser, setCurrentUser, currentUserLoading}}>
                {children}
            </AuthenticationContext.Provider>
        </>
    );
    return provider;
};

export const UseAuthenticationContextProvider = () => {
    const context = useContext(AuthenticationContext);
    if (!context)
        throw new Error(
            "UseAuthenticationContextProvider must be used within AuthenticationContextProvider"
        );
    return context;
};

export default AuthenticationContextProvider;
