using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pooyan
{
    class Projectil
    {
        public Texture2D Textura;
        public Vector2 Posicion;
        public Vector2 Direccion;
        public bool Activo;
        public int DaniosQueCausa;
        Viewport viewport;

        public int Ancho
        {
            get { return Textura.Width; }
        }

        public int Alto
        {
            get { return Textura.Height; }
        }

        float VelocidadDeMovimiento;

        public Projectil(Viewport viewport, Texture2D textura, Vector2 posicion, Vector2 direccion, int DaniosQueCausa = 2)
        {
            this.Direccion = direccion;
            Textura = textura;
            Posicion = new Vector2(posicion.X, posicion.Y + (new Random(DateTime.Now.Millisecond).Next(-15, 15)));
            this.viewport = viewport;
            Activo = true;
            this.DaniosQueCausa = DaniosQueCausa;
            VelocidadDeMovimiento = 20f;
        }

        public void Update()
        {
            Posicion += Direccion;
            // Si se van de la pantalla los desactivo para que después se borren del vector.
            if (Posicion.X > viewport.Width || Posicion.X < 0 || Posicion.Y > viewport.Height || Posicion.Y < 0)
                Activo = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textura, Posicion, null, Color.White, 0f,
            new Vector2(Ancho / 2, Alto / 2), 1f, SpriteEffects.None, 0f);
        }
    }
}
