using Jypeli;
using Jypeli.Widgets;
using System;
using System.Collections.Generic;

/// <summary>
/// @author kmontasi
/// @version 13.12.2021
/// <summary>
///
public class Alpha : PhysicsGame
{
    /// <summary>
    /// Lista aloitusvalikon kohdille
    /// </summary>
    List<Label> valikonKohdat;


    /// <summary>
    /// luodaan globaali pistelaskuri 
    /// </summary>
    private DoubleMeter pistelaskuri;


    /// <summary>
    /// luodaan elämälaskuri ja sen ominaisuudet
    /// </summary>
    /// 
    private DoubleMeter elamalaskuri;


    /// <summary>
    /// pelin asetukset sekä pelaajan animaation
    /// </summary>
    private const int RUUDUN_KOKO = 40;
    private PlatformCharacter pelaaja1;
    private const double NOPEUS = 100;
    private const double HYPPYNOPEUS = 600;
    private Image pelaajanKuva = LoadImage("PlumberB.png"); 
    private Image pelaajanKuvaHyppy = LoadImage("PlumberHyppy.png");


    /// <summary>
    /// Kuvat kaikista objekteista
    /// </summary>
    private Image TasoRuohoKuva = LoadImage("TasoRuoho.png");  
    private Image PutkiAlasKuva = LoadImage("Putkialas.png");  
    private Image TiiliTasoKuva = LoadImage("tiilitaso.png");  
    private Image KiviKuva = LoadImage("kivi.png");            
    private Image tahtiKuva = LoadImage("tahti.png");
    private Image SydanKuva = LoadImage("sydan.png");
    private Image loppuLippu = LoadImage("lippu.png");


    /// <summary>
    /// vihollisten animaatiot ja muut kuvat
    /// </summary>
    private Image vihollisenPieniApina = LoadImage("pieniapina.png");
    private Image lamauttavaBanaani = LoadImage("lamauttavabanaani.png");
    private Image vihollisenKuvaTulihahmo = LoadImage("vihollinen.png");
    private Image tulipalloKuva = LoadImage("tulipallo.png");
    private Image[] KongApina = LoadImages("kongapina", "kongapinaleft");
    private Image Boulder = LoadImage("boulder.png");
 

    /// <summary>
    /// Pääohjelma alkaa sekä se kutsuu valikon
    /// </summary>
    public override void Begin()
    {
        Valikko();
    }


    /// <summary>
    /// Valikko jossa kaikki asetukset
    /// kun klikataan aloita, kutsutaan aliohjelma
    /// kun valitaan lopeta peli, sammuta ohjelma
    /// </summary>

   
    private void Valikko()
    { 
        Label otsikko = new Label("Pupuhuhdan Plumberman"); 
        otsikko.Y = 100; 
        otsikko.Font = new Font(40, true);
        Add(otsikko);

        valikonKohdat = new List<Label>(); 

        Label kohta1 = new Label("Aloita uusi peli");  
        kohta1.Position = new Vector(0, 40); 
        valikonKohdat.Add(kohta1);  


        Label kohta3 = new Label("Lopeta peli");
        kohta3.Position = new Vector(0, 0);
        valikonKohdat.Add(kohta3);
        // Lisätään kaikki luodut kohdat peliin foreach-silmukalla
        foreach (Label valikonKohta in valikonKohdat)
        {
            Add(valikonKohta);
        }

        Mouse.ListenOn(kohta1, MouseButton.Left, ButtonState.Pressed, aloitaPeli, null);
         Mouse.ListenOn(kohta3, MouseButton.Left, ButtonState.Pressed, Exit, null);
        Mouse.ListenOn(kohta1, HoverState.Enter, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta1, true);
        Mouse.ListenOn(kohta1, HoverState.Exit, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta1, false);
    }


    /// <summary>
    /// värjää valikon otsikko kun hiiri on sen päällä
    /// </summary>
    private void ValikossaLiikkuminen(Label kohta, bool paalla)
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
   
