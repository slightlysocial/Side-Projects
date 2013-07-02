package objects
{
	import starling.display.Image;
	import starling.display.MovieClip;
	import starling.display.Sprite;
	import starling.events.Event;
	
	public class Obstacle extends Sprite
	{
		private var _type:int;
		private var _speed:int;
		private var _distance:int;
		private var _alreadyHit:Boolean;
		private var _position:String;
		private var obstactleImage:Image;
		private var obstacleCrashImage:Image;
		private var obstactleAnimation:MovieClip;
		
		public function Obstacle(_type:int, _distance:int, _speed:int = 0)
		{
			super();
			
			this._type 		= _type;
			this._distance 	= _distance;
			this._speed 	= _speed;
			
			_alreadyHit = false;			
			
			this.addEventListener(Event.ADDED_TO_STAGE, onAdded);
		}

		private function onAdded(e:Event):void
		{
			this.removeEventListener(Event.ADDED_TO_STAGE, onAdded);
			
			createObstacleArt();
			createObstacleCrashArt();
		}
		
		private function createObstacleCrashArt():void
		{
			//obstacleCrashImage = new Image();
			//obstacleCrashImage.visible = false;
			//this.addChild(obstacleCrashImage);
		}
		
		private function createObstacleArt():void
		{			
			obstactleImage = new Image(Assets.getTexture("Asteroid"+_type));
			obstactleImage.x = 0;
			obstactleImage.y = 0;
			this.addChild(obstactleImage);
		}
		
		public function get postition():String
		{
			return _position;
		}
		
		public function set postition(value:String):void
		{
			_position = value;
		}
		
		public function get speed():int
		{
			return _speed;
		}
		
		public function set speed(value:int):void
		{
			_speed = value;
		}
		
		public function get distance():int
		{
			return _distance;
		}
		
		public function set distance(value:int):void
		{
			_distance = value;
		}		
		
		public function set alreadyHit(value:Boolean):void
		{
			_alreadyHit = value;
			
			if(value)
			{
				//obstacleCrashImage.visible = true;
				//if(_type == 4) obstactleAnimation.visible = false
				//else obstactleImage.visible = false;
			}
		}
		
		public function get alreadyHit():Boolean
		{
			return _alreadyHit;
		}
	}
}