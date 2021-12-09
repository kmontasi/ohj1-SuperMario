using Jypeli;
using Jypeli.Widgets;
using System;
using System.Collections.Generic;



public class Alpha : PhysicsGame
{

    private const int RUUDUN_KOKO = 40;// NÄYTYÖN KOKO
    ///-----------------------------------------------------------------------------------------------------///
    ///PELAAJAN KAIKKI ASETUKSET!!!
    //PELAAJAMALLIN ASETUKSET
    //PELAAJAN NIMI !!TÄMÄ PYSYY AINA SAMANA ELLEI LISÄTÄ UUTTA PELAAJAA
    private PlatformCharacter pelaaja1;
    //PELAAJAN ASETUKSET
    private const double NOPEUS = 100;
    private const double HYPPYNOPEUS = 600;
    ///Pelaajan Animaatiot ja Grafiikat
    private Image pelaajanKuva = LoadImage("PlumberB.png"); ///Pelaajan STATIC animaatio
    private Image pelaajanKuvaHyppy = LoadImage("PlumberHyppy.png");///Pelaaajan hyppy animaatio


    ///-----------------------------------------------------------------------------------------------------///
    //TASON GRAFIIKAT
    private Image TasoRuohoKuva = LoadImage("TasoRuoho.png");  //RUOHO
    private Image PutkiAlasKuva = LoadImage("Putkialas.png");  //Putki ALAS
                                                               //Putki Start ylös
    private Image TiiliTasoKuva = LoadImage("tiilitaso.png");  //Tiilitaso
    private Image KiviKuva = LoadImage("kivi.png");            //KiviTaso
    ///-----------------------------------------------------------------------------------------------------///


    //INTERAKTIIVISET ESINEET SEKÄ POWERUPIT

    //tahti !!Vaihda
    private Image tahtiKuva = LoadImage("tahti.png");

    //elämät
    private Image SydanKuva = LoadImage("sydan.png");
    ///-----------------------------------------------------------------------------------------------------///

    //NEGATIIVISET OLIOT

    //satuttaa pelaajaa ja ottaa yhden dyfämen pois
    private Image pomminKuva = LoadImage("Pommi.png");//POMMI  kUVA'

    //POMMIN TO DO !!! RÄJÄHDUS ANIMAATIO
    private Image loppuLippu = LoadImage("lippu.png");//POMMI  kUVA'
    //POMMISTA TULEVAT SAVUT

    //VIHOLLISET

    //Vihollinen pieniapina
    /// <summary>
    /// ampuu banaaneja alaspäin
    /// </summary>
    /// 

    /// VPienen apinan banaani



    private Image vihollisenPieniApina = LoadImage("pieniapina.png");
    private Image lamauttavaBanaani = LoadImage("lamauttavabanaani.png");
    //Vihollinen tulijhahmo
    private Image vihollisenKuvaTulihahmo = LoadImage("vihollinen.png");//VIHOLLINEN YKSI (TULIHAHMO
    private Image tulipalloKuva = LoadImage("tulipallo.png");//VIHOLLINEN YKSI TULIPALLO ORDNANCE (TULIHAHMO)
    private Image[] KongApina = LoadImages("kongapina", "kongapinaleft");
    private Image Boulder = LoadImage("boulder.png");
    ///-----------------------------------------------------------------------------------------------------///
    ///


