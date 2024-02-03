import logo from './logo.svg';
import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { Container } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css'
import Header from './views/layout/Header';
import Footer from './views/layout/Footer';

function App() {
  return (

    <>
      <Header />
      <BrowserRouter>
        <Container>
          <Routes>
            <Route path='/' element={<h1>Hello World!!!</h1>} />
          </Routes>
        </Container>
      </BrowserRouter>
      <Footer />
    </>
  );
}

export default App;
