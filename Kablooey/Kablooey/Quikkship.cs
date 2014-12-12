using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Kablooey
{
	public class QuikkShip : Ship
	{
		public QuikkShip (Scene scene, int timeSeed) : base(scene)
		{
			textureInfo  = new TextureInfo("/Application/textures/quikkShipTex.png");
			
			sprite 			= new SpriteUV(textureInfo);
			sprite.Quad.S 	= textureInfo.TextureSizef;
			
			Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.RandGenerator rand = new Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.RandGenerator(timeSeed);
			float randomY = rand.NextFloat(0, 470);
			sprite.Position = new Vector2(960, randomY);
			
			health = 1;
			healthBackUp = 1;
			speed  = 4.0f;
			alive = true;
			
			//Add to the current scene.
			scene.AddChild(sprite);
		}
		
		public void Dispose()
		{
			textureInfo.Dispose();
		}
	}
}

