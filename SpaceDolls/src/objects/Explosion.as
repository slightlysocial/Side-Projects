package objects
{
	import starling.core.Starling;
	import starling.display.MovieClip;
	import starling.display.Sprite;
	import starling.events.Event;
	
	public class Explosion extends Sprite
	{
		public var explosion:MovieClip;
		
		public function Explosion()
		{
			super();
			this.addEventListener(Event.ADDED_TO_STAGE, onAdded);
		}
		
		private function onAdded():void
		{
			this.removeEventListener(Event.ADDED_TO_STAGE, onAdded);
			
			explosion = new MovieClip(Assets.getAtlas().getTextures("expl_"), 25);
			explosion.x = Math.ceil(explosion.width/2);		//stop from having blurry edges
			explosion.y = Math.ceil(explosion.height/2);	//stop from having blurry edges
			starling.core.Starling.juggler.add(explosion); // plays the animation for us.
			explosion.loop = false;
			this.addChild(explosion);			
		}
	}
}