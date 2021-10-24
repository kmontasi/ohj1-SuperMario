using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;
using System;
using System.Collections.Generic;

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
    private PlatformCharacter pelaaja1;

    //PELAAJAN KUVA
    //!!Lisätään seuraavassa vaiheeessa valikko josta valittua mallia voi muokata. eli eirlaisia hahmoja
    private Image pelaajanKuva = LoadImage("norsu.png");

    //INTERAKTIIVISET ESINEET
    //KOLIKOT !!Vaihda
    private Image kolikkoKuva = LoadImage("tahti.png");
    //POMMI 
    //satuttaa pelaajaa
    private Image pommiKuva = LoadImage("pommi.png");

    //VIHOLLISET
    //VIHOLLINEN YKSI
    private Image vihollisenKuva = LoadImage("vihollinen.png");



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
        kentta.SetTileMethod('-', LisaaPommi);
        kentta.SetTileMethod('*', LisaaKolikko);
        kentta.SetTileMethod('p', LisaaPelaaja);
        kentta.SetTileMethod('1', LisaaVihollinen);

        kentta.Execute(RUUDUN_KOKO, RUUDUN_KOKO);
        Level.CreateBorders();
        Level.Background.CreateGradient(Color.White, Color.SkyBlue);
    }


    private void LisaaTaso(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject taso = PhysicsObject.CreateStaticObject(leveys, korkeus);
        taso.Position = paikka;
        taso.Color = Color.Green;
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
    private void LisaaPommi(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject pommi = PhysicsObject.CreateStaticObject(leveys, korkeus);
        pommi.IgnoresCollisionResponse = true;
        pommi.Position = paikka;
        pommi.Image = kolikkoKuva;
        pommi.Tag = " pommi";
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
    private void LisaaVihollinen(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject vihollinen = PhysicsObject.CreateStaticObject(leveys, korkeus);
        vihollinen.IgnoresCollisionResponse = true;
        vihollinen.Position = paikka;
        vihollinen.Image = pommiKuva;
        vihollinen.Tag = "vihollinen";
        Add(vihollinen);
    }
    private void LisaaNappaimet()
    {
        Keyboard.Listen(Key.F1, ButtonState.Pressed, ShowControlHelp, "Näytä ohjeet");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");

        Keyboard.Listen(Key.Left, ButtonState.Down, Liikuta, "Liikkuu vasemmalle", pelaaja1, -NOPEUS);
        Keyboard.Listen(Key.Right, ButtonState.Down, Liikuta, "Liikkuu vasemmalle", pelaaja1, NOPEUS);
        Keyboard.Listen(Key.Up, ButtonState.Pressed, Hyppaa, "Pelaaja hyppää", pelaaja1, HYPPYNOPEUS);

        ControllerOne.Listen(Button.Back, ButtonState.Pressed, Exit, "Poistu pelistä");

        ControllerOne.Listen(Button.DPadLeft, ButtonState.Down, Liikuta, "Pelaaja liikkuu vasemmalle", pelaaja1, -NOPEUS);
        ControllerOne.Listen(Button.DPadRight, ButtonState.Down, Liikuta, "Pelaaja liikkuu oikealle", pelaaja1, NOPEUS);
        ControllerOne.Listen(Button.A, ButtonState.Pressed, Hyppaa, "Pelaaja hyppää", pelaaja1, HYPPYNOPEUS);

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
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
}