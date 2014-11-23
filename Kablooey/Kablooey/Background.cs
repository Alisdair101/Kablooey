using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Kablooey
{
	public class Background
	{
		private SpriteUV 	sprite;
		private TextureInfo textureInfo;
		
		public Background (Scene scene)
		{
			textureInfo = new TextureInfo("/Application/textures/background.png");
			
			sprite = new SpriteUV(textureInfo);
			sprite.Quad.S = textureInfo.TextureSizef;
			
			sprite.Position = new Vector2(0.0f, 0.0f);
			
			scene.AddChild (sprite);
		}
		
		public void Dispose()
		{
			textureInfo.Dispose();
		}
	}
}