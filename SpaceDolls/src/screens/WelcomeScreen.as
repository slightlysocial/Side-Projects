package screens
{	
	import com.greensock.TweenLite;
	
	import events.NavigationEvent;
	
	import flash.display3D.textures.CubeTexture;
	
	import objects.CyborgController;
	
	import starling.display.Button;
	import starling.display.Image;
	import starling.display.Sprite;
	import starling.events.Event;
	
	
	public class WelcomeScreen extends Sprite
	{
		private var bg:Image;
		private var girlBig:Image;
		private var playButton:Button;
		private var cyborg:CyborgController;
		
		public function WelcomeScreen()
		{
			super();
			this.addEventListener(Event.ADDED_TO_STAGE, onAdded);
		}
		
		private function onAdded():void
		{
			this.removeEventListener(Event.ADDED_TO_STAGE, onAdded);
			drawScreen();
		}
		
		private function drawScreen():void
		{
			bg = new Image(Assets.getTexture("BgSpace"));
			addChild(bg);
			
			girlBig = new Image(Assets.getTexture("girlWelcome"));			
			addChild(girlBig);
			
			cyborg = new CyborgController();
			addChild(cyborg);
			//cyborg.x = 200;
			cyborg.y = 400;
			
			playButton = new Button(Assets.getTexture("WelcomePlayBtn"));
			addChild(playButton);
			playButton.x = 300;
			playButton.y = 150;
			
			addEventListener(Event.TRIGGERED, onMainMenuClick);
			
		}
		
		private function onMainMenuClick(e:Event):void
		{
			if((e.target as Button) == playButton)
			{
				this.dispatchEvent(new NavigationEvent(NavigationEvent.CHANGE_SCREEN, {id:"play"}, true));
			}
		}
		
		public function initialize():void
		{
			this.visible = true;
			
			addEventListener(Event.ENTER_FRAME, onFrameUpdate);
			
			cyborg.x = -600;
			TweenLite.to(cyborg, 2, {x:200, onComplete:cyborg.stand});
			
			girlBig.x = 1600;
			TweenLite.to(girlBig, 1, {x:550});
		}
		
		private function onFrameUpdate():void
		{
			var curDate:Date = new Date();
			girlBig.y = 40 + (Math.cos(curDate.getTime() * 0.002) * 10);
			playButton.y = 150 + (Math.cos(curDate.getTime() * 0.002) * 20);
		}
		
		public function deactivate():void
		{
			this.visible = false;
		}
		
	}
}