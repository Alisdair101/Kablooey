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
		private static Sce.PlayStation.HighLevel.UI.Scene 				uiScene;
		private static Sce.PlayStation.HighLevel.UI.Label				scoreLabel;
		
		private static GamePadData gamePadData;
		
		private static Background background;
		private static Fortress fortress;
		private static Gun gun;
		private static Title title;
		private static GameOver gameover;
		
		private static Ship[] teleportShips;
		private static Ship[] quikkShips;
		private static Ship[] slowShips;
		
		private static int teleportShipCount;
		private static int quikkShipCount;
		private static int slowShipCount;
		
		private static bool teleportShipAdded;
		private static bool quikkShipAdded;
		private static bool slowShipAdded;
		private static bool menuon = true;
		private static bool game = false;
		private static bool end = false;
	
		private static Bullet[] bullets;
		private static bool fireButtonDown;
		
		private static Timer  timer;
		private static int    timeSeed;
		
		private static bool quitGame;
		
		private static int score;
		
		public static void Main(string[] args)
		{
			Initialize ();
			
			//Game loop
			quitGame = false;
			while (!quitGame)
			{
				Update (timer);
				
				//Update Director Instance and UI
				Director.Instance.Update();
				
				//Render Director Instance and UI
				Director.Instance.Render();
				UISystem.Render();
				
				Director.Instance.GL.Context.SwapBuffers();
				Director.Instance.PostSwap();
			}
			
			Cleanup ();
		}

		public static void Initialize()
		{
			//Set up director
			Director.Initialize();
			UISystem.Initialize(Director.Instance.GL.Context);
			
			//Set game scene
			gameScene = new Sce.PlayStation.HighLevel.GameEngine2D.Scene();
			gameScene.Camera.SetViewFromViewport();
			
			//Set the ui scene.
			uiScene = new Sce.PlayStation.HighLevel.UI.Scene();
			score = 0;
			
			//Create the Panel
			Panel panel  = new Panel();
			panel.Width  = Director.Instance.GL.Context.GetViewport().Width;
			panel.Height = Director.Instance.GL.Context.GetViewport().Height;
			
			//Create the Score Label
			scoreLabel = new Sce.PlayStation.HighLevel.UI.Label();
			scoreLabel.HorizontalAlignment = HorizontalAlignment.Center;
			scoreLabel.VerticalAlignment = VerticalAlignment.Top;
			scoreLabel.SetPosition(
				Director.Instance.GL.Context.GetViewport().Width/2 - scoreLabel.Width/2,
				Director.Instance.GL.Context.GetViewport().Height*0.1f - scoreLabel.Height/2);
			scoreLabel.TextColor = new UIColor(0.0f, 0.0f, 0.0f, 1);
			scoreLabel.Text = "Score: " + score;
			
			//Add the UI to the scene
			panel.AddChildLast(scoreLabel);
			uiScene.RootWidget.AddChildLast(panel);
			UISystem.SetScene(uiScene);
			
			//Background
			background = new Background(gameScene);
			
			//Fortress
			fortress = new Fortress(gameScene);
			
			//Bullets
			bullets = new Bullet[20];
			
			for(int i = 0; i <= 19; i++)
			{
				bullets[i] = new Bullet(gameScene);	
			}
			
			fireButtonDown = false;
				
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
			
			
			//Create menu
			 	
			if (menuon == true )
			{
			title = new Title(gameScene);
			
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
			
			title.Dispose();
			gameover.Dispose();
			
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
			
			//run menu
			var touches = Touch.GetData(0);
			var touch = Touch.GetData(0);
			
			if (menuon == true)
			{
				if (touches.Count > 0 )
				{
					menuon = false ;
					game = true;
					
				}
				
			}	
			
			if (game == true)
			{
				title.Update(0.0f);		
			
				//Background Update
				background.Update(0.0f);
				
				//Fortress Update
				fortress.Update(0.0f);
				
				//Gun Update
				gun.Update(0.0f);
				
				//Bullets Update
				UpdateBullets();
				
				if(fortress.getHealth() <= 0)
				{
					//quitGame = true;
					end = true;
					game = false;
					gameover = new GameOver(gameScene);
					
				}
				//Ship Updates and Respawns
				UpdateShips();
				
				//Update Bullet Position and Fire Check
				UpdateBullets();
				
				//Collision Checks and Updates
				UpdateCollisions();
			
			}
			
			//On game end check for tap to end game
			if (end == true)
			{
				if (touch.Count > 0 )
				{
					end = false;
					quitGame = true;
							
				}
						
			}
				
			
		}
		
		public static void UpdateShips()
		{
			if(quikkShipCount < 4 && timer.Seconds() >= 10 && !quikkShipAdded)
			{
				quikkShipCount += 1;
				quikkShipAdded = true;
			}
			else if(slowShipCount < 4 && timer.Seconds() >= 15 && !slowShipAdded)
			{
				slowShipCount += 1;
				slowShipAdded = true;
			}
			else if(teleportShipCount < 4 && timer.Seconds() >= 20 && !teleportShipAdded)
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
					//Update score
					score += 1;	
					scoreLabel.Text = "Score: " + score;
				}
			}
			
			for(int i = 0; i <= quikkShipCount; i++)
			{
				quikkShips[i].Update(0.0f);
				
				if(quikkShips[i].getAlive() == false)
				{
					timeSeed = (int)timer.Milliseconds(); 
					quikkShips[i].Respawn(timeSeed);
					//Update score
					score += 1;	
					scoreLabel.Text = "Score: " + score;
				}
			}
				
			for(int i = 0; i <= slowShipCount; i++)
			{
				slowShips[i].Update(0.0f);
				
				if(slowShips[i].getAlive() == false)
				{
					timeSeed = (int)timer.Milliseconds(); 
					slowShips[i].Respawn(timeSeed);
					//Update score
					score += 1;	
					scoreLabel.Text = "Score: " + score;
				}
			}
		}
		
		public static void UpdateBullets()
		{
			gamePadData = GamePad.GetData(0);
			
			if((gamePadData.Buttons & GamePadButtons.R) != 0)
			{
				for(int i = 0; i <= 19; i++)
				{
					if(!fireButtonDown && bullets[i].getFired() == false)
					{
						bullets[i].setFired(true);
						
						Vector2 retAimPos = gun.getRetAimPosition();
						bullets[i].setTrajectory(retAimPos);
						
						fireButtonDown = true;
					}
				}
			}
			
			if((gamePadData.Buttons & GamePadButtons.R) == 0)
			{
				fireButtonDown = false;
			}
			
			for(int i = 0; i <= 19; i++)
			{
				if(bullets[i].getFired() == true)
				{
					bullets[i].Update(0.0f);
				}
			}
		}
		
		public static void UpdateCollisions()
		{
			//Update Ship Collisions
			UpdateShipCollisions();
			
			//Update Bullet Collisions
			UpdateBulletCollisions();
		}
		
		public static void UpdateShipCollisions()
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
		
		public static void UpdateBulletCollisions()
		{
			for(int i = 0; i <= 19; i++)
			{
				if(bullets[i].getFired() == true)
				{
					for(int ii = 0; ii <= teleportShipCount; ii++)
					{
						bool collision = Collision(bullets[i].getBounds(), teleportShips[ii].getBounds());
						
						if(collision)
						{
							teleportShips[ii].hit();
							bullets[i].resetBullet();
						}
					}
				}
					
				if(bullets[i].getFired() == true)
				{
					for(int ii = 0; ii <= quikkShipCount; ii++)
					{
						bool collision = Collision(bullets[i].getBounds(), quikkShips[ii].getBounds());
						
						if(collision)
						{
							quikkShips[ii].hit();
							bullets[i].resetBullet();
						}
					}
				}
					
				if(bullets[i].getFired() == true)
				{
					for(int ii = 0; ii <= slowShipCount; ii++)
					{
						bool collision = Collision(bullets[i].getBounds(), slowShips[ii].getBounds());
						
						if(collision)
						{
							slowShips[ii].hit();
							bullets[i].resetBullet();
						}
					}
				}
			}
		}
	}
}