import React from "react";
import { PhoneIcon, ArrowSmRightIcon } from "@heroicons/react/outline";
import { SupportIcon, ShieldExclamationIcon } from "@heroicons/react/solid";
import SupportImg from "../assets/support.jpeg";
function Support() {
  return (
    <div className="w-full h-screen mt-24">
      <div className="w-full h-[720px] bg-gray-900/90 absolute">
        <img
          src={SupportImg}
          alt="img"
          className="w-full h-full object-cover mix-blend-overlay"
        />
      </div>
      <div className="max-w-[1240px] mx-auto text-white relative">
        <div className="px-4 py-12">
          <h2 className="text-3xl pt-2 text-slate-300 uppercase text-center">
            Support
          </h2>
          <h3 className="text-5xl font-bold py-6 text-center">
            Finding the team
          </h3>
          <p className="py-4 text-3xl text-slate-300">
            Lorem ipsum dolor sit amet consectetur adipisicing elit. Nesciunt
            unde, necessitatibus aspernatur aliquam esse ratione rem ut
            architecto assumenda temporibus distinctio iure sunt odio alias sint
            molestiae explicabo, vel officiis.
          </p>
        </div>
        <div className="grid grid-cols-1 lg:grid-cols-3 relative gap-x-8 gap-y-16 px-4 pt-12 sm:pt-20 text-black">
          <div className="bg-white rounded-xl shadow-2xl">
            <div className="p-8">
              <PhoneIcon className="w-16 p-4 bg-red-600 text-white rounded-lg mt-[-4rem]" />
              <h3 className="font-bold text-2xl my-6">ikajdik</h3>
              <p className="text-gray-600 text-xl">
                Lorem ipsum dolor sit amet consectetur, adipisicing elit. Esse,
                et, voluptas veniam earum minima, voluptatem adipisci quaerat
                error cupiditate repelle.
              </p>
            </div>
          <div className="bg-slate-100 pl-8 py-4">
            <p className="flex items-center text-red-600">
              Contact Us
              <ArrowSmRightIcon className="w-5 ml-2" />
            </p>
          </div>
          </div>
          <div className="bg-white rounded-xl shadow-2xl">
            <div className="p-8">
              <SupportIcon className="w-16 p-4 bg-red-600 text-white rounded-lg mt-[-4rem]" />
              <h3 className="font-bold text-2xl my-6">ikajdik</h3>
              <p className="text-gray-600 text-xl">
                Lorem ipsum dolor sit amet consectetur, adipisicing elit. Esse,
                et, voluptas veniam earum minima, voluptatem adipisci quaerat
                error cupiditate repelle.
              </p>
            </div>
          <div className="bg-slate-100 pl-8 py-4">
            <p className="flex items-center text-red-600">
              Contact Us
              <ArrowSmRightIcon className="w-5 ml-2" />
            </p>
          </div>
          </div>
          <div className="bg-white rounded-xl shadow-2xl">
            <div className="p-8">
              <ShieldExclamationIcon className="w-16 p-4 bg-red-600 text-white rounded-lg mt-[-4rem]" />
              <h3 className="font-bold text-2xl my-6">ikajdik</h3>
              <p className="text-gray-600 text-xl">
                Lorem ipsum dolor sit amet consectetur, adipisicing elit. Esse,
                et, voluptas veniam earum minima, voluptatem adipisci quaerat
                error cupiditate repelle.
              </p>
            </div>
          <div className="bg-slate-100 pl-8 py-4">
            <p className="flex items-center text-red-600">
              Contact Us
              <ArrowSmRightIcon className="w-5 ml-2" />
            </p>
          </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Support;
