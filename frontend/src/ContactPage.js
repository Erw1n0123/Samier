import React, { Navigate, useState, useEffect } from 'react';

export function ContactPage() {

  useEffect(() => {
    window.scrollTo({
      top: 0,
      behavior: "instant"
    })
  });

  const user = JSON.parse(localStorage.getItem('user'));

  return (
    <div style={{ minHeight: '100vh' }}>
      {/* Kapcsolat */}
      <div className='hatter' style={{ minHeight: '100vh' }}>
        <div className="container">
          <div className="row"><p></p>
            <h1 className="col-12 kapcsolat-szöveg">Kapcsolat</h1>
            <div className="col-8 m-auto">
              <form onSubmit={(event) => {
                event.persist();
                event.preventDefault();
                {/* ADATOK KÜLDÉSE */}
                fetch(`https://localhost:6969/Munka`, {
                  method: "POST",
                  headers: {
                    'Content-Type': 'application/json',
                  },
                  body: JSON.stringify({
                    MunkaTeljesNev: event.target.elements.teljesnev.value,
                    MunkaEmail: event.target.elements.email.value,
                    MunkaTelefonszam: event.target.elements.telefonszam.value,
                    MunkaIranyitoszam: event.target.elements.iranyitoszam.value,
                    MunkaTelepules: event.target.elements.telepules.value,
                    MunkaCim: event.target.elements.cim.value,
                    SzId: event.target.elements.szid.value,
                    MunkaLeiras: event.target.elements.leiras.value
                  }),
                })
                  .then(() => {
                    alert("Kész")
                  })
                  .catch(console.log);
              }}>
                {user == null || user[0] == "" ?
                <div>
                  <input type="text" className="kapcsolat-input" placeholder="Név" name="teljesnev" maxLength={50} required />
                  <input type="text" className="kapcsolat-input" placeholder="Email" name="email" maxLength={100} required />
                  <input type="number" className="kapcsolat-input" placeholder="Telefonszám" name="telefonszam" maxLength={11} required />
                  <input type="number" className="kapcsolat-input" placeholder="Irányítószám" name="iranyitoszam" maxLength={4} required />
                  <input type="text" className="kapcsolat-input" placeholder="Település" name="telepules" maxLength={50} required />
                  <input type="text" className="kapcsolat-input" placeholder="Lakcím" name="cim" maxLength={150} required />
                  <label type="text" placeholder="Szolgáltatás" name="szolgaltatasok" />
                  <select style={{ width: '100%', height: 'auto', padding: '20px 15px 10px 15px' }} name="szid" id="services">
                    <option value="1">- PORSZÍVÓZÁS</option>
                    <option value="2">- ABLAKTISZTÍTÁS</option>
                    <option value="3">- VASALÁS</option>
                    <option value="4">- MOSÁS</option>
                    <option value="5">- NAGYTAKARÍTÁS</option>
                    <option value="6">- ÁLTALÁNOS TAKARÍTÁS</option>
                  </select>
                  <textarea className="kapcsolat-uzenet" placeholder="megjegyzés / rövid leírás" rows={5} id="comment" name="leiras" defaultValue={""} maxLength={300} required />
                  <div className="kapcsolat-button col"><button type='submit'>Küld</button></div>
                </div> 
                :
                <div>
                <input type="text" className="kapcsolat-input" placeholder="Név" name="teljesnev" defaultValue={user[2]} maxLength={50} required />
                <input type="text" className="kapcsolat-input" placeholder="Email" name="email" defaultValue={user[3]} maxLength={100} required />
                <input type="number" className="kapcsolat-input" placeholder="Telefonszám" name="telefonszam" defaultValue={user[4]} maxLength={11} required />
                <input type="number" className="kapcsolat-input" placeholder="Irányítószám" name="iranyitoszam" defaultValue={user[5]} maxLength={4} required />
                <input type="text" className="kapcsolat-input" placeholder="Település" name="telepules" defaultValue={user[6]} maxLength={50} required />
                <input type="text" className="kapcsolat-input" placeholder="Lakcím" name="cim" defaultValue={user[7]} maxLength={150} required />
                <label type="text" placeholder="Szolgáltatás" name="szolgaltatasok" />
                <select style={{ width: '100%', height: 'auto', padding: '20px 15px 10px 15px' }} name="szid" id="services">
                  <option value="1">Porszívózás</option>
                  <option value="2">Ablaktisztítás</option>
                  <option value="3">Vasalás</option>
                  <option value="4">Mosás</option>
                  <option value="5">Nagytakarítás</option>
                  <option value="6">Általános takarítás</option>
                </select>
                <textarea className="kapcsolat-uzenet" placeholder="megjegyzés / rövid leírás" rows={5} id="comment" name="leiras" defaultValue={""} maxLength={300} required />
                <div className="kapcsolat-button col"><button type='submit'>Küld</button></div>
              </div> }
              </form>
            </div>
          </div>
        </div>
      </div>
      {/* Kapcsolat vége */}
    </div>
  )
}

export default ContactPage;