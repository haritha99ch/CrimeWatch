import Navbar from "./Components/Navbar";
import Home from "./Components/Home";
import Footer from "./Components/Footer";
import { Routes, Route } from "react-router-dom";
import Signin from "./Components/Pages/accounts/Signin";
import Signup from "./Components/Pages/accounts/Signup";
import Support from "./Components/Pages/Support/Support";
import Aboutus from "./Components/Pages/Aboutus/Aboutus";

const App = () => {
  return (
    <div>
      <Navbar />
      <div>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/Signin" element={<Signin />} />
        <Route path="/Signup" element={<Signup />} />
        <Route path="/Aboutus" element={<Aboutus />} />
        <Route path="/Support" element={<Support />} />
      </Routes>
      </div>
      <Footer />
    </div>
  );
};

export default App;
