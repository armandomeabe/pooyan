using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pooyan.GameObjects.Base;
using Microsoft.Xna.Framework.Audio;

namespace Pooyan.GameObjects
{
    class Player : GameObject
    {
        // Elementos propios de un personaje de juego.
        public float Life { get; set; }
        public bool IsAlive { get { return Life > 0; } }

        public List<Projectil> Projectils { get; set; }
        public Texture2D ProjectilTexture { get; set; }

        //Sonido
        SoundEffect sonidoDisparo;

        /// <summary>
        /// Constructor del personaje.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="animationPaths">LAS rutaS donde encontrar los diferentes estados del personaje (Disparando, en reposo), son carpetas que contienen N frames: 0.png, 1.png...</param>
        public Player(ContentManager content, List<string> animationPaths, List<int> animationFramesForEachPath)
            : base(content, animationPaths, animationFramesForEachPath)
        {
            this.Life = 100;
            base.position = new Vector2(52, 100);

            sonidoDisparo = Content.Load<SoundEffect>("sound/laserFire");

            Projectils = new List<Projectil>();
            ProjectilTexture = content.Load<Texture2D>(@"Varias\bala1");
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState, GamePadState gamepadState)
        {
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                if (position.Y >= 150)
                    base.Move(new Vector2(0, -5));
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                if (position.Y <= 350)
                    base.Move(new Vector2(0, 5));
            }
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                Shoot(10);
            }

            foreach (var projectil in Projectils.ToList())
            {
                projectil.Update(gameTime);
                if (!projectil.activo)
                    Projectils.Remove(projectil);
            }

            base.Update(gameTime);
        }

        public void Shoot(int burstQuantity = 1)
        {
            for (int i = 0; i < burstQuantity; i++)
            {
                sonidoDisparo.Play();
                Projectils.Add(new Projectil(base.Content) { position = new Vector2(position.X + 30, position.Y + 20 + (new Random()).Next(-10, 10)), texture = ProjectilTexture });
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var projectil in Projectils)
            {
                projectil.Draw(spriteBatch);
            }
            base.Draw(spriteBatch);
        }

        public void ReceiveDamage(float damages)
        {
            this.Life -= damages;
        }
    }
}
