
import {BrowserRouter, Route, Routes} from 'react-router-dom'
import Home from './pages/components/home/Home';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from './pages/layouts/header/Header';
import Foot from './pages/layouts/footer/Foot';
import { Container } from 'react-bootstrap';

const App = () => {
  return(
    <BrowserRouter>
      <Header />
      <Container>

        <Routes>
          <Route element={<Home />} path='/' />
        </Routes>

        
      </Container>
      <Foot />
    </BrowserRouter>
  )
}

export default App;
