using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Kablooey
{
	public class Ship
	{
		private static SpriteUV 	sprite;
		private static TextureInfo	textureInfo;
		private static float		yPositionBeforePush;
		
		private static int			counter;
		
		private static bool			alive;
		
		public Ship (Scene scene)
		{
			textureInfo  = new TextureInfo("/Application/textures/bird.png");
			
			sprite	 		= new SpriteUV();
			sprite 			= new SpriteUV(textureInfo);	
			sprite.Quad.S 	= textureInfo.TextureSizef;
			sprite.Position = new Vector2(Director.Instance.GL.Context.GetViewport().Width,Director.Instance.GL.Context.GetViewport().Height*0.5f);
			//sprite.Pivot 	= new Vector2(0.5f,0.5f);
			alive = true;
			
			//Add to the current scene.
			scene.AddChild(sprite);
		}
		
		public void Dispose()
		{
			textureInfo.Dispose();
		}
		
		public void Update(float deltaTime)
		{	
			sprite.Position = new Vector2(sprite.Position.X -10f, sprite.Position.Y);
		}	
		
		public void Tapped()
		{
			Bounds2 box = sprite.GetContentLocalBounds();
			sprite.GetContentWorldBounds(ref box);
			
		}
		
		public SpriteUV Sprite
		{
			get{
				return sprite;
			}
		}
	}
}

