using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Kablooey
{
	public class TeleportShip : Ship
	{
		public TeleportShip (Scene scene) : base(scene)
		{
			textureInfo     = new TextureInfo("/Application/textures/teleportShipTex.png");
			
			sprite 			= new SpriteUV(textureInfo);
			sprite.Quad.S 	= textureInfo.TextureSizef;
			sprite.Position = new Vector2(750.0f, 200.0f);
			health = 2.0f;
			speed  = 1.0f;
			alive = true;
			
			//Add to the current scene.
			scene.AddChild(sprite);
		}
	}
}