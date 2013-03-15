using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Pooyan
{
    class Personaje
    {
        Vector2 DireccionDisparos;
        // Disparos
        public Texture2D TexturaProyectil;
        public List<Projectil> Proyectiles;
        SoundEffect SonidoLaser;
        // El rate de disparo del personaje

        public Animation Animacion;
        public Vector2 Posicion;
        public bool Activo;
        float velocidadMovimiento = 8.0f;

        // Referencia al GraphicsDevice principal del juego.
        GraphicsDevice graphicsDevice;

        public int Ancho
        {
            get { return Animacion.AnchoFrame; }
        }

        public int Alto
        {
            get { return Animacion.AltoFrame; }
        }

        public void Inicializar(Animation animation, Vector2 position, ContentManager content, GraphicsDevice graphicsDevice)
        {
            this.DireccionDisparos = new Vector2(15, 0);
            this.graphicsDevice = graphicsDevice;
            Animacion = animation;
            Posicion = position;
            Activo = true;

            // Proyectiles
            //Proyectiles = new List<Projectil>();
            //TexturaProyectil = content.Load<Texture2D>("laser");
            //SonidoLaser = content.Load<SoundEffect>("sound/laserFire");
        }

        public void Disparar(int type, GameTime gameTime)
        {
            TimeSpan delayShoot = gameTime.TotalGameTime; // -TiempoDeUltimoDisparo[type];

            //if (delayShoot < DisparosFrecuencia[type])
            //    return;
            //TiempoDeUltimoDisparo[type] = gameTime.TotalGameTime;
            //Trace.WriteLine("delayShoot: " + delayShoot.ToString());

            Proyectiles.Add(new Projectil(graphicsDevice.Viewport, TexturaProyectil, Posicion, DireccionDisparos));
            SonidoLaser.Play();
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState, GraphicsDevice graphics, Vector2 DireccionDisparos)
        {
            Animacion.Posicion = Posicion;
            Animacion.Update(gameTime);

            //ActualizarProyectiles();

            // Teclado / Dpad
            /*if (keyboardState.IsKeyDown(Keys.Left))
            {
                Posicion.X -= velocidadMovimiento;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                Posicion.X += velocidadMovimiento;
            }*/
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                Posicion.Y -= velocidadMovimiento;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                Posicion.Y += velocidadMovimiento;
            }

            // Evitar que el personaje salga de la ventana
            Posicion.X = MathHelper.Clamp(Posicion.X, 0, graphics.Viewport.Width - Ancho);
            Posicion.Y = MathHelper.Clamp(Posicion.Y, 0, graphics.Viewport.Height - Alto);

            if (keyboardState.IsKeyDown(Keys.A))
                Disparar(0, gameTime);
        }

       /* public void ActualizarProyectiles()
        {
            // Update the Projectiles
            for (int i = Proyectiles.Count - 1; i >= 0; i--)
            {
                Proyectiles[i].Update();
                if (Proyectiles[i].Activo == false)
                {
                    Proyectiles.RemoveAt(i);
                }
            }
        }*/

        public void Draw(SpriteBatch spriteBatch)
        {
            /*foreach (var p in Proyectiles)
            {
                p.Draw(spriteBatch);
            }*/

            Animacion.Draw(spriteBatch);
        }
    }
}
