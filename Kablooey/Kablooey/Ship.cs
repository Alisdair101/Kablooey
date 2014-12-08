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
		protected float 		health;
		protected float 		speed;
		protected bool			alive;
		protected Vector2		startSpawn;
		protected Bounds2 		bounds;
		
		public Ship (Scene scene)
		{
			
		}
	
		public void Respawn(int timeSeed)
		{
			Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.RandGenerator rand = new Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.RandGenerator(timeSeed);
			float randomY = rand.NextFloat(0, 470);
			sprite.Position = new Vector2(960, randomY);
			
			alive = true;
		}
		
		public SpriteUV getSprite()
		{
			return sprite;
		}
		
		public bool getAlive()
		{
			return alive;	
		}
		
		public void setAlive(bool alive)
		{
			this.alive = alive;	
		}
		
		public void Update(float deltaTime)
		{
			sprite.Position = new Vector2(sprite.Position.X - speed, sprite.Position.Y);
			
			if(sprite.Position.X < -171)
			{
				alive = false;
			}
		}
		
		public SpriteUV Sprite()
		{
			return sprite;
		}
		
		public Bounds2 getBounds()
		{
			bounds = sprite.GetlContentLocalBounds();
			sprite.GetContentWorldBounds(ref bounds);
			return bounds;
		}
	}
}