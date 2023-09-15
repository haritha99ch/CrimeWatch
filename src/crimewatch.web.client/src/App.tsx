import Navbar from './Components/Navbar'
import Home from './Components/Home'
import About from './Components/About'
import Support from './Components/Support'
import { Route, Routes, BrowserRouter } from 'react-router-dom';
import AccountSignUp from './pages/account/SignUp';
import AccountIndex from './pages/account/Index';
import ReportCreate from './pages/report/Create';

const App = () => {
  return (
    <>
    <Navbar/>
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
    </>

  )
}

export default App