    /// <summary>
    /// muut aliohjelmat kun vlaikosta on valittu aloita peli, aloitetaan clear allilla joka poistaa vanhat valikon grafiikat
    /// tämän jälkeen kutsutaan txt kenttä ja lisätään kaikki graafiset ominaisuudet
    /// aliohjelna myös kutsuu aika-,elämä- sekä elämälaskurin.
    /// </summary>
    private void aloitaPeli()
    {
        ClearAll();
        

        Gravity = new Vector(0, -1000);

        LuoKentta();///Luo kenttä kutsumalla text
        LisaaNappaimet();///Lisää ohjaimet

        Camera.Follow(pelaaja1);
        Camera.ZoomFactor = 1.2;
        Camera.StayInLevel = true;
        MasterVolume = 0.0;
        LuoPistelaskuri();
        LuoElamalaskuri();
        LuoAikalaskuri();

    }


    /// <summary>
    /// luodaan pistelaskuri
    /// sekä sen asetukset ja sijainti
    /// </summary>

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


    /// <summary>
    /// luodaan aikalaskuri sekä sen ominaisuudet sekä sijainti
    /// </summary>
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



    /// <summary>
    /// luodaan elämälaskuri sekä sen asetukset ja sijainti
    /// </summary>
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

    /// <summary>
    /// kutsutaan jäviö valikko kun elämät loppuvat ja samalla clear all niin poistetaan vanha pelikenttä
    /// </summary>

    private void ElamaLoppui()
    {
        ClearAll();
        int valinta = 1;
        tulosValikko(valinta);
    }
    

    /// <summary>
    /// luodaan kenttä sekä annetaan olioille tietyt kutsut
    /// </summary>

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


    /// <summary>
    /// luodaan ohjaimet sekä pelaajaa animaatioliikkeet
    /// </summary>
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

/// <summary>
/// aliohjelma mustalle tasolle
/// </summary>
/// <param name="paikka"></param>
/// <param name="leveys"></param>
/// <param name="korkeus"></param>
    private void LisaaTaso(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject taso = PhysicsObject.CreateStaticObject(leveys, korkeus);
        taso.Position = paikka;
        taso.Color = Color.Black;
        Add(taso);
    }


    /// <summary>
    /// aliohjelma ruohotasolle
    /// </summary>
    /// <param name="paikka"></param>
    /// <param name="leveys"></param>
    /// <param name="korkeus"></param>
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
  /// <summary>
  /// aliohjelma loppulipulle
  /// </summary>
  /// <param name="paikka"></param>
  /// <param name="leveys"></param>
  /// <param name="korkeus"></param>
    private void LisaaLoppuLippu(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject lippu = PhysicsObject.CreateStaticObject(leveys, korkeus);
        lippu.IgnoresCollisionResponse = true;
        lippu.Position = paikka;
        lippu.Image = loppuLippu;
        lippu.Tag = "lippu";
        Add(lippu);
    }


    /// <summary>
    /// peli loppuu sekä  clear all kun pelaaja osuu loppu lippuun eli maaliin
    /// kutsuu tulosvalikon sekä antaa valikon asetukset
    /// </summary>
    /// <param name="hahmo"></param>
    /// <param name="lippu"></param>


    private void TormaaLoppuLippuun(PhysicsObject hahmo, PhysicsObject lippu)
    {
        ClearAll();
        int valinta = 0;
        tulosValikko(valinta);
    }

    /// <summary>
    ///aliohjelma tulosvalikolle 
    /// päättää otsikon toisen ohjelman lähetetyn arvon avulla eli onko häviö valikko vai onko voittovalikko
    /// </summary>
    /// <param name="valinta"></param>

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

/// <summary>
/// aliohjelma putkelle
/// </summary>
/// <param name="paikka"></param>
/// <param name="leveys"></param>
/// <param name="korkeus"></param>
    private void LisaaputkiAlas(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject taso = PhysicsObject.CreateStaticObject(leveys, korkeus);
        taso.Position = paikka;
        taso.Image = PutkiAlasKuva;
        Add(taso);
        AddCollisionHandler(taso, "tulipallo", TormaaTulipallo);
        AddCollisionHandler(taso, "Banaani", TormaaLamauttavaanBanaaniin);

    }
/// <summary>
/// áliohjelma tiilitasolle
/// </summary>
/// <param name="paikka"></param>
/// <param name="leveys"></param>
/// <param name="korkeus"></param>

