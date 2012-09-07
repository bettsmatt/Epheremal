using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Epheremal.Model;

namespace Epheremal.Assets
{
    class TextureProvider
    {
        public static Texture2D GetBlockTextureFor(Engine game, BlockType type, EntityState state)
        {
            ContentManager manager = new ContentManager(game.Services, "Content");
            bool good = state == EntityState.GOOD;
            switch (type)
            {
                case BlockType.TEST: return (good ? manager.Load<Texture2D>("test") : null);
            }
            throw new NotSupportedException();
        }

    }
}
