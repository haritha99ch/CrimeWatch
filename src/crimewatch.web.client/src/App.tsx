import Navbar from './Components/Navbar'
import Home from './Components/Home'
import About from './Components/About'
import Support from './Components/Support'
import { Route, Routes, BrowserRouter } from 'react-router-dom';
import SignUp from './pages/account/SignUp';

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
        <Route path="/Account/SignUp" element={<SignUp/>}/>
      </Routes>
    </BrowserRouter>
    </>

  )
}

export default App
