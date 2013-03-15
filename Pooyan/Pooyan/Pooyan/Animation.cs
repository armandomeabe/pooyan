using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pooyan
{
    class Animation
    {
        // Textura donde estan TODOS los sprites en orden y donde todos tienen el MISMO ANCHO
        Texture2D TexturaSprites;

        // Escala que se aplica a cada sprite
        float Escala;

        // Cuanto tiempo pasó desde el último cambio de frames? (Se usa con la variable siguiente)
        int TiempoTranscurrido;

        // Cuanto tiempo transcurre entre frame y frame? Esto siempre va a ser una subestimación
        int TiempoEntreFrames;

        // Cuantos frames hay en total? El ancho total de la imagen debería ser esta variable * ancho de cada frame
        int FramesEnLaImagen;

        // El índice del frame que se está mostrando en un momento dado
        int FrameActual;

        // Esto es porque se pueden aplicar efectos de color a los frames, como que se ponga rojo si le pegan.
        Color color;

        // La parte de la textura que se quiere dibujar
        Rectangle RectanguloOrigen = new Rectangle();

        // El area donde se quiere dibujar el frame en la ventana
        Rectangle RectanguloDestino = new Rectangle();

        // Útiles para pasar de un frame a otro dentro de una imagen con muchos sprites o frames
        public int AnchoFrame;
        public int AltoFrame;

        public bool Activo;

        // Determina si se repite por siempre la animación
        public bool Repetir;

        public Vector2 Posicion;

        public void Inicializar(Texture2D texture, Vector2 position,
            int frameWidth, int frameHeight, int frameCount,
            int frametime, Color color, float scale, bool looping)
        {
            this.color = color;
            this.AnchoFrame = frameWidth;
            this.AltoFrame = frameHeight;
            this.FramesEnLaImagen = frameCount;
            this.TiempoEntreFrames = frametime;
            this.Escala = scale;

            Repetir = looping;
            Posicion = position;
            TexturaSprites = texture;

            TiempoTranscurrido = 0;
            FrameActual = 0;

            Activo = true;
        }

        public void Update(GameTime gameTime)
        {
            if (!Activo) return;

            // Se actualiza el tiempo transcurrido, gameTime ya viene desde Game1 y se calcula solo
            TiempoTranscurrido += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            // Si el tiempo transcurrido es mayor al tiempo entre frame y frame, hay que cambiarlo y resetearlo
            if (TiempoTranscurrido > TiempoEntreFrames)
            {
                FrameActual++;
                // Si el frame actual es igual a la cantidad total de frames, se resetea el actual
                if (FrameActual == FramesEnLaImagen)
                {
                    FrameActual = 0;
                    if (!Repetir) Activo = false;
                }
                TiempoTranscurrido = 0;
            }

            // El frame correcto se saca de multiplicar el frame actual por el ancho de cada sprite
            // O sea que tienen que ser todos iguales
            RectanguloOrigen = new Rectangle(FrameActual * AnchoFrame, 0, AnchoFrame, AltoFrame);

            // El frame correcto se saca de multiplicar el frame actual por el ancho de cada sprite
            // O sea que tienen que ser todos iguales
            RectanguloDestino = new Rectangle((int)Posicion.X - (int)(AnchoFrame * Escala) / 2,
            (int)Posicion.Y - (int)(AltoFrame * Escala) / 2,
            (int)(AnchoFrame * Escala),
            (int)(AltoFrame * Escala));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Activo)
                spriteBatch.Draw(TexturaSprites, RectanguloDestino, RectanguloOrigen, color);
        }
    }
}
