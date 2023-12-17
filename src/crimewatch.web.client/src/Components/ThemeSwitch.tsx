import {BsMoon, BsSun} from "react-icons/bs";
import {UseThemeContextProvider} from "../providers/ThemeContextProvider";

const ThemeSwitch = () => {
    const {theme, toggleTheme} = UseThemeContextProvider();

    const themeIcon = theme === "light" ? <BsSun/> : <BsMoon/>;

    const content: JSX.Element = <>
        <button className="theme-switcher"
                title="themeSwitch" type="button" onClick={toggleTheme}>
            {themeIcon}
        </button>
    </>
    return content;
}

export default ThemeSwitch