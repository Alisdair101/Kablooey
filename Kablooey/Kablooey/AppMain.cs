using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.UI;

namespace Kablooey
{
	public class AppMain
	{
		private static Sce.PlayStation.HighLevel.GameEngine2D.Scene 	gameScene;
		private int testVar = 1;
		
		private static Background background;
		private static Fortress fortress;
		private static Gun gun;
		private static Ship[] teleportShips;
		private static Ship[] quikkShips;
		private static Ship[] slowShips;
		
		private static Timer  timer;
		private static int    timeSeed;
		
		public static void Main(string[] args)
		{
			Initialize ();
			
			timer = new Timer();
			timeSeed = 0;
			
			//Game loop
			bool quitGame = false;
			while (!quitGame)
			{
				Update ();
				
				Director.Instance.Update();
				Director.Instance.Render();
				
				Director.Instance.GL.Context.SwapBuffers();
				Director.Instance.PostSwap();
			}
			
			Cleanup ();
		}

		public static void Initialize()
		{
			//Set up director
			Director.Initialize();
			
			//Set game scene
			gameScene = new Sce.PlayStation.HighLevel.GameEngine2D.Scene();
			gameScene.Camera.SetViewFromViewport();
			
			//Background
			background = new Background(gameScene);
			
			//Fortress
			fortress = new Fortress(gameScene);
			
			//Gun
			gun = new Gun(gameScene);
			
			//Ships
			teleportShips = new TeleportShip[5];
			quikkShips    = new QuikkShip[5];
			slowShips     = new SlowShip[5];
			
			timer = new Timer();
			
			for(int i = 0; i <= 4; i++)
			{
				timeSeed = (int)timer.Milliseconds();
				teleportShips[i] = new TeleportShip(gameScene, timeSeed+i);
				
				timeSeed = (int)timer.Milliseconds();
				quikkShips[i]    = new QuikkShip(gameScene, timeSeed);
				
				timeSeed = (int)timer.Milliseconds();
				slowShips[i]     = new SlowShip(gameScene, timeSeed);
			}
			
			//Run the scene.
			Director.Instance.RunWithScene(gameScene, true);
		}
		
		public static void Cleanup()
		{
			background.Dispose();
			Director.Terminate();
		}

		public static void Update ()
		{
			//Background Update
			background.Update(0.0f);
			
			timer = new Timer();
			
			//Ship Updates
			for(int i = 0; i <= 4; i++)
			{
				teleportShips[i].Update(0.0f);
				quikkShips[i].Update(0.0f);
				slowShips[i].Update(0.0f);
				
				if(teleportShips[i].getAlive() == false)
				{
					timeSeed = (int)timer.Milliseconds(); 
					teleportShips[i].Respawn(timeSeed);
				}
				
				if(quikkShips[i].getAlive() == false)
				{
					timeSeed = (int)timer.Milliseconds(); 
					quikkShips[i].Respawn(timeSeed);
				}
				
				if(slowShips[i].getAlive() == false)
				{
					timeSeed = (int)timer.Milliseconds(); 
					slowShips[i].Respawn(timeSeed);
				}
			}
		}
	}
}
