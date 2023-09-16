/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  theme: {
    screens: {
      sm: "640px",
      // => @media (min-width: 640px) {...}

      md: "740px",
      // => @media (min-width: 640px) {...}

      lg: "1024px",
      // => @media (min-width: 640px) {...}

      xl: "1280px",
      // => @media (min-width: 640px) {...}

      "2xl": "1536px",
      // => @media (min-width: 640px) {...}
    },
  },
  plugins: [],
  darkMode: "class"
};
