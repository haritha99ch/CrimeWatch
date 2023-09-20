import React from "react";
import { Link } from "react-router-dom";
import SignInImg from "../../../assets/account.jpg";
function Signin() {
  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 h-screen w-full">
      {/* <div className="grid grid-cols-1 h-screen w-full sm:grid-cols-2"> */}
      <div className="hidden sm:block">
        <img className="w-full h-full object-cover" src={SignInImg} alt="" />
      </div>
      <div className="bg-gray-100 flex flex-col justify-center">
        <form action="" className="max-w-[400px] w-full mx-auto bg-white p-4">
          <h2 className="text-4xl font-bold text-center py-6">CRIME.</h2>
          <div className="flex flex-col py-2">
            <label>UserName</label>
            <input className="border p-2" type="text" />
          </div>
          <div className="flex flex-col py-2">
            <label>Password</label>
            <input className="border p-2" type="password" />
          </div>
          <button className="border w-full my-5 py-2 bg-red-600 hover:bg-red-500 text-white">
            Sign in
          </button>
          <div className="flex justify-between">
            <p className="flex items-center">
              <input type="checkbox" className="mr-2" />
              Remember Me
            </p>
            <p>
              Create an{" "}
              <button className="border-none bg-transparent text-black">
                <Link to="/Signup">Account</Link>
              </button>
            </p>
          </div>
        </form>
      </div>
      {/* </div> */}
    </div>
  );
}

export default Signin;
