import React, { useState, useEffect } from 'react';

export function WhyPage() {
  useEffect(() => {
    window.scrollTo({
      top: 0,
      behavior: "instant"
    })
  });
  return (
    <div style={{ minHeight: '100vh' }}>
      {/* MIÉRT MI */}
      <div style={{ backgroundColor: 'rgb(236, 246, 255)', padding: '20px' }}>
        <h1 className="cim" style={{ marginBottom: '5px' }}><span>Miért </span><span style={{ color: '#1f1f1f' }}>minket válasszon?</span></h1>
        <p className="sotet" style={{ textAlign: 'center', width: '100%', paddingBottom: '120px' }}>Több olyan előnyünk is van, ami miatt érdemes minket választani a konkurencia helyett. Először is, csapatunk nagyon tapasztalt és megbízható, és mindig arra törekszünk, hogy magas minőségű szolgáltatást nyújtsunk az ügyfeleknek. Emellett rugalmasak vagyunk, és személyre szabott megoldásokat kínálunk, amelyek megfelelnek az ügyfelek egyedi igényeinek és követelményeinek. Áraink versenyképesek, és minden ügyféllel egyedi árajánlatot készítünk, így mindig az adott igényekre és költségvetésre szabott ajánlatot kapnak. Végül, de nem utolsósorban, szolgáltatásaink fenntarthatóak, és mindent megteszünk annak érdekében, hogy környezettudatosan takarítsunk, és kevesebb hulladékot termeljünk.</p>
        <div className="container">
          <div className="row">
            <div className="col-lg-3 col-sm-6">
              <div className="box">
                <h1 className="box-szam">1000+</h1>
                <h4 className="box-szoveg">Ügyfél</h4>
              </div>
            </div>
            <div className="col-lg-3 col-sm-6">
              <div className="box">
                <h1 className="box-szam">980+</h1>
                <h4 className="box-szoveg">Elégedett ügyfél</h4>
              </div>
            </div>
            <div className="col-lg-3 col-sm-6">
              <div className="box">
                <h1 className="box-szam">100+</h1>
                <h4 className="box-szoveg">Támogató</h4>
              </div>
            </div>
            <div className="col-lg-3 col-sm-6">
              <div className="box">
                <h1 className="box-szam">50+</h1>
                <h4 className="box-szoveg">Alkalmazott</h4>
              </div>
            </div>
            <p className="sotet" style={{ textAlign: 'center', width: '100%', padding: '50px' }}>Vállalkozásunk célja, hogy az ügyfelek meggyőződjenek arról, hogy a vállalkozásunk a legjobb választás az adott piaci szegmensben. Ezért feltüntetjük eddigi ügyfeleink, elégedett ügyfeleink, támogatóink és alkalmazottaink számát.</p>
          </div>
        </div>
      </div>
      {/* MIÉRT MI vége */}
      <h1 className="cim" style={{ marginBottom: '5px' }}><span>Néhány </span><span style={{ color: '#1f1f1f' }}>referenciánk</span></h1>
      <p className="sotet" style={{ textAlign: 'center', width: '100%', paddingBottom: '120px' }}></p>
      {/* VIDEÓK */}
      <div className="container">
        <div className="row">
          <div className="col-lg-6 col-sm-12">
            <div >
              <iframe width="100%" height="315" src="https://www.youtube.com/embed/WZA5gAYpI5U" alt="Referencia videó" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>
            </div>
          </div>
          <div className="col-lg-6 col-sm-12">
            <div>
              <iframe width="100%" height="315" src="https://www.youtube.com/embed/ESj9mMTl0UI" alt="Referencia videó" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>
            </div>
          </div>

        </div>
      </div>
      {/* KÉPEK */}
      <div className="container">
        <div className="row">
          <div className="col-lg-4 col-sm-12">
            <div className="box-kepek">
              <img src="images/ba1.webp" alt="Referencia kép" style={{ width: '100%', height: '300px' }}></img>
            </div>
          </div>
          <div className="col-lg-4 col-sm-12">
            <div className="box-kepek">
              <img src="images/ba2.webp" alt="Referencia kép" style={{ width: '100%', height: '300px' }}></img>
            </div>
          </div>
          <div className="col-lg-4 col-sm-12">
            <div className="box-kepek">
              <img src="images/ba3.webp" alt="Referencia kép" style={{ width: '100%', height: '300px' }}></img>
            </div>
          </div>
          <p></p>
          <div className="col-lg-4 col-sm-12">
            <div className="box-kepek">
              <img src="images/ba4.webp" alt="Referencia kép" style={{ width: '100%', height: '300px' }}></img>
            </div>
          </div>
          <div className="col-lg-4 col-sm-12">
            <div className="box-kepek">
              <img src="images/ba5.webp" alt="Referencia kép" style={{ width: '100%', height: '300px' }}></img>
            </div>
          </div>
          <div className="col-lg-4 col-sm-12">
            <div className="box-kepek">
              <img src="images/ba6.webp" alt="Referencia kép" style={{ width: '100%', height: '300px' }}></img>
            </div>
          </div>
          <p></p>
          <div className="col-lg-4 col-sm-12">
            <div className="box-kepek">
              <img src="images/ba7.webp" alt="Referencia kép" style={{ width: '100%', height: '300px' }}></img>
            </div>
          </div>
          <div className="col-lg-4 col-sm-12">
            <div className="box-kepek">
              <img src="images/ba8.webp" alt="Referencia kép" style={{ width: '100%', height: '300px' }}></img>
            </div>
          </div>
          <div className="col-lg-4 col-sm-12">
            <div className="box-kepek">
              <img src="images/ba9.webp" alt="Referencia kép" style={{ width: '100%', height: '300px' }}></img>
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}

export default WhyPage;