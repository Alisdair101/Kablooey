using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Kablooey
{
	public class Ship
	{
		protected SpriteUV 		sprite;
		protected TextureInfo	textureInfo;
		protected float			yPositionBeforePush;
		protected float 		health;
		protected float 		speed;
		protected bool			alive;
		
		public Ship (Scene scene)
		{
			
		}
		
		public void Dispose()
		{
			textureInfo.Dispose();
		}
		
		public void Update(float deltaTime)
		{	
			sprite.Position = new Vector2(sprite.Position.X - speed, sprite.Position.Y);
		}	
		
		public void Tapped()
		{
			//Bounds2 box = sprite.GetContentLocalBounds();
			//sprite.GetContentWorldBounds(ref box);
		}
		
		public SpriteUV Sprite
		{
			get{return sprite;}
		}
	}
}