    //ALOITUS JA MAIN
    public override void Begin()
    {
        Valikko();
        
    }
    List<Label> valikonKohdat;
   private void Valikko()
    { 
        Label otsikko = new Label("Pupuhuhdan Plumberman"); // Luodaan otsikko
        otsikko.Y = 100; // Otsikko on hieman valikonkohtien yläpuolella
        otsikko.Font = new Font(40, true); // Otsikon teksti on suurempi ja boldattu
        Add(otsikko);

        valikonKohdat = new List<Label>(); // Alustetaan lista, johon valikon kohdat tulevat

        Label kohta1 = new Label("Aloita uusi peli");  // Luodaan uusi Label-olio, joka toimii uuden pelin aloituskohtana
        kohta1.Position = new Vector(0, 40);  // Asetetaan valikon ensimmäinen kohta hieman kentän keskikohdan yläpuolelle
        valikonKohdat.Add(kohta1);  // Lisätään luotu valikon kohta listaan jossa kohtia säilytetään


        Label kohta3 = new Label("Lopeta peli");
        kohta3.Position = new Vector(0, 0);
        valikonKohdat.Add(kohta3);
        // Lisätään kaikki luodut kohdat peliin foreach-silmukalla
        foreach (Label valikonKohta in valikonKohdat)
        {

            Add(valikonKohta);
        }

        Mouse.ListenOn(kohta1, MouseButton.Left, ButtonState.Pressed, aloitaPeli, null);
      //  Mouse.ListenOn(kohta2, MouseButton.Left, ButtonState.Pressed, ParhaatPisteet, null);
         Mouse.ListenOn(kohta3, MouseButton.Left, ButtonState.Pressed, Exit, null);

        ///Hiiren värjäys
        Mouse.ListenOn(kohta1, HoverState.Enter, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta1, true);
        Mouse.ListenOn(kohta1, HoverState.Exit, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta1, false);
    }
    void ValikossaLiikkuminen(Label kohta, bool paalla)
    {
        if (paalla)
        {
            kohta.TextColor = Color.Red;
        }
        else
        {
            kohta.TextColor = Color.Black;
        }
    }
    ///-----------------------------------------------------------------------------------------------------///

    ///!!!YLEISET ASETUKSET!!!
    private void aloitaPeli()
    {
        ClearAll();
        
        ///PERUSASETUKSET

        Gravity = new Vector(0, -1000);///Painovoima

        LuoKentta();///Luo kenttä kutsumalla text
        LisaaNappaimet();///Lisää ohjaimet

        Camera.Follow(pelaaja1);
        Camera.ZoomFactor = 1.2;
        Camera.StayInLevel = true;

        MasterVolume = 0.5;

        ///kutus aikalaskuri
        LuoPistelaskuri();
        LuoElamalaskuri();
        LuoAikalaskuri();

    }


    ///  LUO PISTELASKURI
    ///  Luodaan int meter psitelaskuri
    private DoubleMeter pistelaskuri;

    private void LuoPistelaskuri()
    {
        pistelaskuri = new DoubleMeter(0);

        Label pistenaytto = new Label();
        pistenaytto.X = Screen.Left + 970;
        pistenaytto.Y = Screen.Top - 70;
        pistenaytto.TextColor = Color.White;
        pistenaytto.Color = Color.Red;

        pistenaytto.BindTo(pistelaskuri);
        Add(pistenaytto);
       
        Remove(pistenaytto);
    }


    ///lUO AIKALASKURI
    private Timer aikalaskuri;
    private void LuoAikalaskuri()
    {
        aikalaskuri = new Timer();
        aikalaskuri.Start();

        Label aikanaytto = new Label();
        aikanaytto.TextColor = Color.White;
        aikanaytto.DecimalPlaces = 1;
        aikanaytto.BindTo(aikalaskuri.SecondCounter);
        aikanaytto.X = Screen.Left + 920;
        aikanaytto.Y = Screen.Top - 70;
        Add(aikanaytto);

    }


    ///LUODAAN  ELÄMÄLASKURI
    private DoubleMeter elamalaskuri;

    void LuoElamalaskuri()
    {
        elamalaskuri = new DoubleMeter(3);
        elamalaskuri.MaxValue = 5;
        elamalaskuri.LowerLimit += ElamaLoppui;

        ProgressBar elamapalkki = new ProgressBar(150, 20);
        elamapalkki.X = Screen.Left + 900;
        elamapalkki.Y = Screen.Top - 40;
        elamapalkki.BindTo(elamalaskuri);
        Add(elamapalkki);
    }


    private void ElamaLoppui()
    {
        ClearAll();
        int valinta = 1;
        tulosValikko(valinta);



    }
    ///-----------------------------------------------------------------------------------------------------///

    //LUODAAN KENTTÄ 
    private void LuoKentta()
    {
        TileMap kentta = TileMap.FromLevelAsset("kentta1.txt");
        kentta.SetTileMethod('#', LisaaTaso);
        kentta.SetTileMethod('/', LisaaputkiAlas);
        kentta.SetTileMethod('-', LisaaTasoRuoho);
        kentta.SetTileMethod('p', LisaaPelaaja);
        kentta.SetTileMethod('1', LisaaVihollinenTulihahmo);
        kentta.SetTileMethod('A', LisaaVihollinenPieniApina);
        kentta.SetTileMethod('t', LisaaTiiliTaso);
        kentta.SetTileMethod('k', LisaaKivi);
        kentta.SetTileMethod('d', LisaaKongApina);
        kentta.SetTileMethod('L', LisaaLoppuLippu);
        kentta.Execute(RUUDUN_KOKO, RUUDUN_KOKO);
        Level.CreateBorders();
        Level.Background.CreateGradient(Color.White, Color.Black);
    }


