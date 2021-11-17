using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;
using System;
//using System.Timers;
using System.Globalization;
using System.Collections.Generic;
using Windows;

//using System.Windows.Forms;


public class Alpha : PhysicsGame
{   //YLEISET ASETUKSET

    //LASKURIT
    //Aikalaskuri


    //ELÄMÄNLASKURI




    //OHJAIMET


    // NÄYTYÖN KOKO
    private const int RUUDUN_KOKO = 40;




    ///-----------------------------------------------------------------------------------------------------///
    ///PELAAJAN KAIKKI ASETUKSET!!!
    //PELAAJAMALLIN ASETUKSET
    //PELAAJAN NIMI !!TÄMÄ PYSYY AINA SAMANA ELLEI LISÄTÄ UUTTA PELAAJAA
    //TO-DO- TOISEN PELAAJAN LISÄÄMINEN
    private PlatformCharacter pelaaja1;
    //PELAAJAN ASETUKSET
    private const double NOPEUS = 100;
    private const double HYPPYNOPEUS = 600;
    ///Pelaajan Animaatiot ja Grafiikat
    private Image pelaajanKuva = LoadImage("Plumber.png"); ///Pelaajan STATIC animaatio
    private Image pelaajanKuvaHyppy = LoadImage("PlumberHyppy.png");///Pelaaajan hyppy animaatio


    ///-----------------------------------------------------------------------------------------------------///


    //TASON GRAFIIKAT

    //RUOHO
    private Image TasoRuohoKuva = LoadImage("TasoRuoho.png");  //RUOHO

    //Putki ALAS
    private Image PutkiAlasKuva = LoadImage("Putkialas.png");
    //Putki Start ylös

    ///-----------------------------------------------------------------------------------------------------///


    //INTERAKTIIVISET ESINEET SEKÄ POWERUPIT

    //KOLIKOT !!Vaihda
    private Image kolikkoKuva = LoadImage("tahti.png");
    ///-----------------------------------------------------------------------------------------------------///

    //NEGATIIVISET OLIOT

    //satuttaa pelaajaa ja ottaa yhden dyfämen pois
    private Image pomminKuva = LoadImage("Pommi.png");//POMMI  kUVA'

    //POMMIN TO DO !!! RÄJÄHDUS ANIMAATIO

    //POMMISTA TULEVAT SAVUT

    //VIHOLLISET

    //Vihollinen pieniapina
    /// <summary>
    /// ampuu banaaneja alaspäin
    /// </summary>
    /// 
    private Image vihollisenPieniApina = LoadImage("pieniapina.png");
    /// VPienen apinan banaani
    private Image lamauttavaBanaani = LoadImage("lamauttavabanaani.png");
<<<<<<< HEAD



    //Vihollinen tulijhahmo
    private Image vihollisenKuvaTulihahmo = LoadImage("vihollinen.png");//VIHOLLINEN YKSI (TULIHAHMO)
                                                                        //VIHOLLINEN YKSI TULIPALLO !!!
    private Image tulipalloKuva = LoadImage("tulipallo.png");//VIHOLLINEN YKSI TULIPALLO ORDNANCE (TULIHAHMO)
=======



    //Vihollinen tulijhahmo
    private Image vihollisenKuvaTulihahmo = LoadImage("vihollinen.png");//VIHOLLINEN YKSI (TULIHAHMO)
            //VIHOLLINEN YKSI TULIPALLO !!!
            private Image tulipalloKuva = LoadImage("tulipallo.png");//VIHOLLINEN YKSI TULIPALLO ORDNANCE (TULIHAHMO)
>>>>>>> 36e816c89939bcf864b73ec1ba6796e49fd75683
    ///-----------------------------------------------------------------------------------------------------///



