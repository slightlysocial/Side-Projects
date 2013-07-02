package
{
	import events.NavigationEvent;
	
	import objects.CyborgController;
	
	import screens.InGame;
	import screens.WelcomeScreen;
	
	import starling.display.Sprite;
	import starling.events.Event;
	import starling.text.TextField;
	import screens.EndRoundScreen;
	
	public class Game extends Sprite
	{
		private var cyborg:CyborgController;
		
		private var screenWelcome:WelcomeScreen;
		private var screenInGame:InGame;
		private var screenEndRound:EndRoundScreen;
		
		public function Game()
		{
			super();
			this.addEventListener(Event.ADDED_TO_STAGE, onAdded);
		}
		
		private function onAdded():void
		{
			this.removeEventListener(Event.ADDED_TO_STAGE, onAdded);
			
			this.addEventListener(NavigationEvent.CHANGE_SCREEN, onChangeScreen);
			
			screenInGame = new InGame();
			screenInGame.deactivate();
			addChild(screenInGame);
			
			screenWelcome = new WelcomeScreen();
			addChild(screenWelcome);
			screenWelcome.initialize();
		}
		
		private function onChangeScreen(e:NavigationEvent):void
		{
			switch(e.params.id)
			{
				case "play":
					screenWelcome.deactivate();
					screenInGame.initialize();
					break;				
				case "end_round":
					
					break;
			}
			
		}
	}
}