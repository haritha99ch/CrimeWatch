import {useState,useEffect} from "react";
import {MenuIcon,XIcon} from "@heroicons/react/outline";
import Logo from "@/assets/Logo.png";


type Props = {}

const Navbar = (props: Props) => {

    const flexBetween ="flex items-center justify-between";//applying the tailwind css classes using this string

  return <nav>

    <div className={`${flexBetween} fixed top-0 z-30 w-full py-6`}>
        <div className={`${flexBetween} mx-auto w-5/6`}>
            <div className={`${flexBetween} w-full gap-16`}>
                {/* Left side */}
                <img src={Logo} alt="logo" />
                {/* Right side */}
                <div className={`${flexBetween} w-full`}>
                    <div className={`${flexBetween} gap-8 text-sm`}>
                        <p>Home</p>
                        <p>About</p>
                        <p>News</p>
                        <p>Contact Us</p>
                    </div>
                    <div >
                        <p>Sign In</p>
                        <button>Become a Member</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
  </nav> 
  
}

export default Navbar