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
		private static Ship teleportShip;
		private static Ship quikkShip;
		private static Ship slowShip;
		
		public static void Main(string[] args)
		{
			Initialize ();
			
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
			teleportShip = new TeleportShip(gameScene);
			quikkShip = new QuikkShip(gameScene);
			slowShip = new SlowShip(gameScene);
			
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
			background.Update(0.0f);
		}
	}
}