    /// OHJAIMET
    private void LisaaNappaimet()
    {
        Keyboard.Listen(Key.F1, ButtonState.Pressed, ShowControlHelp, "Näytä ohjeet");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");

        Keyboard.Listen(Key.Left, ButtonState.Down, Liikuta, "Liikkuu vasemmalle", pelaaja1, -NOPEUS);
        Keyboard.Listen(Key.Right, ButtonState.Down, Liikuta, "Liikkuu vasemmalle", pelaaja1, NOPEUS);
        Keyboard.Listen(Key.Up, ButtonState.Pressed, Hyppaa, "Pelaaja hyppää", pelaaja1, HYPPYNOPEUS);

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        //Lisätään pelihahmolle animaatiot liikkeen lisäksi
        Keyboard.Listen(Key.Up, ButtonState.Down, AnimaatioPelaajaHyppy, "Pelaaja hyppää", pelaaja1, NOPEUS);
        Keyboard.Listen(Key.Up, ButtonState.Released, AnimaatioPelaajaAlas, "Pelaaja hyppää", pelaaja1, NOPEUS);
    }


    ///-----------------------------------------------------------------------------------------------------///


    ///LISÄÄ RAKENNUSPALIKAT!!!
    ///Lisaa ruskea persutaso mutataso


    private void LisaaTaso(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject taso = PhysicsObject.CreateStaticObject(leveys, korkeus);
        taso.Position = paikka;
        taso.Color = Color.Black;
        Add(taso);
    }

    ///LISÄÄ PERUS RUOHOTASO
    private void LisaaTasoRuoho(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject taso = PhysicsObject.CreateStaticObject(leveys, korkeus);
        taso.Position = paikka;
        taso.Image = TasoRuohoKuva;
        Add(taso);
        AddCollisionHandler(taso, "tulipallo", TormaaTulipallo);
        AddCollisionHandler(taso, "Banaani", TormaaLamauttavaanBanaaniin);
        AddCollisionHandler(taso, "boulder", TormaaBoulderiin);


    }
    ///lISÄÄ PERUS tiilitaso
    private void LisaaLoppuLippu(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject lippu = PhysicsObject.CreateStaticObject(leveys, korkeus);
        lippu.IgnoresCollisionResponse = true;
        lippu.Position = paikka;
        lippu.Image = loppuLippu;
        lippu.Tag = "lippu";
        Add(lippu);
    }
    private void TormaaLoppuLippuun(PhysicsObject hahmo, PhysicsObject lippu)
    {
        ClearAll();
        int valinta = 0;
        tulosValikko(valinta);
        


    }
    private void tulosValikko(int valinta)
    {
        string otsikkoSisältö;
        if (valinta == 0)
            {
            otsikkoSisältö = "Voitit";
            }
        else
        {
            otsikkoSisältö = "Hävisit!";
        }
        Label otsikko = new Label(otsikkoSisältö); // Luodaan otsikko
        otsikko.Y = 100; // Otsikko on hieman valikonkohtien yläpuolella
        otsikko.Font = new Font(40, true); // Otsikon teksti on suurempi ja boldattu
        Add(otsikko);

        valikonKohdat = new List<Label>(); // Alustetaan lista, johon valikon kohdat tulevat

        Label kohta1 = new Label("Yritä Uudelleen");  // Luodaan uusi Label-olio, joka toimii uuden pelin aloituskohtana
        kohta1.Position = new Vector(0, 40);  // Asetetaan valikon ensimmäinen kohta hieman kentän keskikohdan yläpuolelle
        valikonKohdat.Add(kohta1);  // Lisätään luotu valikon kohta listaan jossa kohtia säilytetään


        Label kohta3 = new Label("Lopeta peli");
        kohta3.Position = new Vector(0, 0);
        valikonKohdat.Add(kohta3);

        string pisteMäärä = pistelaskuri.Value.ToString();
        int muutos = Convert.ToInt32(aikalaskuri.CurrentTime);
        string aikaMäärä = muutos.ToString();
            

        Label pisteet = new Label("pistemääräsi: "+ pisteMäärä);
        pisteet.Position = new Vector(0, -30);
        valikonKohdat.Add(pisteet);

        Label aika = new Label("aikasi oli: " + aikaMäärä + "sekunttia");
        aika.Position = new Vector(0, -60);
        valikonKohdat.Add(aika);




        // Lisätään kaikki luodut kohdat peliin foreach-silmukalla
        foreach (Label valikonKohta in valikonKohdat)
        {

            Add(valikonKohta);
        }

        Mouse.ListenOn(kohta1, MouseButton.Left, ButtonState.Pressed, aloitaPeli, null);
        //  Mouse.ListenOn(kohta2, MouseButton.Left, ButtonState.Pressed, ParhaatPisteet, null);
        Mouse.ListenOn(kohta3, MouseButton.Left, ButtonState.Pressed, Exit, null);

        ///Hiiren värjäys
        Mouse.ListenOn(kohta1, HoverState.Enter, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta1, true);
        Mouse.ListenOn(kohta1, HoverState.Exit, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta1, false);
    }

