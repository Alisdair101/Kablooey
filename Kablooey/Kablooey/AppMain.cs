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
		private static int testVar = 1;
		
		private static Background background;
		private static Fortress fortress;
		private static Gun gun;
		
		private static Ship[] teleportShips;
		private static Ship[] quikkShips;
		private static Ship[] slowShips;
		
		private static int teleportShipCount;
		private static int quikkShipCount;
		private static int slowShipCount;
		
		private static bool teleportShipAdded;
		private static bool quikkShipAdded;
		private static bool slowShipAdded;
		
		private static Timer  timer;
		private static int    timeSeed;
		
		private static bool quitGame;
		
		public static void Main(string[] args)
		{
			Initialize ();
			
			//Game loop
			quitGame = false;
			while (!quitGame)
			{
				Update (timer);
				
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
			
			teleportShipCount = 0;
			quikkShipCount = 0;
			slowShipCount = 0;
			
			teleportShipAdded = false;
			quikkShipAdded = false;
			slowShipAdded = false;
			
			timeSeed = 0;
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
			fortress.Dispose();
			
			foreach(TeleportShip teleportShip in teleportShips)
			{
				teleportShip.Dispose();
			}
			
			foreach(QuikkShip quikkShip in quikkShips)
			{
				quikkShip.Dispose();
			}
			
			foreach(SlowShip slowShip in slowShips)
			{
				slowShip.Dispose();
			}
			
			Director.Terminate();
		}
		
		public static bool Collision(Bounds2 sprite1Bounds, Bounds2 sprite2Bounds)
		{
			if(sprite1Bounds.Overlaps(sprite2Bounds))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static void Update (Timer timer)
		{
			//Background Update
			background.Update(0.0f);
			
			//Fortress Update
			fortress.Update (0.0f);
			
			if(fortress.getHealth() <= 0)
			{
				quitGame = true;
			}
			
			//Ship Updates and Respawns
			UpdateShips();
			
			//Collision Checks and Updates
			UpdateCollisions();
		}
		
		public static void UpdateShips()
		{
			if(timer.Seconds() >= 10 && !quikkShipAdded)
			{
				quikkShipCount += 1;
				quikkShipAdded = true;
			}
			else if(timer.Seconds() >= 15 && !slowShipAdded)
			{
				slowShipCount += 1;
				slowShipAdded = true;
			}
			else if(timer.Seconds() >= 20 && !teleportShipAdded)
			{
				teleportShipCount += 1;
				
				//Not used, but added in case order of ships added changed
				teleportShipAdded = true;
				teleportShipAdded = false;
				
				quikkShipAdded = false;
				slowShipAdded = false;
				
				timer.Reset();
			}
			
			//Ship Updates
			for(int i = 0; i <= teleportShipCount; i++)
			{
				teleportShips[i].Update(0.0f);
				
				if(teleportShips[i].getAlive() == false)
				{
					timeSeed = (int)timer.Milliseconds();
					teleportShips[i].Respawn(timeSeed);
				}
			}
			
			for(int i = 0; i <= quikkShipCount; i++)
			{
				quikkShips[i].Update(0.0f);
				
				if(quikkShips[i].getAlive() == false)
				{
					timeSeed = (int)timer.Milliseconds(); 
					quikkShips[i].Respawn(timeSeed);
				}
			}
				
			for(int i = 0; i <= slowShipCount; i++)
			{
				slowShips[i].Update(0.0f);
				
				if(slowShips[i].getAlive() == false)
				{
					timeSeed = (int)timer.Milliseconds(); 
					slowShips[i].Respawn(timeSeed);
				}
			}
		}
		
		public static void UpdateCollisions()
		{
			//Collision Detection
			for(int i = 0; i <= teleportShipCount; i++)
			{
				//Teleport Ship Collision Detection
				//Call Collision, pass in fortress bounds and teleportShip bounds
				bool collision = Collision (fortress.getBounds(), teleportShips[i].getBounds());
				
				//Teleport Ship Collision Outcome
				if(collision)
				{
					//Teleport ship alive property set to false
					teleportShips[i].setAlive(false);
					//Fortress health - 1
					fortress.hit(1);
				}
			}
				
			for(int i = 0; i <= quikkShipCount; i++)
			{
				//Quikk Ship Collision Detection
				//Call Collision, pass in fortress bounds and quikkShip bounds
				bool collision = Collision (fortress.getBounds(), quikkShips[i].getBounds());
				
				//Quikk Ship Collision Outcome
				if(collision)
				{
					//Quikk ship alive property set to false
					quikkShips[i].setAlive(false);
					//Fortress health - 1
					fortress.hit(1);
				}
			}
			
			for(int i = 0; i <= slowShipCount; i++)
			{
				//Slow Ship Collision Detection
				//Call Collision, pass in fortress bounds and slowShip bounds
				bool collision = Collision (fortress.getBounds(), slowShips[i].getBounds());
			
				//Slow Ship Collision Outcome
				if(collision)
				{
					//Slow ship alive property set to false
					slowShips[i].setAlive(false);
					//Fortress health - 1
					fortress.hit(1);
				}
			}
		}
	}
}
