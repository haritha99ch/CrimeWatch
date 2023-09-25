import React from "react";

function Support() {
  return (
    <div className="h-screen w-full ">
      <div className="h-screen w-full py-12 flex flex-col justify-center">
        <form action="" className="max-w-[600px] w-full mx-auto p-4 ">
          <h2>Contact Us</h2>
          <div className="flex flex-col py-2">
            <label>UserName</label>
            <input type="text" className="border p-2"/>
          </div>
          <div className="flex flex-col py-2">
            <label>Email Address</label>
            <input type="text" className="border p-2" />
          </div>
          <div className="flex flex-col py-2">
            <label>Contact Number</label>
            <input type="text" className="border p-2" />
          </div>
          <div className="flex flex-col py-2">
            <label>Message</label>
            <input type="textfield"  className="border p-2"/>
          </div>
          <button className="border w-full my-5 py-2 bg-red-600 hover:bg-red-500 text-white">Submit</button>
        </form>
      </div>
    </div>
  );
}

export default Support;
