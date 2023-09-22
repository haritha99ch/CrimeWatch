import Navbar from "./Components/Navbar";
import Home from "./Components/Home";
import About from "./Components/About";
import Support from "./Components/Support";
import { Route, Routes, BrowserRouter } from "react-router-dom";
import AccountSignUp from "./pages/account/SignUp";
import AccountIndex from "./pages/account/Index";
import ReportCreate from "./pages/report/Create";
import ThemeContextProvider from "./providers/ThemeContextProvider";
import ThemeSwitch from "./Components/ThemeSwitch";
import ReportIndex from "./pages/report/Index";
import ReportDetails from "./pages/report/Details";
import AccountSignIn from "./pages/account/SignIn";
import AuthenticationContextProvider from "./providers/AuthenticationContextProvider";

const App = () => {
  return (
    <>
      <AuthenticationContextProvider>
        <ThemeContextProvider>
          <BrowserRouter>
            <Navbar />
            <div className="dark:dark-mode-bg-primary min-h-screen">
              <Routes>
                <Route
                  path="/"
                  element={
                    <>
                      <Home />
                      <About />
                      <Support />
                    </>
                  }
                />
                <Route path="/Account/SignUp" element={<AccountSignUp />} />
                <Route path="/Account/SignIn" element={<AccountSignIn />} />
                <Route path="/Account/Index" element={<AccountIndex />} />
                <Route path="/Report/Create" element={<ReportCreate />} />
                <Route path="/Report/Index" element={<ReportIndex />} />
                <Route path="/Report/Details/:id" element={<ReportDetails />} />
              </Routes>
              <ThemeSwitch />
            </div>
          </BrowserRouter>
        </ThemeContextProvider>
      </AuthenticationContextProvider>
    </>
  );
};

export default App;
