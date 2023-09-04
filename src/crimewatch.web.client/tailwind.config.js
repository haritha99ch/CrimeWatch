export default {
  content: ["./src/**/*.{js,jsx,ts,tsx}"],
  theme: {
    extend: {
      colors:{
        "gray-20":"#F8F4EB",
        "gray-50":"#EFE6E6",
        "gray-100":"#DFCCCC",
        "gray-500":"#5E0000",
        "primary-100":"#EFE1E0",
        "primary-100":"#FFA6A3",
        "primary-300":"#FF6B66",
        "secondary-400":"#FFCD58",
        "secondary-500":"#FFCD58",

      },
      backgroundImage:(theme)=>({
        "gradient-yellowred":"linear-gradient(90deg,#FF616A 0%,#FFC837 100%)","mobile-home":"url('./assets/HomePageGraphic.png')"   
      }),
      fontFamily:{
        dmsans:["DM Sans","sans-serif"],
        montserrat:["Montserrat","sans-serif"]
      },
      content:{
        evolvetext:"url('./assets/EvolveText.png')",
        abstractwaves:"url('./assets/AbstractWave.png')",
        sparkle:"url('./assets/AbstractWave.png')",
        circles:"url('./assets/AbstractWave.png')",
      }
    },
    screens:{
      xs:"480px",
      sm:"768px",
      md:"1060px"
    }
  },
  plugins: [],
};
