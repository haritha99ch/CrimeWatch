import React from 'react'

function About() {
  return (
    <div className='w-full my-32' id='AboutUs'>
        <div className='max-w-[1240px] mx-auto'>
            <div className='text-center'> 
                <h2 className='text-5xl font-bold'>Trusted by the all victims</h2>
                <p className='text-3xl py-6 text-gray-500'>Lorem ipsum dolor, sit amet consectetur adipisicing elit. Beatae suscipit unde odio quaerat consectetur perferendis in! Odit culpa numquam animi. Culpa labore ullam autem incidunt ipsam. Incidunt in ad saepe.</p>
            </div>
            <div className='grid md:grid-cols-3 gap-1 px-2 text-center'>
                <div className='border py-8 rounded-xl shadow-xl'>
                    <p className='text-6xl font-bold text-red-600'>100%</p>
                    <p className='text-gray-400 mt-2'>secure
                    </p>
                </div >
                <div className='border py-8 rounded-xl shadow-xl'>
                    <p className='text-6xl font-bold text-red-600'>24/7</p>
                    <p className='text-gray-400 mt-2'>onboard
                    </p>
                </div>
                <div className='border py-8 rounded-xl shadow-xl'>
                    <p className='text-6xl font-bold text-red-600'>1000</p>
                    <p className='text-gray-400 mt-2'>solved
                    </p>
                </div>
            </div>
        </div>
        
    </div>
  )
}

export default About