using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Kablooey
{
	public class Fortress
	{
		private SpriteTile  sprite;
		private TextureInfo textureInfo;
		private Bounds2 	bounds;
		private int 		tileIndex = 0;
		
		private int health;
		
		public Fortress (Scene scene)
		{
			textureInfo = new TextureInfo(new Texture2D("/Application/textures/fortress.png", false), new Vector2i(2, 1));
			
			sprite = new SpriteTile(textureInfo);
			sprite.Quad.S = textureInfo.TileSizeInPixelsf;
			sprite.Position = new Vector2(0.0f, 0.0f);
			
			sprite.TileIndex2D = new Vector2i(tileIndex, 0);
			
			health = 10;
			
			scene.AddChild(sprite);
		}
		
		public void Dispose()
		{
			textureInfo.Dispose();
		}
		
		public void Update(float deltaTime)
		{
			if(health <= 5)
			{
				tileIndex = 1;
				sprite.TileIndex2D = new Vector2i(tileIndex, 0);
			}
		}
		
		public SpriteTile getSprite()
		{
			return sprite;
		}
		
		public Bounds2 getBounds()
		{
			bounds = sprite.GetlContentLocalBounds();
			sprite.GetContentWorldBounds(ref bounds);
			return bounds;
		}
		
		public int getHealth()
		{
			return health;	
		}
		
		public void hit(int hit)
		{
			health -= hit;
		}
	}
}