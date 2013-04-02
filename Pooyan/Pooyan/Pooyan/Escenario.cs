using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Pooyan.GameObjects;

namespace Pooyan
{
    public class Escenario
    {
        Texture2D textura;
        Vector2 posicion;
        StableGameObject comandante { get; set; }
        List<Enemy> enemigos { get; set; }
        public Player playerReference { get; set; } // Referencia a la instancia de player del Game1.cs, igual en C# todos los objetos son referencias por defecto.
        ContentManager contentManager { get; set; }
        public Escenario(ContentManager contentManager, ref Player player)
        {
            this.contentManager = contentManager;
            this.playerReference = player;
            posicion = new Vector2(0, 0);
            textura = contentManager.Load<Texture2D>("Escenario/Fondo1");
            comandante = new StableGameObject(contentManager, new List<string>(1) { "Extras/Commander/" }, new List<int>(1) { 34 }, new Vector2(460, 20));

            enemigos = new List<Enemy>() { new Enemy(contentManager, new List<string>(1) { "Extras/ZombieDoctor/" }, new List<int>(1) { 11 }, new Vector2(800, 10), Vector2.Zero) };
        }

        public void Update(GameTime gameTime)
        {
            comandante.Update(gameTime);

            if (!enemigos.Any())
            {
                enemigos.Add(new Enemy(contentManager,
                    new List<string>(1) { "Extras/ZombieDoctor/" },
                    new List<int>(1) { 11 },
                    new Vector2(800, 10), Vector2.Zero));
                enemigos.Add(new Enemy(contentManager,
                    new List<string>(1) { "Extras/ZombieDoctor/" },
                    new List<int>(1) { 11 },
                    new Vector2(790, 10), Vector2.Zero));
                enemigos.Add(new Enemy(contentManager,
                    new List<string>(1) { "Extras/ZombieDoctor/" },
                    new List<int>(1) { 11 },
                    new Vector2(750, 10), Vector2.Zero));
            }

            foreach (var enemigo in (from e in enemigos where e.IsAlive select e))
            {
                if (enemigo.position.X > 500)
                    enemigo.Move(new Vector2(-1, 0));
                else
                    enemigo.Move(new Vector2(0, 1));
            }

            foreach (var enemigo in enemigos.ToList())
            {
                foreach (var proyectilEnElAire in playerReference.Projectils)
                {
                    if (proyectilEnElAire.activo && Pooyan.Utils.XY0Utils.EstaRelativamenteCerca(proyectilEnElAire.position, enemigo.position, 20))
                    {
                        //enemigo.ReceiveDamage(10);
                        enemigo.Life = 0;
                        proyectilEnElAire.activo = false;
                    }
                }

                if (enemigo.IsAlive)
                    enemigo.Update(gameTime);
                else
                    enemigos.Remove(enemigo);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, posicion, Color.White);
            comandante.Draw(spriteBatch);

            foreach (var enemigo in (from e in enemigos where e.IsAlive select e))
                enemigo.Draw(spriteBatch);
        }
    }
}
