//test alpha 4-10-21//
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;
using System;
using System.Collections.Generic;

    public class main : PhysicsGame
    {
        public override void begin()
        {
            TasoTutoriaali();

        }


    

    public override void TasoTutoriaali()
    {
    protected override void Paint(Canvas canvas)
    {
        canvas.BrushColor = Color.Red;
        double x = canvas.Left + 100, y = canvas.Top - 100;
        canvas.DrawLine(new Vector(x - 50, y + 50), new Vector(x + 50, y - 50));
        canvas.DrawLine(new Vector(x + 50, y + 50), new Vector(x - 50, y - 50));
        base.Paint(canvas);
    }
    }


    protected override void Paint(Canvas canvas)
{
    canvas.BrushColor = Color.Red;
    double x = canvas.Left + 100, y = canvas.Top - 100;
    canvas.DrawLine(new Vector(x - 50, y + 50), new Vector(x + 50, y - 50));
    canvas.DrawLine(new Vector(x + 50, y + 50), new Vector(x - 50, y - 50));
    base.Paint(canvas);
}
    }