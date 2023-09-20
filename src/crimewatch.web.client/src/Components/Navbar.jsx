import React, { useState } from "react";
import { MenuIcon, XIcon } from "@heroicons/react/outline";
import Logo from "../assets/Logo.png";
import { Link } from "react-router-dom";
function Navbar() {
  const [nav, setNav] = useState(false);
  const handleClick = () => setNav(!nav);

  return (
    <div className="w-screen h-[80px] z-10 bg-zinc-200 fixed drop-shadow-lg">
      <div className="px-2 flex justify-between items-center w-full h-full">
        <div className="flex items-center">
          <img src={Logo} alt="logo" className="w-13 h-10 mx-6" />
          <ul className="hidden md:flex">
            <li>
              <Link to="/">Home</Link>
            </li>
            <li>
              <Link to="/Aboutus">AboutUs</Link>
            </li>
            <li>
              <Link to="/Support">Support</Link>
            </li>
            <li>
              <Link>News</Link>
            </li>
          </ul>
        </div>
        <div className="hidden md:flex pr-4">
          <button className="border-none bg-transparent text-black mr-4">
            <Link to="/Signin">Signin</Link>
          </button>
          <Link to="/Signup">
            {" "}
            <button className="px-8 py-3">Signup</button>
          </Link>
        </div>
        <div className="md:hidden mr-4" onClick={handleClick}>
          {!nav ? <MenuIcon className="w-5" /> : <XIcon className="w-5" />}
        </div>
      </div>

      <ul className={!nav ? "hidden" : "absolute bg-zinc-200 w-full px-8"}>
        <li className="border-b-2 border-zinc-300 w-full">
          <Link to="/">Home</Link>
        </li>
        <li className="border-b-2 border-zinc-300 w-full">
          <Link to="/Aboutus">About us</Link>
        </li>
        <li className="border-b-2 border-zinc-300 w-full">
          <Link to="/Support">Contact</Link>
        </li>
        <li className="border-b-2 border-zinc-300 w-full">
          <Link>News</Link>
        </li>
        <div className="flex flex-col my-4">
          <button className="bg-transparent text-red-600 px-8 py-3 mb-4">
            <Link to="/Signin">SignIn</Link>
          </button>
          <button className="px-8 py-3">
            <Link to="/Signup">Signup</Link>
          </button>
        </div>
      </ul>
    </div>
  );
}

export default Navbar;
