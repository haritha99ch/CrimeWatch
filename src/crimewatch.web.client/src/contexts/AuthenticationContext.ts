import { createContext } from "react";
import Moderator from "../models/Moderator"
import Witness from "../models/Witness"

type AuthenticationContextType = {
    currentUser: Witness | Moderator | null;
    setCurrentUser: (user: Witness | Moderator | null) => void;
    currentUserLoading: boolean;
}

const AuthenticationContext = createContext<AuthenticationContextType | null>(null);

export default AuthenticationContext;