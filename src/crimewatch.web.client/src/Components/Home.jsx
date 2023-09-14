import React from "react";
import {
  UserCircleIcon,
  FolderOpenIcon,
  PhotographIcon,
  LocationMarkerIcon,
  OfficeBuildingIcon
} from "@heroicons/react/outline";
import backgroundImage from "../assets/banner.jpg";
function Home() {
  return (
    <div className="w-full h-screen bg-zinc-200 flex flex-col justify-between">
      <div className="grid md:grid-cols-2 max-w-[1240px] m-auto ">
        <div className="flex flex-col justify-center md:items-start w-full px-2 py-8">
          <p className="text-2xl">Unique Sequencing & Production</p>
          <h1 className="py-3 text-5xl md:text-7xl font-bold">
            Crime Management
          </h1>
          <p className="text-2xl">This is the new way of akwdj.</p>
          <button className="py-3 px-6 sm:w-[60%] my-4">Get started</button>
        </div>
        <div>
          <img className="w-full" src={backgroundImage} alt="img" />
        </div>
        <div className="absolute flex flex-col py-8 md:min-w-[760px] bottom-[5%] mx-1 md:left-1/2 transform md:-translate-x-1/2 bg-zinc-200 border border-slate-300 rounded-xl text-center shadow-xl">
          <p>Our  Services</p>
          <div className="flex justify-between flex-wrap px-4">
            <p className="flex px-4 py-2 text-slate-500"><UserCircleIcon className="h-6 text-red-600"/>Create an Account</p>
            <p className="flex px-4 py-2 text-slate-500"><FolderOpenIcon className="h-6 text-red-600"/>Create a Report</p>
            <p className="flex px-4 py-2 text-slate-500"><PhotographIcon className="h-6 text-red-600"/>Upload Evidence</p>
            <p className="flex px-4 py-2 text-slate-500"><LocationMarkerIcon className="h-6 text-red-600"/>Mark the Location</p>
            <p className="flex px-4 py-2 text-slate-500"><OfficeBuildingIcon className="h-6 text-red-600"/>Make it Happen</p>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Home;