    private void LisaaTiiliTaso(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject LisaaTiiliTaso = PhysicsObject.CreateStaticObject(leveys, korkeus);
        LisaaTiiliTaso.Position = paikka;
        LisaaTiiliTaso.Image = TiiliTasoKuva;
        Add(LisaaTiiliTaso);
    }

/// <summary>
/// aliohjelma kivitasolle
/// </summary>
/// <param name="paikka"></param>
/// <param name="leveys"></param>
/// <param name="korkeus"></param>
    private void LisaaKivi(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject kivi = PhysicsObject.CreateStaticObject(leveys, korkeus);
        kivi.Position = paikka;
        kivi.Image = KiviKuva;
        Add(kivi);
    }



    /// <summary>
    /// aliohjelma tähdellle
    /// </summary>
    /// <param name="vihollinen"></param>
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

/// <summary>
/// aliohjelma sydämelle
/// </summary>
/// <param name="vihollinen"></param>
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

    /// <summary>
    ///  aliohjelma pääviholliselle
    /// kong luo ajastimen sekä joka 1.8 sekuntti kutsuu kiviohjelman joka vierii eteenpäin
    /// </summary>
    /// <param name="paikka"></param>
    /// <param name="leveys"></param>
    /// <param name="korkeus"></param>
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

    /// <summary>
    /// aliohjelma boulderille
    /// </summary>
    /// <param name="kongApina"></param>
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

    /// <summary>
    /// kongapinan collitionit
    /// </summary>
    /// <param name="hahmo"></param>
    /// <param name="kong"></param>
    private void TormaaKongApinaan(PhysicsObject hahmo, PhysicsObject kong)
    {
        
        kong.Destroy();
        LisaaTahti(kong);

    }
    /// <summary>
    /// boulderit collitionit
    /// </summary>
    /// <param name="ruoho"></param>
    /// <param name="boulder"></param>
    private void TormaaBoulderiin(PhysicsObject ruoho, PhysicsObject boulder)
    {
        boulder.Destroy();
    }



    /// <summary>
    /// aliohjelma tulihahmolle
    /// </summary>
    /// <param name="paikka"></param>
    /// <param name="leveys"></param>
    /// <param name="korkeus"></param>
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


    /// <summary>
    /// aliohelma tuliohelman collitionille
    /// </summary>
    /// <param name="hahmo"></param>
    /// <param name="vihollinen"></param>
    private void TormaaViholliseenTulihahmo(PhysicsObject hahmo, PhysicsObject vihollinen)
    {

        vihollinen.Destroy();

        LisaaTahti(vihollinen);
        LisaaSydan(vihollinen);

    }


   /// <summary>
   /// lisää tulipallo sekä sen ominaisuudet
   /// </summary>
   /// <param name="vihollinenTulihahmo"></param>
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



    /// <summary>
    /// tulipallon collitionit
    /// </summary>
    /// <param name="hahmo"></param>
    /// <param name="tulipallo"></param>
    private void TormaaTulipallo(PhysicsObject hahmo, PhysicsObject tulipallo)
    {
        if (hahmo == pelaaja1)
        {
            double x = 1;
            elamanVähennys(x);
        }

        tulipallo.Destroy();

    }

    /// <summary>
    /// pieniapina joka heittää banaaneja ajastimen mukaan, siinä on myös rand joka päättää että heitetäänkö banaani ja mihin suuntaan se heitetääm
    /// </summary>
    /// <param name="paikka"></param>
    /// <param name="leveys"></param>
    /// <param name="korkeus"></param>
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

    /// <summary>
    /// pienen apinana collitionit
    /// </summary>
    /// <param name="hahmo"></param>
    /// <param name="vihollinen"></param>
    private void TormaaVihollinenPieniApina(PhysicsObject hahmo, PhysicsObject vihollinen)
    {
            
        vihollinen.Destroy();
        LisaaTahti(vihollinen);
        LisaaSydan(vihollinen);
    }

   /// <summary>
   /// pienen apinan banaani
   /// </summary>
   /// <param name="vihollinenPieniApina"></param>

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

