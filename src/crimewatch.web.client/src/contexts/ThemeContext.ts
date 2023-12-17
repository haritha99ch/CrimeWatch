import {createContext} from "react"
import Theme from "../types/Theme"

type ThemeContextType = {
    theme: Theme;
    toggleTheme: () => void;
}

const ThemeContext = createContext<ThemeContextType | null>(null);

export default ThemeContext;