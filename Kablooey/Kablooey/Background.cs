using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Kablooey
{
	public class Background
	{
		private SpriteUV[] 	backgroundSprites;
		private TextureInfo textureInfo;
		
		private float 		width;
		
		public Background (Scene scene)
		{
			backgroundSprites = new SpriteUV[2];
			
			textureInfo = new TextureInfo("/Application/textures/background.png");
			
			backgroundSprites[0] = new SpriteUV(textureInfo);
			backgroundSprites[0].Quad.S = textureInfo.TextureSizef;
			
			backgroundSprites[1] = new SpriteUV(textureInfo);
			backgroundSprites[1].Quad.S = textureInfo.TextureSizef;
			
			Bounds2 bounds = backgroundSprites[0].Quad.Bounds2();
			width = bounds.Point10.X;
			
			backgroundSprites[0].Position = new Vector2(0.0f, 0.0f);
			backgroundSprites[1].Position = new Vector2(backgroundSprites[0].Position.X+width, 0.0f);
			
			foreach(SpriteUV sprite in backgroundSprites)
			{
				scene.AddChild(sprite);
			}
		}
		
		public void Dispose()
		{
			textureInfo.Dispose();
		}
		
		public void Update(float deltaTime)
		{
			backgroundSprites[0].Position = new Vector2(backgroundSprites[0].Position.X - 0.5f, backgroundSprites[0].Position.Y);
			backgroundSprites[1].Position = new Vector2(backgroundSprites[1].Position.X - 0.5f, backgroundSprites[1].Position.Y);
			
			//Move the background
			//Left
			if(backgroundSprites[0].Position.X < -width)
			{
				backgroundSprites[0].Position = new Vector2(backgroundSprites[1].Position.X+width, 0.0f);
			}
			else
			{
				backgroundSprites[0].Position = new Vector2(backgroundSprites[0].Position.X-1, 0.0f);	
			}
			
			//Right
			if(backgroundSprites[1].Position.X < -width)
			{
				backgroundSprites[1].Position = new Vector2(backgroundSprites[0].Position.X+width, 0.0f);
			}
			else
			{
				backgroundSprites[1].Position = new Vector2(backgroundSprites[1].Position.X-1, 0.0f);
			}
		}
	}
}