    /// <summary>
    /// mitä käy kun banaaniin osutaan
    /// pelaajan elämä vähennetään ja banaani tulotaan (huom!! banaanilla on monta collaria)
    /// </summary>
    /// </summary>
    /// <param name="hahmo"></param>
    /// <param name="Banaani"></param>
    private void TormaaLamauttavaanBanaaniin(PhysicsObject hahmo, PhysicsObject Banaani)
    {
        if (hahmo == pelaaja1)
        {
            double x = 0.1;
            double y = 2;

            elamanVähennys(x);
        }

        Banaani.Destroy();

    }

 /// <summary>
 /// lisää pelaaja sekä sen ominaisuudet
 /// </summary>
 /// <param name="paikka"></param>
 /// <param name="leveys"></param>
 /// <param name="korkeus"></param>
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

/// <summary>
/// boulderin collitionit
/// </summary>
/// <param name="hahmo"></param>
/// <param name="esine"></param>
    private void TormaaBoulder(IPhysicsObject hahmo, IPhysicsObject esine)
    {
        elamalaskuri.Value -= 2;
        esine.Destroy();
    }


    /// <summary>
    /// mitä käy kun pisteeseen lisäävään olioon osutaan. (tätä aliohjelmaa voiaan käyttää yleisesti kaikkialla pisteitä lisättävilla olioilla)
    /// </summary>
    /// <param name="hahmo"></param>
    /// <param name="esine"></param>
    private void TormaPisteeseen(IPhysicsObject hahmo, IPhysicsObject esine)
    {

        pistelaskuri.Value += 100;

        esine.Destroy();
    }

    /// <summary>
    /// mitä käy kun osutaan  elämiä lisäävään olentoon kuten sydömeen
    /// </summary>
    /// <param name="hahmo"></param>
    /// <param name="esine"></param>
    private void TormaaSydameen(IPhysicsObject hahmo, IPhysicsObject esine)
    {

        int rand = RandomGen.NextInt(0, 2);
        elamanLisays(rand);

        esine.Destroy();
    }


    /// <summary>
    /// pelaajan hypyn asetuksen kuten nopeus ja korkeus
    /// </summary>
    /// <param name="hahmo"></param>
    /// <param name="nopeus"></param>
    private void Hyppaa(PlatformCharacter hahmo, double nopeus)
    {
        hahmo.Jump(nopeus);
    }
    /// <summary>
    ///  liikkumisen nopeus
    /// </summary>
    /// <param name="hahmo"></param>
    /// <param name="nopeus"></param>
    private void Liikuta(PlatformCharacter hahmo, double nopeus)
    {
        hahmo.Walk(nopeus);
    }


    /// <summary>
    /// pelaahan animaatio kun pelaaja painaa space baria eli pelaajan hyppy animaatio 
    /// </summary>
    /// <param name="hahmo"></param>
    /// <param name="nopeus"></param>

    private void AnimaatioPelaajaHyppy(PlatformCharacter hahmo, double nopeus)
    {
        pelaaja1.Image = pelaajanKuvaHyppy;
    }


    /// <summary>
    /// kuva joka resetoii ÁnimaatioPelaajaHypyn eli pelaajan peruskuva
    /// </summary>
    /// <param name="hahmo"></param>
    /// <param name="nopeus"></param>
    private void AnimaatioPelaajaAlas(PlatformCharacter hahmo, double nopeus)
    {
        pelaaja1.Image = pelaajanKuva;
    }


    /// <summary>
    /// laskuri joka vähentää elamaskuri olisota annetus ohjelman määrän elämiä
    /// </summary>
    /// <param name="määrä"></param>
    private void elamanVähennys(double määrä)
    {

        elamalaskuri.Value -= määrä;
    }

    /// <summary>
    /// aliohjelma joka lisää elamalaskuriin annetun määrän elämiä
    /// </summary>
    /// <param name="määrä"></param>
    private void elamanLisays(double määrä)
    {
        elamalaskuri.Value += määrä;
    }



    /// <summary>
    /// bonuspisteiden asetukset sekä niden ominaisuudet
    /// </summary>
    /// <param name="paikka"></param>
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
