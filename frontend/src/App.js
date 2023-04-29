import { BrowserRouter as Router, NavLink, Routes, Route, Navigate } from "react-router-dom";
import './App.css';
import { MainPage } from "./MainPage";
import { AboutPage } from "./AboutPage";
import { ContactPage } from "./ContactPage";
import { WhyPage } from "./WhyPage";
import { ServicesPage } from "./ServicesPage";
import { LogIn } from "./LogIn";
import { Registration } from "./Registration";


function App() {

  const user = JSON.parse(localStorage.getItem('user'));
  console.log(user);

  return (
    <Router>
      {/* NAVBAR */}
      <header>
        <h1>SAMIER</h1>
        <nav className="navbar navbar-expand-lg py-3 py-lg-0">
          <ul>
            <button
              className="navbar-toggler"
              name="gomb"
              aria-label="gomb"
              type="button"
              data-bs-toggle="collapse"
              data-bs-target="#navbarCollapse"
            >
              <span className="navbar-toggler-icon"></span>
            </button>
            <div className="collapse navbar-collapse" id="navbarCollapse">
              <div className="navbar-nav ms-3 py-0 pe-4">
                <NavLink to={'/'}><li>Főoldal</li></NavLink>
                <NavLink to={'/services'}><li>Szolgáltatásaink</li></NavLink>
                <NavLink to={'/why'}><li>Miért&nbsp;mi?</li></NavLink>
                <NavLink to={'/about'}><li>Rólunk</li></NavLink>
                <NavLink to={'/contact'}><li>Kapcsolat</li></NavLink>
                {user == null || user[0] == "" ? <div className="dropstart">
                  <button className="btn btn-dark" type="button" name='gomb' data-bs-toggle="dropdown" aria-expanded="false">
                    <i className="bi bi-person-circle"></i>
                  </button>
                  <ul className="dropdown-menu">
                    <NavLink to={'/login'}><li className="dropdown-item"><i className="bi bi-box-arrow-in-right"></i>&nbsp;&nbsp;Bejelentkezés</li></NavLink>
                    <NavLink to={'/registration'}><li className="dropdown-item"><i className="bi bi-person-add"></i>&nbsp;&nbsp;Regisztráció</li></NavLink>
                  </ul>
                </div> : <div className="dropstart">
                  <button className="btn btn-dark" type="button" data-bs-toggle="dropdown" name="gomb" aria-expanded="false">
                    <i className="bi bi-person-circle"></i>&nbsp;&nbsp;{user[1]}
                  </button>
                  <ul className="dropdown-menu">
                    <li className="dropdown-item" onClick={() => {
                      fetch(`https://localhost:6969/Logout/${user[0]}`, {
                        method: "POST",
                        headers: {
                          "Content-Type": "application/json",
                        }
                      })
                        .then((res) => {
                          localStorage.removeItem('user');
                          window.location.reload();
                          return res.text()
                        })
                        .then((data) => {
                          alert(data);
                        });
                    }}>
                      <i className=""></i>&nbsp;&nbsp;Kijelentkezés</li>
                    {/*</ul></><NavLink to={""}><li className="dropdown-item"><i className="bi bi-person-add"></i>&nbsp;&nbsp;Profil</li></NavLink>*/}

                  </ul>
                </div>
                }

              </div></div>
          </ul>
        </nav>
      </header>
      {/* NAVBAR vége */}

      <Routes>
        <Route path="/" exact element={<MainPage />} />
        <Route path="/about" exact element={<AboutPage />} />
        <Route path="/contact" exact element={<ContactPage />} />
        <Route path="/why" exact element={<WhyPage />} />
        <Route path="/services" exact element={<ServicesPage />} />
        <Route path="/login" exact element={<LogIn />} />
        <Route path="/registration" exact element={<Registration />} />
      </Routes>


      {/* FOOTER */}
      <footer>
        <div>
          <div className="container">
            <div className="row g-4" style={{ textAlign: 'center' }}>
              <div className="col-sm-12 col-lg-6" style={{ paddingTop: '5px' }}>
                <NavLink className='col-2' aria-label="facebook" to={'https://hu-hu.facebook.com/'} target='_blank'><i className="bi bi-facebook ikon"></i></NavLink>
                <NavLink className='col-2' aria-label="messenger" to={'https://www.messenger.com/'} target='_blank'><i className="bi bi-messenger"></i></NavLink>
                <NavLink className='col-2' aria-label="whatsapp" to={'https://www.whatsapp.com/?lang=hu'} target='_blank'><i className="bi bi-whatsapp"></i></NavLink>
                <NavLink className='col-2' aria-label="instagram" to={'https://www.instagram.com/'} target='_blank'><i className="bi bi-instagram"></i></NavLink>
                <NavLink className='col-2' aria-label="twitter" to={'https://twitter.com/'} target='_blank'><i className="bi bi-twitter"></i></NavLink>
                <NavLink className='col-2' aria-label="youtube" to={'https://www.youtube.com/'} target='_blank'><i className="bi bi-youtube"></i></NavLink>
              </div>
              <div className="col-lg-6" style={{ paddingTop: '5px', color: 'black' }}>
                <i className="bi bi-telephone-fill col-12">&nbsp;+3670/123-4567</i>&nbsp;&nbsp;&nbsp;&nbsp;
                <i className="bi bi-envelope-at-fill col-12">&nbsp;samier@gmail.com</i>&nbsp;&nbsp;&nbsp;
                <i className="bi bi-geo-alt-fill col-12">Miskolc, Fonoda utca 69.</i>
              </div>
            </div>
          </div>
        </div>

      </footer>
      {/* FOOTER vége */}

    </Router>

  );
}

export default App;
