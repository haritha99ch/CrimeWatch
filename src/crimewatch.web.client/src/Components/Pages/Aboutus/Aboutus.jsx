import React from "react";
import AboutImg from "../../../assets/about.jpg"
function Aboutus() {
  return (
    <div className=" grid grid-cols-1 sm:grid-cols-2 h-screen w-full pt-20 ">
          <div className="w-full h-full mb-0  bg-gray-900/90 absolute">
        <img src={AboutImg} 
        className="w-full h-full object-cover mix-blend-overlay"
        alt="img" />
          </div>
      <div className="hidden sm:block my-10 text-white relative">
        <div className="grid w-full h-[400px] object-cover my-32 px-3">
          <div className="grid md:grid-cols-3 gap-1 px-2 my-2 text-center bg-slate-50/10">
            <div className="border py-8 rounded-xl shadow-xl">
              <p className="text-6xl font-bold text-red-600">100%</p>
              <p className="text-white mt-2 text-3xl">secure</p>
            </div>
            <div className="border py-8 rounded-xl shadow-xl">
              <p className="text-6xl font-bold text-red-600">24/7</p>
              <p className="text-white mt-2 text-3xl">onboard</p>
            </div>
            <div className="border py-8 rounded-xl shadow-xl">
              <p className="text-6xl font-bold text-red-600">10/10</p>
              <p className="text-white mt-2 text-3xl">Ratings</p>
            </div>
            <div className="border py-8 rounded-xl shadow-xl">
              <p className="text-6xl font-bold text-red-600">24/7</p>
              <p className="text-white mt-2 text-3xl">onboard</p>
            </div>
            <div className="border py-8 rounded-xl shadow-xl">
              <p className="text-6xl font-bold text-red-600">24/7</p>
              <p className="text-white mt-2 text-3xl">onboard</p>
            </div>
            <div className="border py-8 rounded-xl shadow-xl">
              <p className="text-6xl font-bold text-red-600">24/7</p>
              <p className="text-white mt-2 text-3xl">onboard</p>
            </div>
          </div>
        </div>
      </div>
      <div className=" flex flex-col justify-center">
        <div
          className="max-w-[700px] h-[400px] mx-auto shadow-xl"
        >
          <div className="max-w-[700px] text-white relative mx-auto">

          <h2 className="text-4xl font-bold text-center py-6">CRIME.</h2>
          <p className="text-2xl">Lorem ipsum dolor sit amet consectetur, adipisicing elit. Nam, voluptates consequuntur. Vel sunt veniam ullam quaerat ex, pariatur atque, natus dolor reiciendis aspernatur inventore ducimus voluptates quae quasi, et numquam.</p>
          <p className="text-2xl">Lorem, ipsum dolor sit amet consectetur adipisicing elit. Incidunt recusandae ullam culpa consequatur expedita ea voluptates temporibus. Numquam dicta pariatur dolorum at ipsam voluptate quas aut quae, fuga, suscipit ullam.</p>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Aboutus;
