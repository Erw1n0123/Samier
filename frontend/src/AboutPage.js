import React, { useState, useEffect } from 'react';


export function AboutPage() {

  useEffect(() => {
    window.scrollTo({
      top: 0,
      behavior: "instant"
    })
  });

  const [termekeks, setTermekeks] = useState([]);
  const [isFetchPending, setFetchPending] = useState(false);

  useEffect(() => {
    setFetchPending(true);
    fetch("https://localhost:6969/Raktar")
      .then((res) => res.json())
      .then((termekek) => setTermekeks(termekek))
      .catch(console.log)
      .finally(() => {
        setFetchPending(false);
      });
  }, []);

  return (
    <div style={{ minHeight: '100vh' }}>
      {/* RÓLUNK */}
      <div style={{ backgroundColor: 'rgb(236, 252, 255)', padding: '20px' }}>
        <div className="container">
          <div className="row" style={{ paddingTop: '30px' }}>
            <div className="col-md-6" style={{ padding: '10px' }}>
              <div><img src="images/img-2.webp" className="img-fluid" alt='Rólunk' /></div>
            </div>
            <div className="col-md-6">
              <h1><span>Rólunk </span> </h1>
              <p className="sotet">Szolgáltatásainkkal segítjük az ügyfeleket tisztán és rendezetten tartani irodáikat, otthonaikat vagy bármely más helyszínt. Az általunk nyújtott szolgáltatások közé tartozik a napi, heti vagy havonta ismétlődő takarítás, az ablaktisztítás és a speciális takarítási feladatok is. Célunk, hogy az ügyfelek elégedettek legyenek a munkánkkal, és minél kényelmesebbé tegyük számukra az életüket.</p>
            </div>
            <p className="ipsum_text sotet">Több olyan előnyünk is van, ami miatt érdemes minket választani a konkurencia helyett. Először is, csapatunk nagyon tapasztalt és megbízható, és mindig arra törekszünk, hogy magas minőségű szolgáltatást nyújtsunk az ügyfeleknek. Emellett rugalmasak vagyunk, és személyre szabott megoldásokat kínálunk, amelyek megfelelnek az ügyfelek egyedi igényeinek és követelményeinek. Áraink versenyképesek, és minden ügyféllel egyedi árajánlatot készítünk, így mindig az adott igényekre és költségvetésre szabott ajánlatot kapnak. Végül, de nem utolsósorban, szolgáltatásaink fenntarthatóak, és mindent megteszünk annak érdekében, hogy környezettudatosan takarítsunk, és kevesebb hulladékot termeljünk.</p>
          </div>
        </div>
      </div>
      <div className="product-grid">
        {/* RÓLUNK vége */}
        {/* TERMÉKEK */}
        <h1 className="cim"><span>Mi </span><span style={{ color: '#1f1f1f' }}>ezeket a </span><span>termékeket</span> <span style={{ color: '#1f1f1f' }}>használjuk</span></h1>
        <div className='container'>
          <div className='row'>
            {termekeks.map((termekek) => (
              <div className="product-card col-sm-3 col-lg-2" key={termekek.rid} style={{ textAlign: "center" }}>
                <h3>{termekek.nev}</h3>
                <img src={`data:image/webp;base64, ${termekek.kepfajl}`} alt="Termékek" className="hell"></img>
              </div>
            ))}
          </div>
        </div>
        {/* TERMÉKEK vége */}
      </div>
    </div>
  );
}

export default AboutPage;