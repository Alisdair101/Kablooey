using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Kablooey
{
	public class Bullet
	{
		private SpriteUV 	sprite;
		private TextureInfo	textureInfo;
		private Bounds2 	bounds;
		
		private Vector2		trajectory;
		
		private bool fired;
		
		private float speed;
		
		public Bullet (Scene scene)
		{
			textureInfo = new TextureInfo("/Application/textures/bullet.png");
			sprite = new SpriteUV(textureInfo);
			sprite.Quad.S = textureInfo.TextureSizef;
			sprite.Position = new Vector2(170.0f, 290.0f);
			
			fired = false;
			
			speed = 10.0f;
			
			scene.AddChild(sprite);
		}
		
		public void Update(float deltaTime)
		{
			sprite.Position = new Vector2(sprite.Position.X + (trajectory.X*speed), sprite.Position.Y + (trajectory.Y*speed));
			
			if(sprite.Position.X >= 960)
			{
				resetBullet();
			}
			if(sprite.Position.X <= 0)
			{
				resetBullet();
			}
			if(sprite.Position.Y >= 544)
			{
				resetBullet();
			}
			if(sprite.Position.Y <= 0)
			{
				resetBullet();
			}
		}
		
		public Bounds2 getBounds()
		{
			bounds = sprite.GetlContentLocalBounds();
			sprite.GetContentWorldBounds(ref bounds);
			return bounds;
		}
		
		public void setTrajectory(Vector2 reticuleAimPos)
		{
			trajectory = new Vector2(reticuleAimPos.X - sprite.Position.X, reticuleAimPos.Y - sprite.Position.Y);
			
			float magnitude = (float)System.Math.Sqrt((float)System.Math.Pow(trajectory.X, 2.0) + (float)System.Math.Pow(trajectory.Y, 2.0f));
			
			trajectory = new Vector2(trajectory.X/magnitude, trajectory.Y/magnitude);
		}
		
		public bool getFired()
		{
			return fired;	
		}
		
		public void setFired(bool fired)
		{
			this.fired = fired;	
		}
		
		public void resetBullet()
		{
			fired = false;
			sprite.Position = new Vector2(170.0f, 290.0f);
		}
	}
}

