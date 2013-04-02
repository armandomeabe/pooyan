using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pooyan.GameObjects.Base;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pooyan.GameObjects
{
    class Enemy : GameObject
    {
        // Elementos propios de un personaje de juego.
        public float Life { get; set; }
        public bool IsAlive { get { return Life > 0; } } // Si salió de la pantalla también se considera como que isalive==false
        Animation Explosion { get; set; }

        public Enemy(ContentManager content, List<string> animationPaths, List<int> animationFramesForEachPath, Vector2 initialPosition, Vector2 initialSpeed)
            : base(content, animationPaths, animationFramesForEachPath)
        {
            base.speed = initialSpeed;
            base.position = initialPosition;
            this.Life = 100;
            Explosion = new Animation("/Extras/Explosion2/", 25);
        }

        public void Update(GameTime gameTime)
        {
            if (base.position.X < -80 || base.position.X > 1500 || base.position.Y < 0 || base.position.Y > 480)
                this.Life = 0;
            else
            {
                if (this.Life < 100)
                {
                    Explosion.Update(gameTime.ElapsedGameTime, position);
                }
                base.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.Life < 100)
            {
                Explosion.Draw(spriteBatch);
            }
            base.Draw(spriteBatch);
        }

        public void ReceiveDamage(float damages)
        {
            this.Life -= damages;
        }
    }
}
