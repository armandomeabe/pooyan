using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Pooyan
{
    class Animation
    {
        /// <summary>
        /// Duration of Each Frame
        /// </summary>
        public double FrameDuration { get; set; }

        /// <summary>
        /// Number of Frames
        /// </summary>
        public int StateCount { get; set; }

        /// <summary>
        /// Path of Image Resource
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Position on Screen
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets whether animation should be looped
        /// </summary>
        public bool Loop { get; set; }

        /// <summary>
        /// Format of File Path
        /// </summary>
        public string FilePathFormat { get; set; }

        /// <summary>
        /// Stores all Frames in Textures
        /// </summary>
        private Texture2D[] textures;

        /// <summary>
        /// Time elapsed since last state change
        /// </summary>
        private double timeSinceLastStateChange;

        /// <summary>
        /// Current State
        /// </summary>
        private int state;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="stateCount">Ruta donde se encuentran los png numerados de 0 a n: 0.png, 1.png...</param>
        public Animation(string Path, int stateCount)
        {
            FrameDuration = 0.1;
            Position = Vector2.Zero;
            Loop = true;
            FilePathFormat = "{0}{1}"; // Ej: "carpeta/" + "gif1"
            this.Path = Path;
            this.StateCount = stateCount;
        }

        // Cargar las imágenes en memoria.
        public void Load(ContentManager content)
        {
            textures = new Texture2D[StateCount];
            for (uint i = 0; i < StateCount; i++)
            {
                textures[i] = content.Load<Texture2D>(string.Format(FilePathFormat, Path, i));
            }
        }

        public void Update(TimeSpan elapsedGameTime, Vector2 position)
        {
            this.Position = position;
            timeSinceLastStateChange += elapsedGameTime.TotalSeconds;
            if (timeSinceLastStateChange > FrameDuration)
            {
                timeSinceLastStateChange -= FrameDuration;
                state++;
            }
            if (Loop && state == StateCount)
            {
                state = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin(); // Quizás debería sacar esto de acá, si ya fue iniciado desde donde se llama...
            if (state < StateCount)
            {
                spriteBatch.Draw(textures[state], Position, Color.White);
            }
            //spriteBatch.End(); // Y esto también.
        }
    }
}
