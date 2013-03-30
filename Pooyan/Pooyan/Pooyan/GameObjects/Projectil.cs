using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pooyan.GameObjects.Base;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Pooyan.GameObjects
{
    class Projectil : SimpleDrawableObject
    {
        ParticleEngine.Engine particleEngine;
        Animation explosion1;

        public bool activo = true;
        public bool fueraDeRango { get { return position.X <= 760; } }

        public Projectil(ContentManager content)
            : base()
        {
            var textures = new List<Texture2D>();
            textures.Add(content.Load<Texture2D>(@"Extras\spark"));
            particleEngine = new ParticleEngine.Engine(textures);

            explosion1 = new Animation(@"Extras\Explosion1\", 21);
            explosion1.Load(content);
        }

        public void Update(GameTime gameTime)
        {
            //activo = (position.X <= 760);

            if (fueraDeRango)
                position.X += 10;

            // Partículas
            particleEngine.EmitterLocation = new Vector2(base.position.X, base.position.Y);
            particleEngine.Update();

            // Explosión
            if (!fueraDeRango)
                explosion1.Update(gameTime.ElapsedGameTime, new Vector2(position.X,position.Y - 100));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (fueraDeRango)
            {
                particleEngine.Draw(spriteBatch);
                spriteBatch.Draw(texture, position, Color.White);
            }
            else
            {
                if (!explosion1.animacionFinalizada)
                    explosion1.Draw(spriteBatch);
                else
                    activo = false;
            }
        }
    }
}