    /// Lisää putki alas
    private void LisaaputkiAlas(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject taso = PhysicsObject.CreateStaticObject(leveys, korkeus);
        taso.Position = paikka;
        taso.Image = PutkiAlasKuva;
        Add(taso);
        AddCollisionHandler(taso, "tulipallo", TormaaTulipallo);
        AddCollisionHandler(taso, "Banaani", TormaaLamauttavaanBanaaniin);

    }
    /// Lisää putki top


    ///LISÄÄ TIILITAS

    private void LisaaTiiliTaso(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject LisaaTiiliTaso = PhysicsObject.CreateStaticObject(leveys, korkeus);
        LisaaTiiliTaso.Position = paikka;
        LisaaTiiliTaso.Image = TiiliTasoKuva;
        Add(LisaaTiiliTaso);
    }

    ///Kun kertakäyttöesineet osuvat tasoiin niin käyttävät alla olevaa koodia poistamiseen
    private void LisaaKivi(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject kivi = PhysicsObject.CreateStaticObject(leveys, korkeus);
        kivi.Position = paikka;
        kivi.Image = KiviKuva;
        Add(kivi);
    }
    ///-------------------------------------------------
    /// ----------------------------------------------------///


    ///POWERUPIT 
    ///kOLIKOT JOISTA VÄHENNETÄÄN AIKAA SUORITUSISTA
    private void LisaaTahti(PhysicsObject vihollinen)
    {
        PhysicsObject tahti = new PhysicsObject(25, 25);
        tahti.Shape = Shape.Circle;
        tahti.IgnoresCollisionResponse = false;
        tahti.Position = vihollinen.Position;
        tahti.Image = tahtiKuva;
        tahti.Tag = "tahti";
        tahti.IgnoresGravity = false;

        Add(tahti);

    }
    private void LisaaSydan(PhysicsObject vihollinen)
    {
        PhysicsObject sydan = new PhysicsObject(25, 25);
        sydan.Shape = Shape.Heart;
        sydan.IgnoresCollisionResponse = false;
        sydan.Position = vihollinen.Position;
        sydan.Image = SydanKuva;
        sydan.Tag = "sydan";
        sydan.IgnoresGravity = false;

        Add(sydan);

    }

    private void LisaaKongApina(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject kongApina = PhysicsObject.CreateStaticObject(70, 120);
        kongApina.IgnoresCollisionResponse = true;
        kongApina.Position = paikka;
        kongApina.Animation = new Animation(KongApina);
        kongApina.Animation.Start();
        kongApina.Animation.FPS = 3;
        kongApina.Tag = "kongApina";
        Add(kongApina);
        //lisää ajastin joka toinen sekuntti kuva muuttuu

        Timer ajastin = new Timer();
        ajastin.Interval = 1.8; // Kuinka usein ajastin "laukeaa" sekunneissa
        ajastin.Timeout += delegate { LisaaBoulder(kongApina); }; // Aliohjelma, jota kutsutaan 2 sekunnin välein
        ajastin.Start(); // Ajastin pitää aina muistaa käynnistää
    }

