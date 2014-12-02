using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Kablooey
{
	public class Gun
	{
		private SpriteTile sprite;
		private TextureInfo textureInfo;
		private int tileIndex = 1;
		
		private bool fire = false;
		
		public Gun (Scene scene)
		{
			textureInfo = new TextureInfo(new Texture2D("/Application/textures/gun.png", false), new Vector2i(1, 2));
			
			sprite = new SpriteTile(textureInfo);
			sprite.Quad.S = textureInfo.TileSizeInPixelsf;
			sprite.Position = new Vector2(115.0f, 285.0f);
			
			sprite.TileIndex2D = new Vector2i(tileIndex, 0);
			
			if(fire == true)
			{
				tileIndex = 1;	
			}
			
			scene.AddChild(sprite);
		}
		
		public void Dispose()
		{
			textureInfo.Dispose();
		}
		
		public void Update(float deltaTime)
		{
			
		}
	}
}

