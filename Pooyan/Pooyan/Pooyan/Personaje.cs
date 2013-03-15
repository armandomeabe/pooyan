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
        public List<Proyectil> Proyectiles;
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

        public void Inicializar(Animation animation, ContentManager content, GraphicsDevice graphicsDevice, Texture2D texturaProyectil)
        {
            this.TexturaProyectil = texturaProyectil;
            this.DireccionDisparos = new Vector2(15, 0);
            this.graphicsDevice = graphicsDevice;
            Animacion = animation;
            Posicion = new Vector2(80, 100);
            Activo = true;
            Proyectiles = new List<Proyectil>();
        }

        public void Disparar(GameTime gameTime)
        {
            TimeSpan delayShoot = gameTime.TotalGameTime; // -TiempoDeUltimoDisparo[type];

            //if (delayShoot < DisparosFrecuencia[type])
            //    return;
            //TiempoDeUltimoDisparo[type] = gameTime.TotalGameTime;
            //Trace.WriteLine("delayShoot: " + delayShoot.ToString());

            Proyectiles.Add(new Proyectil(graphicsDevice.Viewport, TexturaProyectil, Posicion, DireccionDisparos));
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState, GraphicsDevice graphics, Vector2 DireccionDisparos)
        {
            Animacion.Posicion = Posicion;
            Animacion.Update(gameTime);

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                Posicion.Y -= velocidadMovimiento;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                Posicion.Y += velocidadMovimiento;
            }

            if (keyboardState.IsKeyDown(Keys.LeftControl))
                Disparar(gameTime);

            // Evitar que el personaje salga de la ventana
            Posicion.Y = MathHelper.Clamp(Posicion.Y, 125, 360);

            if (keyboardState.IsKeyDown(Keys.A))
                Disparar(gameTime);

            foreach (var proyectil in Proyectiles)
            {
                proyectil.Update(gameTime);
            }
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            Animacion.Draw(spriteBatch);
            foreach (var proyectil in Proyectiles)
            {
                proyectil.Draw(spriteBatch);
            }
        }
    }
}
