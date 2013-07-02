package objects
{
	import starling.core.Starling;
	import starling.display.MovieClip;
	import starling.display.Sprite;
	import starling.events.Event;
	
	public class HeartPowerUp extends Sprite
	{
		public var heartAnim:MovieClip;
		
		private var _timeOnScreen:Number
		private var _xDirection:Number; // the heart will float left/right
		private var _yDirection:Number;	// the heart will float up/down
		private var _alreadyHit:Boolean;
		
		public function HeartPowerUp()
		{
			super();
			this.addEventListener(Event.ADDED_TO_STAGE, onAdded);
		}
		
		public function get alreadyHit():Boolean
		{
			return _alreadyHit;
		}

		public function set alreadyHit(value:Boolean):void
		{
			_alreadyHit = value;
		}

		public function get yDirection():Number
		{
			return _yDirection;
		}

		public function set yDirection(value:Number):void
		{
			_yDirection = value;
		}

		public function get xDirection():Number
		{
			return _xDirection;
		}

		public function set xDirection(value:Number):void
		{
			_xDirection = value;
		}

		public function get timeOnScreen():Number
		{
			return _timeOnScreen;
		}

		public function set timeOnScreen(value:Number):void
		{
			_timeOnScreen = value;
		}

		private function onAdded():void
		{
			this.removeEventListener(Event.ADDED_TO_STAGE, onAdded);
			
			heartAnim = new MovieClip(Assets.getAtlas().getTextures("Hearts_01_64x64_"), 25);
			heartAnim.x = Math.ceil(heartAnim.width/2);		//stop from having blurry edges
			heartAnim.y = Math.ceil(heartAnim.height/2);	//stop from having blurry edges
			starling.core.Starling.juggler.add(heartAnim); // plays the animation for us.
			heartAnim.loop = false;
			this.addChild(heartAnim);
			
			this._timeOnScreen = 90;
			this._xDirection = -1;
			this._yDirection = 1;
		}
	}
}