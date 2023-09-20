import React from "react";
import { FaFacebook, FaGithub, FaTwitter } from "react-icons/fa";
function Footer() {
  return (
    <div className="w-full mt-24 bg-slate-900 text-gray-300 py- px-2 ">
      <div className="max-w-[1240px] mx-auto grid grid-col-2 md:grid-cols-6 border-b-2 border-gray-600 py-8">
        <div>
          <h6 className="font-bold uppercase pt-2">Solutions</h6>
          <ul>
            <li className="py-1">Create Account</li>
            <li className="py-1">three</li>
            <li className="py-1">three</li>
            <li className="py-1">Four</li>
          </ul>
        </div>
        <div>
          <h6 className="font-bold uppercase pt-2">Company</h6>
          <ul>
            <li className="py-1">About</li>
            <li className="py-1">Support</li>
            <li className="py-1">Signup</li>
            <li className="py-1">Signin</li>
          </ul>
        </div>
        <div>
          <h6 className="font-bold uppercase pt-2">Legal</h6>
          <ul>
            <li className="py-1">Privacy</li>
            <li className="py-1">Terms</li>
            <li className="py-1">Policies</li>
            <li className="py-1">Conditions</li>
          </ul>
        </div>
      </div>
      <div className="flex max-w-[1240px] m-auto justify-between sm:flex-row text-gray-500">
        <p>2023 Workflow,LLC All rights reserved</p>
        <div className="flex justify-between sm:max-w-[300px] pt-4 text-2xl">
            <FaFacebook/>
            <FaGithub/>
            <FaTwitter/>
        </div>
      </div>
    </div>
  );
}

export default Footer;
