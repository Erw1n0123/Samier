import React, { useState, useEffect } from 'react';
import { useParams, NavLink } from 'react-router-dom';

export function MainPage() {

  useEffect(() => {
    window.scrollTo({
      top: 0,
      behavior: "instant"
    })
  });

  return (
    <div style={{ minHeight: '100vh' }}>
      {/* HEADER */}
      <div id="header">
        <div className="container">
          <div className="row">
            <div className="col-sm-5" id="header-bal" style={{ margin: 'auto' }}>
              <img src="images/logo.webp" className="img-fluid" alt='Samier logó'></img>
            </div>
            <div className="col-sm-5 d-none d-md-block" id="header-jobb" style={{ margin: 'auto' }}>
              <img src="images/img-1.webp" className="img-fluid" alt='Vállakozás logó' />
            </div>
          </div>
        </div></div>
      {/* HEADER vége*/}
      {/* RÓLUNK */}
      <div style={{ backgroundColor: 'rgb(236, 252, 255)', color: 'rgb(236, 252, 255)' }}>SAMIER</div>
      <div style={{ backgroundColor: 'rgb(236, 252, 255)', padding: '20px' }}>
        <div className="container">
          <div className="row">
            <div className="col-md-6">
              <div><img src="images/img-2.webp" className="img-fluid" alt='Rólunk' /></div>
            </div>
            <div className="col-md-6">
              <h1><span>Rólunk </span> </h1>
              <p className="sotet">Szolgáltatásainkkal segítjük az ügyfeleket tisztán és rendezetten tartani irodáikat, otthonaikat vagy bármely más helyszínt. Az általunk nyújtott szolgáltatások közé tartozik a napi, heti vagy havonta ismétlődő takarítás, az ablaktisztítás és a speciális takarítási feladatok is. Célunk, hogy az ügyfelek elégedettek legyenek a munkánkkal, és minél kényelmesebbé tegyük számukra az életüket.</p>
              <br />
              <NavLink to={'/about'}><a className="sotet"><b>Továbbiak</b><span style={{ padding: '10px' }}><img src="images/contact-icon1.webp" alt='nyíl' /></span></a></NavLink>
            </div>
          </div>
        </div>
      </div>
      {/* RÓLUNK vége */}
      {/* MIÉRT MI */}
      <div style={{ backgroundColor: 'rgb(236, 246, 255)', padding: '20px' }}>
        <div className="container">
          <h1 className="cim"><span>Miért </span><span style={{ color: '#1f1f1f' }}>minket válasszon?</span></h1>
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
            <NavLink to={'/why'}><a className="sotet"><b>Továbbiak</b><span style={{ padding: '10px' }}><img src="images/contact-icon1.webp" alt='nyíl' /></span></a></NavLink>
          </div>
        </div>
      </div>
      {/* MIÉRT MI vége */}
      {/* SZOLGÁLTATÁSAINK */}
      <h1 className="cim">Szolgáltatásaink</h1>
      <div style={{ padding: '20px' }}>
        <div className="container">
          <div className="row p-4">
            <div className="col-sm-5">
              <div><img src="images/img-4.webp" alt='Vasalás' className="img-fluid" /></div>
            </div>
            <div className="col-sm-7" style={{ margin: 'auto' }}>
              <h1 className="sotet">Vasalás</h1>
              <p className="sotet">A vasalás rendkívül idő és energia igényes. Egy nagyobb mosás esetén akár 3-4 órát is igénybe vehet a textíliák vasalása. Bizonyos ruhaneműknél speciális eszközökkel lehet csak végrehajtani. Minden tisztítási szolgáltatás tartalmazza a vasalást. Így nyújtva teljes körű megoldást a ruhatisztításra fokozva az Ön kényelmét. Válassza Ön is a teljes körű tisztítási szolgáltatásunkat! Fordítsa idejét fontosabb dolgokra!</p>
            </div>
          </div>
          <div className="row">
            <div className="col-sm-7" style={{ margin: 'auto' }}>
              <h1 className="sotet">Porszívózás</h1>
              <p className="sotet">Általában 2 munkanap alatt vállaljuk gépi gyártású szőnyegek (normál rövidszőrű, hosszúszőrű shaggy szőnyegek) sík-mélymosását, faliszőnyegek, rongyszőnyegek, gyapjűszőnyegek egyedi mosását, valamint kézi csomózású szőnyegek, kézi szőttes szőnyegek és keleti szőnyegek egyedi mosását, vagy kézi tisztítását, valamint antik szőnyegek kézi tisztítását!</p>
            </div>
            <div className="col-sm-5">
              <div><img src="images/img-5.webp" alt='Porszívózás' className="img-fluid" /></div>
            </div>
          </div>
          <div className="row">
            <div className="col-sm-5">
              <div><img src="images/img-6.webp" alt='Ablaktisztítás' className="img-fluid" /></div>
            </div>
            <div className="col-sm-7" style={{ margin: 'auto' }}>
              <h1 className="sotet">Ablaktisztítás</h1>
              <p className="sotet">Mindenki szeret tisztán látni, de elfáradni annál kevésbé.
                Kollégáink a napi rutinnak megfelelően, szakszerűen és gyorsan végzik munkájukat. Az ablak és üvegfelületek tisztításánál a legkorszerűbb technológiákat és vegyszereket használjuk, legyen szó modern, vagy régi ablakokról az eredmény tartós és szép lesz.
                Az ablaktisztítás során a keretek takarításáért nem számolunk fel külön árat.</p>
            </div>
          </div>
          <div className="row p-4">
            <div className="col-sm-7" style={{ margin: 'auto' }}>
              <h1 className="sotet">Mosás</h1>
              <p className="sotet">A ruhákat kis kapacitású gépeken jó minőségű környezetkímélő mosószerekkel és adalékanyagokkal mossuk. Mindvégig szem előtt tartjuk a textília jellemzőit és ennek megfelelően választjuk ki, a használt mosószereket, illetve a mosás és szárítás hőmérsékletét. Az alapos mosás után gondos kézi vasalás következik, amely során minden apró gyűrődést igyekszünk kisimítani. Az elkészült textíliákat higiénikus csomagolásban tároljuk és adjuk át tulajdonosaiknak.</p>
            </div>
            <div className="col-sm-5">
              <div><img src="images/img-7.webp" className="img-fluid" alt='Mosás' /></div>
            </div>
          </div>
          <div className="row">
            <div className="col-sm-5">
              <div><img src="images/kep.webp" className="img-fluid" alt='Nagytakarítás' /></div>
            </div>
            <div className="col-sm-7" style={{ margin: 'auto' }}>
              <h1 className="sotet">Nagytakarítás</h1>
              <p className="sotet">Egy átlagos háztartásban évente kétszer ajánlott a nagytakarítást megejteni. Nagyjából ez a 6 hónap az az időintervallum, ami alatt összegyűlik annyi por és szennyeződés a lakásban aminek a száműzéséhez már egy általános takarítás kevés. Nem csak a szabadon álló helyeket portalanítjuk le és töröljük tisztára, mint a napi (rutin) takarítás alkalmával, hanem minden tárgyat, bútort elmozdítunk a helyéről és alatta is, mögötte is kitakarítunk. Könyves polcok esetében lepakoljuk a polc teljes tartalmát (könyveket és a polcon található többi tárgyat), a könyveket leportalanítjuk, a csecsebecséket áttöröljük, majd a szekrény minden oldalát (beleértve a tetejét, alját és az összes polcot) megtisztítjuk.</p>
            </div>
          </div>
          <div className="row p-4">
            <div className="col-sm-7" style={{ margin: 'auto' }}>
              <h1 className="sotet">Általános takarítás</h1>
              <p className="sotet">Ha keveset volt otthon a héten, és egy hosszú munkanap után néha az utolsó dolog amit tenni akarunk, hogy körbe takarítsuk a lakást. De mi készségesen segítségedre leszünk a takarításban, így neked csak élvezned kell a csodás tisztaságot, amit magunk után hagyunk.</p>
            </div>
            <div className="col-sm-5">
              <div><img src="images/alt.webp" className="img-fluid szolgkep" alt='Általános takarítás' /></div>
            </div>
          </div>
        </div>
      </div>
      {/* SZOLGÁLTATÁSAINK vége */}
      {/* Kapcsolat */}
      <div style={{ backgroundColor: '#1f1f1f', paddingBottom: '30px' }}>
        <div className="container">
          <div className="row">
            <h1 className="col-12 kapcsolat-szöveg">Elérhetőségeink</h1>
            <div className="col-lg-6 col-sm-12">
              <div className="ratio ratio-16x9">
                <iframe name="térkép" title="Térkép" src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d10658.285130063214!2d20.82670116977539!3d48.099214399999994!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x4740a076435c425d%3A0x6907bfb26af86f88!2sMiskolc%2C%20Fonoda%20u.%2069%2C%203527!5e0!3m2!1shu!2shu!4v1679480389753!5m2!1shu!2shu" width={600} height={280} frameBorder={0} style={{ border: 0, width: '100%' }} allowFullScreen />
              </div>
            </div>
            <div className="col-lg-6 col-sm-12">
              <h1 className="kapcsolat-szöveg"><i className="bi bi-geo-alt-fill"></i> Miskolc, Fonoda utca 69.</h1>
              <h1 className="kapcsolat-szöveg"><i className="bi bi-telephone-fill"></i> +36 70/123-4567</h1>
              <h1 className="kapcsolat-szöveg"><i className="bi bi-envelope-at-fill"></i> samier@gmail.com</h1>
            </div>
          </div>
        </div>
      </div>
      {/* Kapcsolat vége */}
      </div>
  );
}

export default MainPage;