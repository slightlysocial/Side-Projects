package screens
{
	import starling.display.Sprite;
	import starling.events.Event;
	import starling.text.TextField;
	
	public class EndRoundScreen extends Sprite
	{
		public function EndRoundScreen()
		{
			super();
			this.addEventListener(Event.ADDED_TO_STAGE, onAdded);
		}
		
		private function onAdded(e:Event):void
		{
			this.removeEventListener(Event.ADDED_TO_STAGE, onAdded);
			drawScreen();
		}
		
		private function drawScreen():void
		{
			var finished:TextField = new TextField(200,100,"FINISHED!");
			this.addChild(finished);
			finished.x = stage.stageWidth/2 - finished.width/2;
			finished.y = stage.stageHeight/2 - 50;
		}
	}
}