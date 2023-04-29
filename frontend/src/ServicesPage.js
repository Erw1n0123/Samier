import React, { useState, useEffect } from 'react';

export function ServicesPage() {

  const [szolg, setSzolg] = useState([]);
  const [isFetchPending, setFetchPending] = useState(false);

  useEffect(() => {
    window.scrollTo({
      top: 0,
      behavior: "instant"
    })
  });

  useEffect(() => {
    setFetchPending(true);
    fetch("https://localhost:6969/Szolgaltatas")
      .then((res) => res.json())
      .then((Szolg) => setSzolg(Szolg))
      .catch(console.log)
      .finally(() => {
        setFetchPending(false);
      });
  }, []);

  return (
    <div style={{ minHeight: '100vh' }}>
      {/* SZOLGÁLTATÁSAINK */}
      <h1 className="cim">Szolgáltatásaink</h1>
      <div style={{ padding: '20px' }}>
        <div className="container">
          {szolg.map((sz) => {
            if (sz.id % 2 == 0) {
              return (
                <div className="row">
                  <div className="col-sm-7 float-left" style={{ margin: 'auto' }}>
                    <h1 className="sotet">{sz.nev}</h1>
                    <p className="sotet">{sz.leiras}</p>
                  </div>
                  <div className="col-sm-5">
                    <div><img src={`data:image/webp;base64, ${sz.kepfajl}`} alt="Szolgáltatás" className="img-fluid szolgkep" /></div>
                  </div>
                </div>
              )
            }
            else {
              return (
                <div className="row">
                  <div className="col-sm-5">
                    <div className='float-right'><img src={`data:image/webp;base64, ${sz.kepfajl}`} alt="Szolgáltatás" className="img-fluid szolgkep" /></div>
                  </div>
                  <div className="col-sm-7" style={{ margin: 'auto' }}>
                    <h1 className="sotet">{sz.nev}</h1>
                    <p className="sotet">{sz.leiras}</p>
                  </div>
                </div>
              )}
          })}
        </div>
      </div>
      {/* SZOLGÁLTATÁSAINK vége */}
    </div>
  )

}

export default ServicesPage;