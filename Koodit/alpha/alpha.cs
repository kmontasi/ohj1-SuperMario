using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


public class alpha : PhysicsGame
{   //YLEISET ASETUKSET
    //OHJAIMET


    // NÄYTYÖN KOKO
    private const int RUUDUN_KOKO = 40;


    //PELAAJAN ASETUKSET
    private const double NOPEUS = 100;
    private const double HYPPYNOPEUS = 600;


    //PELAAJAMALLIN ASETUKSET
    //PELAAJAN NIMI !!TÄMÄ PYSYY AINA SAMANA ELLEI LISÄTÄ UUTTA PELAAJAA
    //TO-DO- TOISEN PELAAJAN LISÄÄMINEN
    private PlatformCharacter pelaaja1;
    

    //PELAAJAN KUVA
    //!!Lisätään seuraavassa vaiheeessa valikko josta valittua mallia voi muokata. eli eirlaisia hahmoja
    private Image pelaajanKuva = LoadImage("Plumber.png");


    //PELAAJA KUVA ANIMAATIOT
    //animaatiot hyppaa
    private Image pelaajanKuvahyppaa = LoadImage("tahti.png");


    //TASON GRAFIIKAT
    //Tiili
    private Image TasoTiiliKuva = LoadImage("TasoRuoho.png");

    //Putki ALAS
    private Image PutkiAlasKuva = LoadImage("PutkiALas.png");




    //INTERAKTIIVISET ESINEET



    //KOLIKOT !!Vaihda
    private Image kolikkoKuva = LoadImage("tahti.png");


    //NEGATIIVISET OLIOT
    //POMMI 
    //satuttaa pelaajaa ja ottaa yhden dyfämen pois
    private Image pomminKuva = LoadImage("Pommi.png");
    //POMMIN RÄJÄHDUS ANIMAATIO

    //POMMISTA TULEVAT SAVUT
    
    //VIHOLLISET
    //VIHOLLINEN YKSI (TULIHAHMO)
    private Image vihollisenKuva = LoadImage("vihollinen.png");

    //VIHOLLINEN YKSI TULIPALLO TO-DO!!!




    //ALOITUS JA MAIN
    public override void Begin()
    {
        Gravity = new Vector(0, -1000);

        LuoKentta();
        LisaaNappaimet();

        Camera.Follow(pelaaja1);
        Camera.ZoomFactor = 1.2;
        Camera.StayInLevel = true;

        MasterVolume = 0.5;
    }

    //LUODAAN KENTTÄ 

    private void LuoKentta()
    {
        TileMap kentta = TileMap.FromLevelAsset("kentta1.txt");
        kentta.SetTileMethod('#', LisaaTaso);
        kentta.SetTileMethod('/', LisaaputkiAlas);
        kentta.SetTileMethod('-', LisaaTasoTiili);
        kentta.SetTileMethod('2', LisaaPommi);
        kentta.SetTileMethod('*', LisaaKolikko);
        kentta.SetTileMethod('p', LisaaPelaaja);
        kentta.SetTileMethod('1', LisaaVihollinen);

        kentta.Execute(RUUDUN_KOKO, RUUDUN_KOKO);
        Level.CreateBorders();
        Level.Background.CreateGradient(Color.White, Color.Black);
    }

 
    private void LisaaTaso(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject taso = PhysicsObject.CreateStaticObject(leveys, korkeus);
        taso.Position = paikka;
        taso.Color = Color.Green;
        Add(taso);
    }
    private void LisaaTasoTiili(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject taso = PhysicsObject.CreateStaticObject(leveys, korkeus);
        taso.Position = paikka;
        taso.Image = TasoTiiliKuva;
        Add(taso);
    }

    private void LisaaputkiAlas(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject taso = PhysicsObject.CreateStaticObject(leveys, korkeus);
        taso.Position = paikka;
        taso.Image = PutkiAlasKuva;
        Add(taso);
    }


    private void LisaaKolikko(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject kolikko = PhysicsObject.CreateStaticObject(leveys, korkeus);
        kolikko.IgnoresCollisionResponse = true;
        kolikko.Position = paikka;
        kolikko.Image = kolikkoKuva;
        kolikko.Tag = "kolikko";
        Add(kolikko);
    }
    private void LisaaVihollinen(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject vihollinen = PhysicsObject.CreateStaticObject(leveys, korkeus);
        vihollinen.IgnoresCollisionResponse = true;
        vihollinen.Position = paikka;
        vihollinen.Image = vihollisenKuva;
        vihollinen.Tag = "vihollinen";
        Add(vihollinen);
    }
    private void LisaaPommi(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject pommi = PhysicsObject.CreateStaticObject(leveys, korkeus);
        pommi.IgnoresCollisionResponse = true;
        pommi.Position = paikka;
        pommi.Image = pomminKuva;
        pommi.Tag = "pommi";
        Add(pommi);
    }
    private void LisaaPelaaja(Vector paikka, double leveys, double korkeus)
    {
        pelaaja1 = new PlatformCharacter(leveys, korkeus);
        pelaaja1.Position = paikka;
        pelaaja1.Mass = 4.0;
        pelaaja1.Image = pelaajanKuva;
        AddCollisionHandler(pelaaja1, "kolikko", TormaaKolikkoon);
        AddCollisionHandler(pelaaja1, "pommi", TormaaPommiin);
        Add(pelaaja1);
    }


    ////HUOM LISÄÄ VIHOLLIENN
    ///

    private void LisaaNappaimet()
    {
        Keyboard.Listen(Key.F1, ButtonState.Pressed, ShowControlHelp, "Näytä ohjeet");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");

        Keyboard.Listen(Key.Left, ButtonState.Down, Liikuta, "Liikkuu vasemmalle", pelaaja1, -NOPEUS);
        Keyboard.Listen(Key.Right, ButtonState.Down, Liikuta, "Liikkuu vasemmalle", pelaaja1, NOPEUS);
        Keyboard.Listen(Key.Up, ButtonState.Pressed, Hyppaa, "Pelaaja hyppää", pelaaja1, HYPPYNOPEUS);

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");

        Keyboard.lis


        //Lisätään pelihahmolle animaatiot liikkeen lisäksi
       
    }

    private void Lisaaanimaatiot()
    {
        
    }
    private void Liikuta(PlatformCharacter hahmo, double nopeus)
    {
        hahmo.Walk(nopeus);
    }

    private void Hyppaa(PlatformCharacter hahmo, double nopeus)
    {
        hahmo.Jump(nopeus);
    }

   

    private void TormaaKolikkoon(PhysicsObject hahmo, PhysicsObject kolikko)
    {

        MessageDisplay.Add("Keräsit Kolikon!");
        kolikko.Destroy();
    }

    private void TormaaPommiin(PhysicsObject hahmo, PhysicsObject pommi)
    {

        MessageDisplay.Add("BREH SÄ KUOLIT");
        pommi.Destroy();
    }




    //ANIMAATIOT

    private void PelaajaHyppy(object sender, KeyEventArgs e)
    {
        if (e.KeyValue == (char)Microsoft.Xna.Framework.Input.Keys.E)
        {
            

        }
    }

}