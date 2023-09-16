import React,{useState} from "react";
import { MenuIcon, XIcon } from "@heroicons/react/outline";
import Logo from "../assets/Logo.png";
function Navbar() {

const [nav,setNav] = useState(false)
const handleClick = () => setNav(!nav)

  return (
    <div className="w-screen h-[80px] z-10 dark:dark-mode-bg-secondary bg-zinc-200 fixed drop-shadow-lg">
      <div className="px-2 flex justify-between items-center w-full h-full">
        <div className="flex items-center">

        <img src={Logo} alt="logo" className="w-13 h-10 mx-6" />
        <ul className="hidden md:flex dark:dark-mode-text-primary">
          <li>Home</li>
          <li>About Us</li>
          <li>Contact</li>
          <li>News</li>
        </ul>
        </div>
        <div className="hidden md:flex pr-4">
            <button className="border-none bg-transparent text-black dark:dark-mode-text-primary mr-4">SignIn</button>
            <button className="px-8 py-3">Signup</button>
        </div>
        <div className="md:hidden mr-4" onClick={handleClick}>
          {!nav ?  <MenuIcon className="w-5" />:<XIcon className="w-5"/>}
     
        </div>
      </div>

      <ul className={!nav ? 'hidden':'absolute bg-zinc-200 w-full px-8 dark:dark-mode-bg-secondary dark:dark-mode-text-primary'}>
        <li className="border-b-2 border-zinc-300 w-full">Home</li>
        <li className="border-b-2 border-zinc-300 w-full">About us</li>
        <li className="border-b-2 border-zinc-300 w-full">Contact</li>
        <li className="border-b-2 border-zinc-300 w-full">News</li>
        <div className="flex flex-col my-4">
            <button className="bg-transparent text-red-600 px-8 py-3 mb-4 dark:dark-mode-text-primary">SignIn</button>
            <button className="px-8 py-3">Signup</button>
        </div>
      </ul>
    </div>
  );
}

export default Navbar;
