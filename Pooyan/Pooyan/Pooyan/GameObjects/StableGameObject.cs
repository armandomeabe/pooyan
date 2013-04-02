using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pooyan.GameObjects.Base;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Pooyan.GameObjects
{
    class StableGameObject : GameObject
    {
        public StableGameObject(ContentManager content, List<string> animationPaths, List<int> animationFramesForEachPath, Vector2 initialPosition)
            : base(content, animationPaths, animationFramesForEachPath)
        {
            base.speed = Vector2.Zero;
            base.position = initialPosition;
        }

        public void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
