import Navbar from './Components/Navbar'
import Home from './Components/Home'
import { Route, Routes, BrowserRouter } from 'react-router-dom';
import SignUp from './pages/account/SignUp';

const App = () => {
  return (
    <>
    <Navbar/>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home/>}/>
        <Route path="/Account/SignUp" element={<SignUp/>}/>
      </Routes>
    </BrowserRouter>
    </>
  )
}

export default App
