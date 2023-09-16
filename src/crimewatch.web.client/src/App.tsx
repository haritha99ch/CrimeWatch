import Navbar from './Components/Navbar'
import Home from './Components/Home'
import About from './Components/About'
import Support from './Components/Support'
import { Route, Routes, BrowserRouter } from 'react-router-dom';
import AccountSignUp from './pages/account/SignUp';
import AccountIndex from './pages/account/Index';
import ReportCreate from './pages/report/Create';
import ThemeContextProvider from './providers/ThemeContextProvider';
import ThemeSwitch from './Components/ThemeSwitch';

const App = () => {
  return (
    <>
    <ThemeContextProvider>
      <Navbar/>
        <div className='dark:dark-mode-bg-primary'>
          <BrowserRouter>
            <Routes>
              <Route path="/" element={
                <>
                  <Home/>
                  <About/>
                  <Support/>
                </>
                }/>
              <Route path="/Account/SignUp" element={<AccountSignUp/>}/>
              <Route path="/Account/Index" element={<AccountIndex/>}/>
              <Route path="/Report/Create" element={<ReportCreate/>}/>
            </Routes>
          </BrowserRouter>
          <ThemeSwitch/>
        </div>
    </ThemeContextProvider>
    </>

  )
}

export default App
