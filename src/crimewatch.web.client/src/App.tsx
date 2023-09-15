import Navbar from './Components/Navbar'
import Home from './Components/Home'
import About from './Components/About'
import Support from './Components/Support'
import { Route, Routes, BrowserRouter } from 'react-router-dom';
import AccountSignUp from './pages/account/SignUp';
import AccountIndex from './pages/account/Index';

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
      </Routes>
    </BrowserRouter>
    </>

  )
}

export default App
