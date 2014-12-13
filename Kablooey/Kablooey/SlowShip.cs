using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Kablooey
{
	public class SlowShip : Ship
	{
		public SlowShip (Scene scene, int timeSeed) : base(scene)
		{
			textureInfo  = new TextureInfo(new Texture2D("/Application/textures/slowShipTex.png", false), new Vector2i(4, 1));
			
			sprite 			= new SpriteTile(textureInfo);
			sprite.Quad.S 	= textureInfo.TileSizeInPixelsf;
			
			Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.RandGenerator rand = new Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.RandGenerator(timeSeed);
			float randomY = rand.NextFloat(0, 470);
			sprite.Position = new Vector2(960, randomY);
			
			health = 10;
			healthBackUp = 10;
			speed  = 1.0f;
			alive = true;
			tileIndex = 0;
			
			sprite.ScheduleInterval( (dt) => 
			{
				if(tileIndex == 0)
				{
					tileDirection = true;
				}
				else if(tileIndex == 3)
				{	
					tileDirection = false;
				}
				
				if(tileDirection == true)
				{
					tileIndex += 1;
					sprite.TileIndex2D = new Vector2i(tileIndex, 0);
				}
				else if(tileDirection == false)
				{
					tileIndex -= 1;
					sprite.TileIndex2D = new Vector2i(tileIndex, 0);
				}
			}, 0.05f);
			
			//Add to the current scene.
			scene.AddChild(sprite);
		}
		
		public void Dispose()
		{
			textureInfo.Dispose();
		}
	}
}