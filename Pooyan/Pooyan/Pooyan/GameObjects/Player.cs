using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pooyan.GameObjects.Base;

namespace Pooyan.GameObjects
{
    class Player : GameObject
    {
        // Elementos propios de un personaje de juego.
        public float Life { get; set; }
        public bool IsAlive { get { return Life > 0; } }

        public List<Projectil> Projectils { get; set; }
        public Texture2D ProjectilTexture { get; set; }

        // Extras
        ParticleEngine.Engine particleEngine;

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
            // Rastro de partículas
            var textures = new List<Texture2D>();
            textures.Add(content.Load<Texture2D>(@"Extras\spark"));
            particleEngine = new ParticleEngine.Engine(textures);
            Projectils = new List<Projectil>();
            ProjectilTexture = content.Load<Texture2D>(@"Varias\bala1");
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState, GamePadState gamepadState)
        {
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                base.Move(new Vector2(0, -5));
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                base.Move(new Vector2(0, 5));
            }
            if (keyboardState.IsKeyDown(Keys.LeftControl))
            {
                Shoot(10);
            }

            foreach (var projectil in Projectils.ToList())
            {
                projectil.position.X += 10;
                if (projectil.position.X > 760)
                    Projectils.Remove(projectil);
            }

            base.Update(gameTime);

            // Partículas
            particleEngine.EmitterLocation = new Vector2(base.position.X + 15, base.position.Y + 25);
            particleEngine.Update();
        }

        public void Shoot(int burstQuantity = 1)
        {
            for (int i = 0; i < burstQuantity; i++)
            {
                Projectils.Add(new Projectil() { position = new Vector2(position.X + 30, position.Y + 20 + (new Random()).Next(-10, 10)), texture = ProjectilTexture });
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var projectil in Projectils)
            {
                spriteBatch.Draw(projectil.texture, projectil.position, Color.White);
            }
            particleEngine.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }

        public void ReceiveDamage(float damages)
        {
            this.Life -= damages;
        }
    }
}
