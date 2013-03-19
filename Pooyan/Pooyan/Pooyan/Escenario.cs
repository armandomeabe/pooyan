using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Pooyan
{
    public class Escenario
    {
        Texture2D textura;
        Vector2 posicion;

        public Escenario(ContentManager cm)
        {
            posicion = new Vector2(0, 0);
            textura = cm.Load<Texture2D>("Escenario/Fondo1");
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, posicion, Color.White);
        }
    }
}
