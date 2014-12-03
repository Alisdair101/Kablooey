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
			sprite	 		= new SpriteUV();
			sprite 			= new SpriteUV(textureInfo);
			sprite.Quad.S 	= textureInfo.TextureSizef;
			textureInfo  = new TextureInfo("/Application/textures/teleportShipTex.png");
			sprite.Position = new Vector2(0.0f, 0.0f);
			health = 2.0f;
			speed  = 1.0f;
		}
	}
}