    //ALOITUS JA MAIN
    public override void Begin()
    {
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
<<<<<<< HEAD

=======
    

    ///-----------------------------------------------------------------------------------------------------///

    ///!!!YLEISET ASETUKSET!!!
>>>>>>> 36e816c89939bcf864b73ec1ba6796e49fd75683

    ///-----------------------------------------------------------------------------------------------------///

<<<<<<< HEAD
    ///!!!YLEISET ASETUKSET!!!
=======
    ///  LUO PISTELASKURI
    ///  Luodaan int meter psitelaskuri
    private IntMeter pistelaskuri;
>>>>>>> 36e816c89939bcf864b73ec1ba6796e49fd75683

    void LuoPistelaskuri()
    {
        pistelaskuri = new IntMeter(0);

<<<<<<< HEAD
    ///  LUO PISTELASKURI
    ///  Luodaan int meter psitelaskuri
    private IntMeter pistelaskuri;

    void LuoPistelaskuri()
    {
        pistelaskuri = new IntMeter(0);
=======
        Label pistenaytto = new Label();
        pistenaytto.X = Screen.Left + 970;
        pistenaytto.Y = Screen.Top - 70;
        pistenaytto.TextColor = Color.White;
        pistenaytto.Color = Color.Red;

        pistenaytto.BindTo(pistelaskuri);
        Add(pistenaytto);
    }
>>>>>>> 36e816c89939bcf864b73ec1ba6796e49fd75683

        Label pistenaytto = new Label();
        pistenaytto.X = Screen.Left + 970;
        pistenaytto.Y = Screen.Top - 70;
        pistenaytto.TextColor = Color.White;
        pistenaytto.Color = Color.Red;

<<<<<<< HEAD
        pistenaytto.BindTo(pistelaskuri);
        Add(pistenaytto);
    }


    ///lUO AIKALASKURI
    private void LuoAikalaskuri()
    {
        Timer aikalaskuri = new Timer();
        aikalaskuri.Start();

        Label aikanaytto = new Label();
        aikanaytto.TextColor = Color.White;
        aikanaytto.DecimalPlaces = 1;
        aikanaytto.BindTo(aikalaskuri.SecondCounter);
        aikanaytto.X = Screen.Left + 920;
        aikanaytto.Y = Screen.Top - 70;
        Add(aikanaytto);

=======
    ///lUO AIKALASKURI
    private void LuoAikalaskuri()
    {
        Timer aikalaskuri = new Timer();
        aikalaskuri.Start();

        Label aikanaytto = new Label();
        aikanaytto.TextColor = Color.White;
        aikanaytto.DecimalPlaces = 1;
        aikanaytto.BindTo(aikalaskuri.SecondCounter);
        aikanaytto.X = Screen.Left + 920;
        aikanaytto.Y = Screen.Top - 70;
        Add(aikanaytto);

>>>>>>> 36e816c89939bcf864b73ec1ba6796e49fd75683
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

    void ElamaLoppui()
    {
        MessageDisplay.Add("Elämät loppuivat, voi voi.");
    }

    ///-----------------------------------------------------------------------------------------------------///

    //LUODAAN KENTTÄ 
    private void LuoKentta()
    {
        TileMap kentta = TileMap.FromLevelAsset("kentta1.txt");
        kentta.SetTileMethod('#', LisaaTaso);
        kentta.SetTileMethod('/', LisaaputkiAlas);
        kentta.SetTileMethod('-', LisaaTasoRuoho);
        kentta.SetTileMethod('2', LisaaPommi);
        kentta.SetTileMethod('*', LisaaKolikko);
        kentta.SetTileMethod('p', LisaaPelaaja);
        kentta.SetTileMethod('1', LisaaVihollinenTulihahmo);
        kentta.SetTileMethod('A', LisaaVihollinenPieniApina);
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

    }
    ///lISÄÄ PERUS tiilitaso

    /// Lisää putki alas
    private void LisaaputkiAlas(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject taso = PhysicsObject.CreateStaticObject(leveys, korkeus);
        taso.Position = paikka;
        taso.Image = PutkiAlasKuva;
        Add(taso);
        AddCollisionHandler(taso, "tulipallo", TormaaTulipallo);

    }
    ///Kun kertakäyttöesineet osuvat tasoiin niin käyttävät alla olevaa koodia poistamiseen

    ///-----------------------------------------------------------------------------------------------------///

    ///POWERUPIT 
    ///kOLIKOT JOISTA VÄHENNETÄÄN AIKAA SUORITUSISTA
    private void LisaaKolikko(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject kolikko = PhysicsObject.CreateStaticObject(leveys, korkeus);
        kolikko.IgnoresCollisionResponse = true;
        kolikko.Position = paikka;
        kolikko.Image = kolikkoKuva;
        kolikko.Tag = "kolikko";
        Add(kolikko);

    }
    ///kOLIKKOJEN COLLARIT
    private void TormaaKolikkoon(PhysicsObject hahmo, PhysicsObject kolikko)
    {

        MessageDisplay.Add("Keräsit Kolikon!");
        kolikko.Destroy();
    }
    ///-----------------------------------------------------------------------------------------------------///
    private void LisaaPommi(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject pommi = PhysicsObject.CreateStaticObject(leveys, korkeus);
        pommi.IgnoresCollisionResponse = true;
        pommi.Position = paikka;
        pommi.Image = pomminKuva;
        pommi.Tag = "pommi";
        Add(pommi);
    }
    /// POMMIN COLLARI
    private void TormaaPommiin(PhysicsObject hahmo, PhysicsObject pommi)
    {

        MessageDisplay.Add("BREH SÄ KUOLIT");
        // TODO vähennö health!!!!!!!!!!!!!!!!!!
    }

    ///-----------------------------------------------------------------------------------------------------///

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

<<<<<<< HEAD

=======
 
>>>>>>> 36e816c89939bcf864b73ec1ba6796e49fd75683
        }
    }

    ///TULIPALLON COLLARIT
    private void TormaaTulipallo(PhysicsObject hahmo, PhysicsObject tulipallo)
    {
        if (hahmo == pelaaja1)
        {
            elamanVähennys();
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
                Banaani.IgnoresGravity = true;

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
    ///-----------------------------------------------------------------------------------------------------///


    ///-----------------------------------------------------------------------------------------------------///


    ///-----------------------------------------------------------------------------------------------------///




    private void LisaaPelaaja(Vector paikka, double leveys, double korkeus)
    {
        pelaaja1 = new PlatformCharacter(30, 35);
        pelaaja1.Position = paikka;
        pelaaja1.Mass = 4.0;
        pelaaja1.Image = pelaajanKuva;
        AddCollisionHandler(pelaaja1, "kolikko", TormaaKolikkoon);
        AddCollisionHandler(pelaaja1, "pommi", TormaaPommiin);
        AddCollisionHandler(pelaaja1, "vihollinenTulihahmo", TormaaViholliseenTulihahmo);
        AddCollisionHandler(pelaaja1, "tulipallo", TormaaTulipallo);
        Add(pelaaja1);
    }
    /// Pelaajan hyppy animaatio
<<<<<<< HEAD
    private void Hyppaa(PlatformCharacter hahmo, double nopeus)
    {
        hahmo.Jump(nopeus);
    }
    //pelaaja  kävely
    private void Liikuta(PlatformCharacter hahmo, double nopeus)
    {
        hahmo.Walk(nopeus);
    }
=======
        private void Hyppaa(PlatformCharacter hahmo, double nopeus)
        {
            hahmo.Jump(nopeus);
        }
    //pelaaja  kävely
        private void Liikuta(PlatformCharacter hahmo, double nopeus)
        {
            hahmo.Walk(nopeus);
        }
>>>>>>> 36e816c89939bcf864b73ec1ba6796e49fd75683


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
<<<<<<< HEAD

    ///ELÄMÄN VÄHENNYS
    ///

    private void elamanVähennys()
    {

        elamalaskuri.Value -= 1;
    }

    private void elamanLisays()
    {
        elamalaskuri.Value += 1;
=======

    ///ELÄMÄN VÄHENNYS
    ///

    private void elamanVähennys()
    {
        
        elamalaskuri.Value -= 1;
    }

    private void elamanLisays()
    {
        elamalaskuri.Value += 1;
    }


>>>>>>> 36e816c89939bcf864b73ec1ba6796e49fd75683
    }


}













//ANIMAATIOT


////     System.DateTime moment = new System.DateTime();
//int aika = moment.Second;
//int i;
//aika +=i
//   for (i = 1; i< 10; i++)
//  {
//     if (aika % 9 == 0)
//    {
//       LisaaTulipallo(paikka, leveys, korkeus);
//}

<<<<<<< HEAD
//    }
=======
    
 
  
>>>>>>> 36e816c89939bcf864b73ec1ba6796e49fd75683