    private void LisaaBoulder(PhysicsObject kongApina)
    {
        if (kongApina.IsDestroyed== true)
        {

        }
        else
        {
            PhysicsObject boulder = new PhysicsObject(30, 30);
            boulder.Shape = Shape.Circle;
            boulder.Color = Color.Red;
            boulder.Position = kongApina.Position;
            boulder.Image = Boulder;
            boulder.Tag = "boulder";
            boulder.IgnoresGravity = false;

            Add(boulder);

            Vector aloitus = new Vector(-200, -100);
            boulder.Hit(aloitus * 1);
        }
    }
    private void TormaaKongApinaan(PhysicsObject hahmo, PhysicsObject kong)
    {
        
        kong.Destroy();
        LisaaTahti(kong);

    }

    private void TormaaBoulderiin(PhysicsObject ruoho, PhysicsObject boulder)
    {
        boulder.Destroy();
    }




    private void LisaaVihollinenTulihahmo(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject vihollinenTulihahmo = PhysicsObject.CreateStaticObject(leveys, korkeus);
        vihollinenTulihahmo.IgnoresCollisionResponse = true;
        vihollinenTulihahmo.Position = paikka;
        vihollinenTulihahmo.Image = vihollisenKuvaTulihahmo;
        vihollinenTulihahmo.Tag = "vihollinenTulihahmo";
        Add(vihollinenTulihahmo);



        Timer ajastin = new Timer();
        ajastin.Interval = 2.2; // Kuinka usein ajastin "laukeaa" sekunneissa
        ajastin.Timeout += delegate { LisaaTulipallo(vihollinenTulihahmo); }; // Aliohjelma, jota kutsutaan 2 sekunnin välein
        ajastin.Start(); // Ajastin pitää aina muistaa käynnistää
    }



    private void TormaaViholliseenTulihahmo(PhysicsObject hahmo, PhysicsObject vihollinen)
    {

        vihollinen.Destroy();

        LisaaTahti(vihollinen);
        LisaaSydan(vihollinen);

    }



    private void LisaaTulipallo(PhysicsObject vihollinenTulihahmo)
    {
        if (vihollinenTulihahmo.IsDestroyed == true)
        {

        }
        else
        {
            PhysicsObject tulipallo = new PhysicsObject(30, 30);
            tulipallo.Shape = Shape.Circle;
            tulipallo.Color = Color.Red;
            tulipallo.Position = vihollinenTulihahmo.Position;
            tulipallo.Image = tulipalloKuva;
            tulipallo.Tag = "tulipallo";
            tulipallo.IgnoresGravity = true;

            Add(tulipallo);

            Vector aloitus = new Vector(200, 0.0);
            tulipallo.Hit(aloitus * 1);


        }
    }

    ///TULIPALLON COLLARIT
    private void TormaaTulipallo(PhysicsObject hahmo, PhysicsObject tulipallo)
    {
        if (hahmo == pelaaja1)
        {
            double x = 1;
            elamanVähennys(x);
        }

        tulipallo.Destroy();

    }



    private void LisaaVihollinenPieniApina(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject vihollinenPieniApina = PhysicsObject.CreateStaticObject(leveys, korkeus);
        vihollinenPieniApina.IgnoresCollisionResponse = true;
        vihollinenPieniApina.Position = paikka;
        vihollinenPieniApina.Image = vihollisenPieniApina;
        vihollinenPieniApina.Tag = "vihollinenPieniApina";
        Add(vihollinenPieniApina);



        Timer ajastin = new Timer();
        ajastin.Interval = 1; // Kuinka usein ajastin "laukeaa" sekunneissa
        ajastin.Timeout += delegate { LisaaBanaani(vihollinenPieniApina); }; // Aliohjelma, jota kutsutaan 2 sekunnin välein
        ajastin.Start(); // Ajastin pitää aina muistaa käynnistää
    }
    private void TormaaVihollinenPieniApina(PhysicsObject hahmo, PhysicsObject vihollinen)
    {

        vihollinen.Destroy();
        LisaaTahti(vihollinen);
        LisaaSydan(vihollinen);
    }
    private void LisaaBanaani(PhysicsObject vihollinenPieniApina)
    {
        if (vihollinenPieniApina.IsDestroyed == true)
        {

        }
        else
        {
            int rand = RandomGen.NextInt(0, 2);
            if (rand == 0)
            {
                PhysicsObject Banaani = new PhysicsObject(25, 25);
                Banaani.Shape = Shape.Circle;
                Banaani.Color = Color.Red;
                Banaani.Position = vihollinenPieniApina.Position;
                Banaani.Image = lamauttavaBanaani;
                Banaani.Tag = "Banaani";
                Banaani.IgnoresGravity = false;

                Add(Banaani);
                double suunta = RandomGen.NextInt(0, 2);
                if (suunta == 1)
                {
                    Vector aloitus = new Vector(200, -10);
                    Banaani.Hit(aloitus * 1);
                }
                else
                {
                    Vector aloitus = new Vector(-200, -30);
                    Banaani.Hit(aloitus * 1);
                }

            }


        }

    }
    private void TormaaLamauttavaanBanaaniin(PhysicsObject hahmo, PhysicsObject Banaani)
    {
        if (hahmo == pelaaja1)
        {
            double x = 0.1;
            double y = 2;
            jaadytys(y);
            elamanVähennys(x);
        }

        Banaani.Destroy();

    }

