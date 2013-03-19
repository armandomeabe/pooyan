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
using Pooyan.GameObjects;

namespace Pooyan
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Escenario escenario;

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
            Window.AllowUserResizing = false;
            Content.RootDirectory = "Content";
            //Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
        }

        protected override void Initialize()
        {
            player = new Player(Content, new List<string>() { @"Varias\Ovni\" }, new List<int> { 11 });
            escenario = new Escenario(Content);
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

            //Animation AnimacionPersonaje = new Animation();
            //Texture2D TexturaPersonaje = Content.Load<Texture2D>("Personajes/pooAnimation");
            //AnimacionPersonaje.Inicializar(TexturaPersonaje, Vector2.Zero, 25, 36, 3, 150, Color.White, 1f, true);
            //var texturaProyectil = Content.Load<Texture2D>("Varias/bala1");
            //player.Inicializar(AnimacionPersonaje, Content, GraphicsDevice, texturaProyectil);
        }

        protected override void Update(GameTime gameTime)
        {
            escenario.Update(gameTime);
            player.Update(gameTime, Keyboard.GetState(), GamePad.GetState(0));
            timer += gameTime.ElapsedGameTime;
            totalTime = timer.ToString(@"mm\:ss");
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            escenario.Draw(spriteBatch);
            player.Draw(spriteBatch);
            base.Draw(gameTime);
            spriteBatch.End();
            
        }
    }
}
