using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Kablooey
{
	public class Gun
	{
		private SpriteTile  gun;
		private SpriteUV    reticule;
		
		private TextureInfo gunTextureInfo;
		private TextureInfo retTextureInfo;
		
		private Vector2 retAimPos;
		private float retSpeed;
		
		private GamePadData gamePadData;
		
		private int tileIndex = 1;
		
		public Gun (Scene scene)
		{
			gunTextureInfo = new TextureInfo(new Texture2D("/Application/textures/gun.png", false), new Vector2i(1, 2));
			gun = new SpriteTile(gunTextureInfo);
			gun.Quad.S = gunTextureInfo.TileSizeInPixelsf;
			gun.Position = new Vector2(115.0f, 285.0f);
			gun.TileIndex2D = new Vector2i(tileIndex, 0);
			
			retTextureInfo = new TextureInfo("/Application/textures/reticule.png");
			reticule = new SpriteUV(retTextureInfo);
			reticule.Quad.S = retTextureInfo.TextureSizef;
			reticule.Position = new Vector2(200.0f, 300.0f);
			
			retAimPos = new Vector2(reticule.Position.X + 23.0f, reticule.Position.Y + 23.0f);
			retSpeed = 10.0f;
			
			scene.AddChild(gun);
			scene.AddChild(reticule);
		}
		
		public void Dispose()
		{
			gunTextureInfo.Dispose();
			retTextureInfo.Dispose();
		}
		
		public void Update(float deltaTime)
		{
			//Gamepad
			gamePadData = GamePad.GetData(0);
			Controls ();
			
			retAimPos = new Vector2(reticule.Position.X + 23.0f, reticule.Position.Y + 23.0f);
			
			if(retAimPos.X >= 960)
			{
				reticule.Position = new Vector2(960 - 23, reticule.Position.Y);
			}
			if(retAimPos.X <= 115)
			{
				reticule.Position = new Vector2(115 - 23, reticule.Position.Y);
			}
			if(retAimPos.Y >= 544)
			{
				reticule.Position = new Vector2(reticule.Position.X, 544 - 23);
			}
			if(retAimPos.Y <= 0)
			{
				reticule.Position = new Vector2(reticule.Position.X, 0 - 23);
			}
		}
		
		public void Controls()
		{
			if((gamePadData.Buttons & GamePadButtons.Left) != 0)
		    {
		        setRetPosition(-1.0f, 0.0f);
		    }
		    if((gamePadData.Buttons & GamePadButtons.Right) != 0)
		    {
		        setRetPosition(1.0f, 0.0f);
		    }
		    if((gamePadData.Buttons & GamePadButtons.Up) != 0)
		    {
		        setRetPosition(0.0f, 1.0f);
		    }
		    if((gamePadData.Buttons & GamePadButtons.Down) != 0)
		    {
		        setRetPosition(0.0f, -1.0f);
		    }
		}
		
		public void setRetPosition(float X, float Y)
		{
			reticule.Position = new Vector2(reticule.Position.X + (X*retSpeed), reticule.Position.Y + (Y*retSpeed));
		}
		
		public Vector2 getRetAimPosition()
		{
			return retAimPos;
		}
	}
}
