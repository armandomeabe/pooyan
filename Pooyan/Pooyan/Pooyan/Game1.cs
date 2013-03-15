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
        Vector2 PosicionInicialPersonaje;

        KeyboardState estadoActualDelTeclado;
        KeyboardState estadoPrevioDelTeclado;

        //String para ver el tiempo total de juego
        string totalTime;

        // Fondos
        Texture2D fondoEstatico;
        Texture2D fondoMenuPrincipal;

        // Enemigos
        //Texture2D texturaEnemigo;
        //Texture2D texturaEnemigoTerrestre;
        //List<Enemigo> Enemigos;
        //List<Enemigo> EnemigosTerrestres;

        // La frecuencia en que aparecen los enemigos
        //TimeSpan EnemigoFrecuenciaSpawn;
        //TimeSpan EnemigoTiempoDeUltimaAparicion;

        Random random;

        // Sonidos
        //SoundEffect SonidoExplosion;
        //Song MusicaEnJuego;

        // Puntaje y fuente para mostrarlo
        int score;
        SpriteFont font;

        //Bandera para Pausa y gameOver
        bool pause;
        bool gameOver;
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
            // El juego empieza pausado y muestra el fondo.
            pause = true;

            //Initialize the player class
            player = new Personaje();

            //Enemigos = new List<Enemigo>();

            //EnemigoTiempoDeUltimaAparicion = TimeSpan.Zero;
            //EnemigoFrecuenciaSpawn = TimeSpan.FromSeconds(1.0f);

            random = new Random();

            //Explosiones = new List<Animacion>();
            score = 0;
            pause = false;
            gameOver = false;
            timer = new TimeSpan(0);
            totalTime = "";

            base.Initialize();
        }

        private void InicializarFondos()
        {
            // Texturas de fondos y objetos jugables
            //fondoEstatico = Content.Load<Texture2D>("mainbackground");
            //fondoMenuPrincipal = Content.Load<Texture2D>("mainMenuTall");
            //texturaEnemigo = Content.Load<Texture2D>("mineAnimation");
            //texturaExplosion = Content.Load<Texture2D>("explosion");
        }

        protected override void LoadContent()
        {
            // El SpriteBatch se usa para dibujar todo.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Animation AnimacionPersonaje = new Animation();
            Texture2D TexturaPersonaje = Content.Load<Texture2D>("pooAnimation");
            AnimacionPersonaje.Inicializar(TexturaPersonaje, Vector2.Zero, 25, 36, 3, 30, Color.White, 1f, true);

            PosicionInicialPersonaje = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X+20, GraphicsDevice.Viewport.TitleSafeArea.Y
            + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            player.Inicializar(AnimacionPersonaje, PosicionInicialPersonaje, Content, GraphicsDevice);

            InicializarFondos();

            // Sonidos
            //MusicaEnJuego = Content.Load<Song>("sound/gameMusic");
            //SonidoExplosion = Content.Load<SoundEffect>("sound/explosion");
            //font = Content.Load<SpriteFont>("gameFont");
            //PlayMusic(MusicaEnJuego);
        }

        /*private void PlayMusic(Song song)
        {
            // No se bien que quieren decir con esto:
            // Due to the way the MediaPlayer plays music,
            // we have to catch the exception. Music will play when the game is not tethered
            try
            {
                // Play the music
                MediaPlayer.Play(song);
                // Loop the currently playing song
                MediaPlayer.IsRepeating = true;
            }
            catch { }
        } */

        /*
        private void AgregarEnemigo()
        {
            var AnimacionEnemigo = new Animacion();
            var Posicion = new Vector2(GraphicsDevice.Viewport.Width + texturaEnemigo.Width / 2, random.Next(100, GraphicsDevice.Viewport.Height - 100));
            AnimacionEnemigo.Inicializar(texturaEnemigo, Posicion, 47, 61, 8, 30, Color.White, 1f, true);

            var Enemigo = new Enemigo();
            Enemigo.Inicializar(AnimacionEnemigo, Content, GraphicsDevice, new Vector2(-5, 0), true);
            Enemigos.Add(Enemigo);
        } */

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || estadoActualDelTeclado.IsKeyDown(Keys.Q))
                this.Exit();

            // Save the previous state of the keyboard and game pad so we can determinesingle key/button presses
            estadoPrevioDelTeclado = estadoActualDelTeclado;

            // Read the current state of the keyboard and gamepad and store it
            estadoActualDelTeclado = Keyboard.GetState();

            //Cambia la flag cuando se presiona pausa y pausa o reproduce la música de fondo
            /*if (estadoActualDelTeclado.IsKeyDown(Keys.P))
            {
                pause = !pause;
                if (pause && !gameOver)
                {
                    MediaPlayer.Pause();
                }
                else
                {
                    MediaPlayer.Resume();
                }
            }*/

            //Si está en pausa no actualiza nada
            if (!pause && !gameOver)
            {
                ActualizarPersonaje(gameTime);

                //ActualizarEnemigos(gameTime);
                UpdateCollision();
                //player.ActualizarProyectiles();

                //EnemigoFrecuenciaSpawn = TimeSpan.FromSeconds(1.0f * random.Next(30));

                timer += gameTime.ElapsedGameTime;
                totalTime = timer.ToString(@"mm\:ss");

                base.Update(gameTime);
            }

            //Para debuggear - Tecla N reinicia el juego
            if (estadoActualDelTeclado.IsKeyDown(Keys.N) && gameOver)
            {
                pause = gameOver = false;

                player.Activo = true;
                player.Posicion = PosicionInicialPersonaje;

                score = 0;

                //Enemigos.Clear();

                timer = new TimeSpan(0);
            }
        }

        private void ActualizarPersonaje(GameTime gameTime)
        {
            player.Update(gameTime, estadoActualDelTeclado, GraphicsDevice, new Vector2(5, 0));
        }

        private void UpdateCollision()
        {
            // Use the Rectangle's built-in intersect functionto 
            // determine if two objects are overlapping
            Rectangle rectangle1;
            Rectangle rectangle2;

            // Only create the rectangle once for the player
            rectangle1 = new Rectangle((int)player.Posicion.X,
            (int)player.Posicion.Y,
            player.Ancho,
            player.Alto);

            // Do the collision between the player and the enemies
            /*for (int i = 0; i < Enemigos.Count; i++)
            {
                rectangle2 = new Rectangle((int)Enemigos[i].Posicion.X,
                (int)Enemigos[i].Posicion.Y,
                Enemigos[i].Ancho,
                Enemigos[i].Alto);

                // Determine if the two objects collided with each
                // other
                if (rectangle1.Intersects(rectangle2))
                {
                    // Subtract the health from the player based on
                    // the enemy damage
                    player.Vida -= Enemigos[i].Danios;

                    // Since the enemy collided with the player
                    // destroy it
                    Enemigos[i].Vida = 0;

                }

            } */

            // Proyectiles enemigos contra el personaje
            // Linq: De los enemigos y los enemigos terrestres seleccionar sus proyectiles
            /*foreach (var proyectiles in (from e in Enemigos select e.proyectiles))
            {
                foreach (var proyectil in proyectiles)
                {
                    if (rectangle1.Intersects(new Rectangle((int)proyectil.Posicion.X, (int)proyectil.Posicion.Y, (int)proyectil.Ancho, (int)proyectil.Alto)))
                    {
                        player.Vida -= proyectil.DaniosQueCausa;
                        proyectil.Activo = false;
                        SonidoExplosion.Play();
                    }
                }
            }*/

            // Proyectiles enemigos contra proyectiles del personaje
            /*foreach (var proyectiles in (from e in Enemigos.Concat(EnemigosTerrestres) select e.proyectiles))
            {
                foreach (var proyectil in proyectiles)
                {
                    for (int i = 0; i < player.Proyectiles.Count; i++)
                    {
                        // Create the rectangles we need to determine if we collided with each other
                        rectangle1 = new Rectangle((int)player.Proyectiles[i].Posicion.X -
                        player.Proyectiles[i].Ancho / 2, (int)player.Proyectiles[i].Posicion.Y -
                        player.Proyectiles[i].Alto / 2, player.Proyectiles[i].Ancho, player.Proyectiles[i].Alto);

                        if (rectangle1.Intersects(new Rectangle((int)proyectil.Posicion.X, (int)proyectil.Posicion.Y, (int)proyectil.Ancho, (int)proyectil.Alto)))
                        {
                            proyectil.Activo = false;
                        }
                    }
                }
            }

            // Projectile vs Enemy Collision
            for (int i = 0; i < player.Proyectiles.Count; i++)
            {
                for (int j = 0; j < Enemigos.Count; j++)
                {
                    // Create the rectangles we need to determine if we collided with each other
                    rectangle1 = new Rectangle((int)player.Proyectiles[i].Posicion.X -
                    player.Proyectiles[i].Ancho / 2, (int)player.Proyectiles[i].Posicion.Y -
                    player.Proyectiles[i].Alto / 2, player.Proyectiles[i].Ancho, player.Proyectiles[i].Alto);

                    rectangle2 = new Rectangle((int)Enemigos[j].Posicion.X - Enemigos[j].Ancho / 2,
                    (int)Enemigos[j].Posicion.Y - Enemigos[j].Alto / 2,
                    Enemigos[j].Ancho, Enemigos[j].Alto);

                    // Determine if the two objects collided with each other
                    if (rectangle1.Intersects(rectangle2))
                    {
                        Enemigos[j].Vida -= player.Proyectiles[i].DaniosQueCausa;
                        player.Proyectiles[i].Activo = false;
                        //El puntaje se suma sólo si el enemigo es alcanzado por un proyectil y su vida es 0
                        if (Enemigos[j].Vida == 0)
                            score += Enemigos[j].Puntos;
                    }
                }
            }*/
            // If the player health is less than zero we died
            //player.Activo = false;
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (pause || gameOver)
            {   // Si estamos en pausa solo dibujo el fondo de menú principal y termina el método.
                spriteBatch.Draw(fondoMenuPrincipal, Vector2.Zero, Color.White);
            }
            if (player.Activo && !gameOver)
            {
                //spriteBatch.Draw(fondoEstatico, Vector2.Zero, Color.White);
                //spriteBatch.Draw(fondoEstatico, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);

                player.Draw(spriteBatch);

                /*foreach (var enemigo in (from enemigos in Enemigos select enemigos))
                {
                    enemigo.Draw(spriteBatch);
                }*/

                //spriteBatch.DrawString(font, "Puntaje: " + score, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y), Color.White);
                //spriteBatch.DrawString(font, "Tiempo: " + totalTime, new Vector2(GraphicsDevice.Viewport.Width - 110, GraphicsDevice.Viewport.TitleSafeArea.Y), Color.White);
            }
            else
            {
                gameOver = true;
                //spriteBatch.DrawString(font, "Puntaje Total: " + score, new Vector2(GraphicsDevice.Viewport.Width / 2 - 300, GraphicsDevice.Viewport.Height / 2 - 15), Color.White);
                //spriteBatch.DrawString(font, "Tiempo Total: " + totalTime, new Vector2(GraphicsDevice.Viewport.Width / 2 - 300, GraphicsDevice.Viewport.Height / 2 + 15), Color.White);
                //spriteBatch.DrawString(font, "Presione N para\njugar nuevamente", new Vector2(GraphicsDevice.Viewport.Width / 2 - 300, GraphicsDevice.Viewport.Height / 2 + 45), Color.Red);
            }

            if (pause)
            {
                //spriteBatch.DrawString(font, "Pausa", new Vector2(GraphicsDevice.Viewport.Width / 2 - 100, GraphicsDevice.Viewport.Height / 2 + 45), Color.White);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
