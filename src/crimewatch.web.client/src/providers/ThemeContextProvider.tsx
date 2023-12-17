import {ReactNode, useContext, useEffect, useState} from "react";
import ThemeContext from "../contexts/ThemeContext";
import {getItem, saveItem} from "../services/LocalFileStorageService";
import Theme from "../types/Theme";

const ThemeContextProvider = (
    {children}: {
        children: ReactNode
    }) => {
    const [theme, setTheme] = useState<Theme>('light')

    const toggleTheme = () => {
        if (theme === 'light') {
            setTheme('dark');
            saveItem('theme', 'dark');
            addClassDark();
            return;
        }
        setTheme('light');
        saveItem('theme', 'light');
        removeClassDark();
    }

    useEffect(() => {
        const localTheme: Theme | null = getItem<Theme>('theme');

        if (localTheme) {
            setTheme(localTheme);
            localTheme === 'dark' ? addClassDark() : removeClassDark();
            return;
        }
        if (window.matchMedia('(prefers-color-scheme: dark)').matches) {
            setTheme('dark');
            saveItem('theme', 'dark');
            addClassDark();
        }

    }, []);

    const provider: JSX.Element = <>
        <ThemeContext.Provider value={{theme, toggleTheme}}>
            {children}
        </ThemeContext.Provider>
    </>
    return provider;
}

const removeClassDark = () =>
    document.documentElement.classList.remove('dark')

const addClassDark = () =>
    document.documentElement.classList.add('dark')
export default ThemeContextProvider

export const UseThemeContextProvider = () => {
    const context = useContext(ThemeContext);
    if (!context) throw new Error("UseThemeContextProvider must be used within ThemeContextProvider");
    return context;
}