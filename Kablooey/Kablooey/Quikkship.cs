using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Kablooey
{
	public class QuikkShip : Ship
	{
		public QuikkShip (Scene scene) : base(scene)
		{
			textureInfo  = new TextureInfo("/Application/textures/quikkShipTex.png");
			
			sprite 			= new SpriteUV(textureInfo);
			sprite.Quad.S 	= textureInfo.TextureSizef;
			sprite.Position = new Vector2(0.0f, 0.0f);
			
			health = 1.0f;
			speed  = 2.0f;
			alive = true;
			
			//Add to the current scene.
			scene.AddChild(sprite);
		}
	}
}