    ///-----------------------------------------------------------------------------------------------------///


    ///-----------------------------------------------------------------------------------------------------///


    ///-----------------------------------------------------------------------------------------------------///




    private void LisaaPelaaja(Vector paikka, double leveys, double korkeus)
    {
        pelaaja1 = new PlatformCharacter(30, 35);
        pelaaja1.Position = paikka;
        pelaaja1.Mass = 4.0;
        pelaaja1.Image = pelaajanKuva;
        AddCollisionHandler(pelaaja1, "tahti", TormaPisteeseen);
        AddCollisionHandler(pelaaja1, "sydan", TormaaSydameen);
        AddCollisionHandler(pelaaja1, "vihollinenTulihahmo", TormaaVihollinenPieniApina);
        AddCollisionHandler(pelaaja1, "tulipallo", TormaaTulipallo);
        AddCollisionHandler(pelaaja1, "vihollinenPieniApina", TormaaVihollinenPieniApina);
        AddCollisionHandler(pelaaja1, "Banaani", TormaaLamauttavaanBanaaniin);
        AddCollisionHandler(pelaaja1, "boulder", TormaaBoulder);
        AddCollisionHandler(pelaaja1, "kongApina", TormaaKongApinaan);
        AddCollisionHandler(pelaaja1, "lippu", TormaaLoppuLippuun);


        Add(pelaaja1);
    }

    private void TormaaBoulder(IPhysicsObject hahmo, IPhysicsObject esine)
    {
        elamalaskuri.Value -= 2;
        esine.Destroy();
    }

    private void TormaPisteeseen(IPhysicsObject hahmo, IPhysicsObject esine)
    {

        pistelaskuri.Value += 100;

        esine.Destroy();
    }
    private void TormaaSydameen(IPhysicsObject hahmo, IPhysicsObject esine)
    {

        int rand = RandomGen.NextInt(0, 2);
        elamanLisays(rand);

        esine.Destroy();
    }



    /// Pelaajan hyppy animaatio
    private void Hyppaa(PlatformCharacter hahmo, double nopeus)
    {
        hahmo.Jump(nopeus);
    }
    //pelaaja  kävely
    private void Liikuta(PlatformCharacter hahmo, double nopeus)
    {
        hahmo.Walk(nopeus);
    }


    ///pelaajan animaatiot
    ///Hyppäessä animaatio muuttuu hyppytliikkeeseen
    ///
    private void AnimaatioPelaajaHyppy(PlatformCharacter hahmo, double nopeus)
    {
        pelaaja1.Image = pelaajanKuvaHyppy;
    }

    private void AnimaatioPelaajaAlas(PlatformCharacter hahmo, double nopeus)
    {
        pelaaja1.Image = pelaajanKuva;
    }
    ///-----------------------------------------------------------------------------------------------------///


    ////HUOM LISÄÄ VIHOLLIENN
    ///
    /// 


    ///Saavutukset, elämän ja ajan vähennys sekä lisäys
    ///

    ///ELÄMÄN VÄHENNYS
    ///

    private void elamanVähennys(double määrä)
    {

        elamalaskuri.Value -= määrä;
    }

    private void elamanLisays(double määrä)
    {
        elamalaskuri.Value += määrä;
    }

    private void jaadytys(double määrä)
    {
        
    }

    private void LisääBonusPiste(PhysicsObject paikka)
    {
        PhysicsObject bonusPiste = new PhysicsObject(25, 25);
        bonusPiste.Shape = Shape.Circle;
        bonusPiste.Color = Color.Red;
        bonusPiste.Position = paikka.Position;
        bonusPiste.Image = lamauttavaBanaani;
        bonusPiste.Tag = "bonusPiste";
        bonusPiste.IgnoresGravity = false;

        Add(bonusPiste);

    }

}
