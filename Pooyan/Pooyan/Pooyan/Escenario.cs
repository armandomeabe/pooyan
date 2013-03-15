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

        Animation bichito;
        Texture2D bichitoTextura;

        public Escenario(ContentManager cm)
        {
            posicion = new Vector2(0, 0);
            textura = cm.Load<Texture2D>("Escenario/Fondo1");
            bichito = new Animation();
            bichitoTextura = cm.Load<Texture2D>("Escenario/bichito");
            bichito.Inicializar(bichitoTextura, new Vector2(50,80), 15*3, 15*3, 3, 150, Color.White, 1f, true);
        }

        public void Update(GameTime gameTime)
        {
            bichito.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, posicion, Color.White);
            bichito.Draw(spriteBatch);
        }
    }
}
