package
{

	
	import com.freshplanet1.AirChartboost;
	
	import flash.display.Sprite;
	import flash.display.StageAlign;
	import flash.display.StageScaleMode;
	import flash.geom.Rectangle;
	import flash.system.Capabilities;
	
	import starling.core.Starling;
	
	[SWF(frameRate="30", backgroundColor="#ffffff")]
	public class Platformer extends Sprite
	{
		private var _starling:Starling;
		
		public function Platformer()
		{
			super();
			
			// support autoOrients
			stage.align = StageAlign.TOP_LEFT;
			stage.scaleMode = StageScaleMode.NO_SCALE;
			
			//setup chartboost
			if(Capabilities.manufacturer.indexOf('iOS') > -1)
			{
				var cb:AirChartboost = AirChartboost.getInstance();
				
				if(cb.isChartboostSupported)
				{
					cb.startSession('511511be17ba47062300006d', '6fa2e64061a1da5a40ed4c1a6f09d35d79bc740f');
					cb.showInterstitial();
				}
			}
			
			//setup viewport for starling
			var screenWidth:int  = stage.fullScreenWidth;
			var screenHeight:int = stage.fullScreenHeight;
			//var viewPort:Rectangle = new Rectangle(0, 0, screenWidth, screenHeight)
			var viewPort:Rectangle = new Rectangle(0, 0, screenWidth, screenHeight)
			
			_starling = new Starling(Game, stage, viewPort);
			_starling.stage.stageWidth  = 960;	// testing on ipad, so set for iPad
			_starling.stage.stageHeight = 640;
			_starling.antiAliasing = 1;
			_starling.start();
			
			_starling.showStats = true;
		}
	}
}