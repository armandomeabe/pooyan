using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pooyan
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Personaje player;

        //String para ver el tiempo total de juego
        string totalTime;

        Random random;

        // Puntaje y fuente para mostrarlo
        int score;
        SpriteFont font;

        //Bandera para Pausa y gameOver
        TimeSpan timer;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 800,
                PreferredBackBufferHeight = 480
            };
            Window.AllowUserResizing = true;
            Content.RootDirectory = "Content";
            //Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
        }

        protected override void Initialize()
        {
            player = new Personaje();
            random = new Random();
            score = 0;
            timer = new TimeSpan(0);
            totalTime = "";
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // El SpriteBatch se usa para dibujar todo.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Animation AnimacionPersonaje = new Animation();
            Texture2D TexturaPersonaje = Content.Load<Texture2D>("pooAnimation");
            AnimacionPersonaje.Inicializar(TexturaPersonaje, Vector2.Zero, 25, 36, 3, 150, Color.White, 1f, true);
            player.Inicializar(AnimacionPersonaje, new Vector2(100, 100), Content, GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            player.Update(gameTime, Keyboard.GetState(), GraphicsDevice, new Vector2(5, 0));
            timer += gameTime.ElapsedGameTime;
            totalTime = timer.ToString(@"mm\:ss");

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            player.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
