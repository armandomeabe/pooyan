using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pooyan.GameObjects.Base
{
    public class GameObject
    {
        public Vector2 position;
        public Vector2 speed;
        Animation[] animatedGif;
        public ContentManager Content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="animationPaths">Son LAS rutaS donde se encuentran las animaciones para los diferentes estados del object (ej: Caminando, En reposo, disparando...)</param>
        /// <param name="animationFramesForEachPath">Cuantos frames tiene cada path?</param>
        public GameObject(ContentManager content, List<string> animationPaths, List<int> animationFramesForEachPath)
        {
            Content = content;
            if (!animationPaths.Count.Equals(animationFramesForEachPath.Count)) throw new Exception("La cantidad rutas de animaciones no se corresponde con la cantidad de frames para cada ruta");

            if (!animationPaths.Count().Equals(animationFramesForEachPath.Count())) throw new Exception("La cantidad de rutas no se corresponde con la cantidad de frames para cada ruta");

            this.position = new Vector2(350);
            animatedGif = new Animation[animationPaths.Count()];

            for (int i = 0; i < animationPaths.Count(); i++)
            {
                animatedGif[i] = new Animation(animationPaths[i], animationFramesForEachPath[i]);
                animatedGif[i].Load(content);
            }

            speed = Vector2.Zero; // Por defecto no se mueve "solo" sino que se llama a .Move()
        }

        protected void Update(GameTime gameTime)
        {
            position += speed;
            animatedGif[0].Update(gameTime.ElapsedGameTime, position);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animatedGif[0].Draw(spriteBatch);
        }

        public void Move(Vector2 relativeDirection)
        {
            this.position += relativeDirection;
        }
    }
}
