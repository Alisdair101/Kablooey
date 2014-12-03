using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Kablooey
{
	public class SlowShip : Ship
	{
		public SlowShip (Scene scene) : base(scene)
		{
			textureInfo  = new TextureInfo("/Application/textures/slowShipTex.png");
			
			sprite 			= new SpriteUV(textureInfo);
			sprite.Quad.S 	= textureInfo.TextureSizef;
			sprite.Position = new Vector2(750.0f, 300.0f);
			health = 10.0f;
			
			speed  = 0.5f;
			alive = true;
			
			//Add to the current scene.
			scene.AddChild(sprite);
		}
	}
}

