import logo from './logo.svg';
import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { Container } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css'
import Header from './layout/Header';
import Footer from './layout/Footer';
import Trip from './components/trip/Trip';

function App() {
  return (

    <>
      <Header />
      <BrowserRouter>
        <Container>
          <Routes>
            <Route path='/' element={<h1>Hello World!!!</h1>} />
            <Route path='/trip/list' element={<Trip />} />
          </Routes>
        </Container>
      </BrowserRouter>
      <Footer />
    </>
  );
}

export default App;
