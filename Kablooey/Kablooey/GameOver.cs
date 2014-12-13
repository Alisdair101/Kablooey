using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Kablooey
{
	public class GameOver
	{
		
		
		private static SpriteUV 	sprite;
		private static TextureInfo	textureInfo;
		
		
		public GameOver (Scene scene)
		{
			textureInfo  = new TextureInfo("/Application/textures/menuEnd.png");
			
			sprite	 		= new SpriteUV();
			sprite 			= new SpriteUV(textureInfo);	
			sprite.Quad.S 	= textureInfo.TextureSizef;
			sprite.Position = new Vector2(0.0f,0.0f);
			
			scene.AddChild(sprite);
			
		}
		
		public void Dispose()
		{
			textureInfo.Dispose();
		}
		
		public void Update(float deltaTime)
		{
			sprite.Visible = false;
		}
	}
}
