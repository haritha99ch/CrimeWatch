import React, {useState} from "react";
import {MenuIcon, XIcon} from "@heroicons/react/outline";
import Logo from "../assets/Logo.png";
import {UseAuthenticationContextProvider} from "../providers/AuthenticationContextProvider";
import {SignOut} from "../services/AccountService";
import {Link} from "react-router-dom";

function Navbar() {
    const [nav, setNav] = useState(false);
    const {currentUser, setCurrentUser} = UseAuthenticationContextProvider();
    const handleClick = () => setNav(!nav);

    const RenderSessionControl = () => {
        return currentUser ? (
            <>
                <button className="border-none bg-transparent text-black dark:dark-mode-text-primary mr-4"
                        onClick={() => {
                            setCurrentUser(null);
                            SignOut();
                            window.location.href = "/Account/SignIn";
                        }}>
                    Sign Out
                </button>
                <Link to={"/Account/Index"}>
                    <button className="px-8 py-3">Account</button>
                </Link>
            </>
        ) : (
            <>
                <Link to={"/Account/SignIn"}>
                    <button className="border-none bg-transparent text-black dark:dark-mode-text-primary mr-4">
                        SignIn
                    </button>
                </Link>
                <Link to={"/Account/SignUp"}>
                    <button className="px-8 py-3">SignUp</button>
                </Link>
            </>
        );
    };

    return (
        <div className="w-screen h-[80px] z-10 dark:dark-mode-bg-secondary bg-zinc-200 fixed drop-shadow-lg">
            <div className="px-2 flex justify-between items-center w-full h-full">
                <div className="flex items-center">
                    <img src={Logo} alt="logo" className="w-13 h-10 mx-6"/>
                    <ul className="hidden md:flex dark:dark-mode-text-primary">
                        <Link to={"/"}>
                            <li>Home</li>
                        </Link>
                        <Link to={"/Report/Index"}>
                            <li>Reports</li>
                        </Link>
                        <Link to={"/#AboutUs"}>
                            <li>About Us</li>
                        </Link>
                        <li>Contact</li>
                        <li>News</li>
                    </ul>
                </div>
                <div className="hidden md:flex pr-4 items-center">{RenderSessionControl()}</div>
                <div className="md:hidden mr-4" onClick={handleClick}>
                    {!nav ? <MenuIcon className="w-5"/> : <XIcon className="w-5"/>}
                </div>
            </div>

            <ul
                className={
                    !nav
                        ? "hidden"
                        : "absolute bg-zinc-200 w-full px-8 dark:dark-mode-bg-secondary dark:dark-mode-text-primary"
                }>
                <li className="border-b-2 border-zinc-300 w-full">Home</li>
                <li className="border-b-2 border-zinc-300 w-full">About us</li>
                <li className="border-b-2 border-zinc-300 w-full">Contact</li>
                <li className="border-b-2 border-zinc-300 w-full">News</li>
                <div className="flex flex-col my-4 gap-4">{RenderSessionControl()}</div>
            </ul>
        </div>
    );
}

export default Navbar;
