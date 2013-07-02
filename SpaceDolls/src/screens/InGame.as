package screens
{
	import flash.display.Sprite;
	import flash.geom.Rectangle;
	import flash.utils.getTimer;
	
	import objects.CyborgController;
	import objects.Explosion;
	import objects.GameBackground;
	import objects.HeartPowerUp;
	import objects.Obstacle;
	
	import starling.core.Starling;
	import starling.display.Button;
	import starling.display.Image;
	import starling.display.MovieClip;
	import starling.display.Sprite;
	import starling.events.Event;
	import starling.events.Touch;
	import starling.events.TouchEvent;
	import starling.text.TextField;
	import starling.utils.HAlign;
	import starling.utils.deg2rad;
	
	public class InGame extends Sprite
	{
		private var planet1:Image;	
		private var bg:GameBackground;
		private var cyborg:CyborgController;
		
		private const MIN_SPEED:Number = 650;
		
		private var playerSpeed:Number;
		private var timePrevious:Number;
		private var timeCurrent:Number;
		private var elapsed:Number;		
		private var heartCountdown:Number;
		private var playerHealth:int;
		
		private var gameState:String; //idle, start, over

		private var scoreDistance:int;
		private var obstacleGapCount:int;
		private var hitObstacle:Number;
		private var scoreLabel:TextField;
		
		private var gameArea:Rectangle;
		
		private var obstaclesToAnimate:Vector.<Obstacle>;
		private var explosionsOnStage:Vector.<Explosion>;
		private var heartsOnStage:Vector.<HeartPowerUp>;
		
		private var startButton:Button;
		
		private var touch:Touch;
		private var touchX:Number;
		private var touchY:Number;
		private var touchYOffset:Number;
		private var isJumping:Boolean = false;
		private var isFalling:Boolean = false;
		private var groundLevel:int;
		private var speedY:int = 0;
		
		public function InGame()
		{
			super();
			this.addEventListener(Event.ADDED_TO_STAGE, onAdded);
		}
		
		private function onAdded():void
		{
			this.removeEventListener(Event.ADDED_TO_STAGE, onAdded);			
			drawGame();
		}
		
		public function initialize():void
		{
			this.visible = true;
			
			obstacleGapCount = 0;
			scoreDistance = 0;
			playerSpeed = 0;
			bg.speed = 0;
			hitObstacle = 0;
			heartCountdown = 1000;
			playerHealth = 100;
			groundLevel = stage.stageHeight - 150;

			obstaclesToAnimate 	= new Vector.<Obstacle>;
			explosionsOnStage  	= new Vector.<Explosion>;
			heartsOnStage		= new Vector.<HeartPowerUp>;
			
			this.addEventListener(Event.ENTER_FRAME, checkElapsed);
			
			gameState = "idle";
		}
		
		private function checkElapsed():void
		{
			timePrevious = timeCurrent;
			timeCurrent = getTimer();
			elapsed = (timeCurrent - timePrevious) * 0.001;
		}
		
		private function drawGame():void
		{			
			bg = new GameBackground();
			addChild(bg);			
			
			var scoreTitle:TextField = new TextField(200,100,"Score:");
			scoreTitle.color = 0xFFFFFF;
			scoreTitle.fontSize = 22;
			scoreTitle.x = 60;
			scoreTitle.y = -10;
			this.addChild(scoreTitle);
			
			//Place a counting score label that gets updated
			scoreLabel = new TextField(200,100,"0");
			scoreLabel.color = 0xFFFFFF;
			scoreLabel.fontSize = 22;
			scoreLabel.hAlign = HAlign.LEFT;
			scoreLabel.x = 200;
			scoreLabel.y = -10;
			this.addChild(scoreLabel);			
			
			planet1 = new Image(Assets.getTexture("redPlanet"));
			planet1.scaleX = 0.5;
			planet1.scaleY = 0.5;
			addChild(planet1);
			planet1.x = 660;
			
			cyborg = new CyborgController();
			addChild(cyborg);
			cyborg.x = -600;
			cyborg.y = groundLevel;
			
			startButton = new Button(Assets.getTexture("WelcomePlayBtn"));
			startButton.x = 200;
			startButton.y = 200;
			addChild(startButton);
			startButton.addEventListener(Event.TRIGGERED, onStartButtonClicked);
			
			gameArea = new Rectangle(0, 0, stage.stageWidth, stage.stageHeight);
		}
		
		private function onStartButtonClicked(e:Event):void
		{
			startButton.visible = false;
			startButton.removeEventListener(Event.TRIGGERED, onStartButtonClicked);
			launchCyborg();
		}
		
		private function launchCyborg():void
		{
			this.addEventListener(Event.ENTER_FRAME, onFrameUpdate);
			this.addEventListener(TouchEvent.TOUCH, onTouch);
			cyborg.run();
		}
		
		private function onTouch(e:TouchEvent):void
		{
			touch = e.getTouch(stage);
			if(touch != null)
			{
				touchX = touch.globalX;
				touchY = touch.globalY;
				touchYOffset = touch.globalY - touch.previousGlobalY;
			}
		}
		
		private function onFrameUpdate(e:Event):void
		{
			switch(gameState)
			{
				case "idle":
				{
					if(cyborg.x < stage.stageWidth * 0.5 * 0.5)
					{
						cyborg.x += ((stage.stageWidth * 0.5 * 0.5 + 10) - cyborg.x) * 0.05;
						
						playerSpeed += (MIN_SPEED - playerSpeed) * 0.05;
						bg.speed = playerSpeed * elapsed;
						cyborg.y = groundLevel;
					}else{
						gameState = "running";
					}
					break;
				}
				case "running":
				{
					trace(speedY);
					
					//check if player is dead
					if(playerHealth <= 0)
					{
						endRound();
					}
					
					if(hitObstacle <= 0)
					{
						if(touchYOffset < -5 && !isJumping && !isFalling) // if NOT jumping already and swiped up
						{
							doJump();
						}
						else if(touchYOffset >= 5 && !isFalling) // if NOT jumping already and swiped down
						{
							if(isJumping && speedY < 0)
							{
								speedY *= -1; //reverse direction to fall if going up
								cyborg.fall();
							}
						}
						
						if(isJumping)
						{
							if(speedY <= 0 && speedY + 1 > 0)
							{
								cyborg.fall();
							}
							speedY +=1;
						}
						
						if(speedY != 0)
						{
							cyborg.y += speedY;
							if(cyborg.y >= groundLevel)
							{
								cyborg.y = groundLevel;
								isJumping = false;
								speedY = 0;
								touchYOffset = 0; // stop jumping after the fall from the touch number not being reset
								cyborg.run();
							}
						}
						scoreDistance += (playerSpeed * elapsed) * 0.01;
						initObstactle();
					}
					else
					{
						cameraShake();
						hitObstacle--;
					}
					
					playerSpeed -= ( playerSpeed - MIN_SPEED) * 0.01; // everytime we change playerSpeed, change the bg speed
					bg.speed = playerSpeed * elapsed;
					
					//check if we spawn heart pickups
					if(heartCountdown <= 0)
					{
						createHeartPowerUp();
						heartCountdown = 500 - Math.random() * 1000;
					}else{
						heartCountdown -=1;
					}
					
					manageExplosions();
					animateObstacles();
					animateHearts();
					
					break;
				}
				case "over":
				{
					
					break;
				}	
				
			}
			
			updateBehaviour();
			
			var curDate:Date = new Date();
			planet1.y = 40 + (Math.cos(curDate.getTime() * 0.002) * 10);
			planet1.x = 660 + (Math.cos(curDate.getTime() * 0.002) * 4);
			
			scoreLabel.text = scoreDistance.toString();
		}
		
		private function endRound():void
		{
			// TODO Auto Generated method stub
			this.removeEventListener(Event.ENTER_FRAME, onFrameUpdate);			
		}
		
		private function manageExplosions():void
		{
			if(explosionsOnStage.length > 0)
			{				
				for(var i:uint = 0; i < explosionsOnStage.length; i++)
				{
					var explosionToTrack:Explosion = explosionsOnStage[i];
					if(explosionToTrack.explosion.isComplete)
					{
						explosionsOnStage.splice(i,1);
						this.removeChild(explosionToTrack);
						explosionToTrack = null;
					}
				}
			}
		}
		
		private function doJump():void
		{
			if(isJumping)
			{
				return;
			}
			speedY = -30;
			isJumping = true;			
			cyborg.jump();
		}
		
		private function updateBehaviour():void
		{
			if(isFalling)
			{
				cyborg.fall();
			}
		}
		
		private function animateHearts():void
		{
			var heartToTrack:HeartPowerUp;
			
			if(heartsOnStage.length > 0)
			{
				for(var i:uint = 0; i < heartsOnStage.length; i++)
				{
					heartToTrack = heartsOnStage[i];

					if(heartToTrack.alreadyHit == false && heartToTrack.bounds.intersects(cyborg.bounds)) //YAY get the heart
					{
						heartToTrack.x = -heartToTrack.width; // this will remove from stage
						playerHealth += 10;
					}
					
					if(heartToTrack.x <= -heartToTrack.width) // when they go off the back of the screen
					{
						//remove from screen
						this.removeChild(heartToTrack);
						heartsOnStage.splice(i,1);
						heartToTrack = null;
					}else{
						heartToTrack.timeOnScreen -= 1; //
						
						// have the hearts float around the screen, moving backwards towards the player
						if(heartToTrack.y >= (gameArea.top+50))
						{
							heartToTrack.yDirection = 1;
						}
						else(heartToTrack.y <= groundLevel)
						{
							heartToTrack.yDirection = -1;
						}
						
						if(heartToTrack.yDirection > 0) // go down the screen
						{
							heartToTrack.y += Math.random() * 2;
						}else{ 							// go up the screen
							heartToTrack.y -= Math.random() * 2;
						}
						
						heartToTrack.x -= Math.random() * 10;
					}
				}	
			}
		}
		
		private function animateObstacles():void
		{
			var obstacleToTrack:Obstacle;
			
			for(var i:uint = 0; i<obstaclesToAnimate.length; i++)
			{
				obstacleToTrack = obstaclesToAnimate[i];
				
				if(obstacleToTrack.alreadyHit == false && obstacleToTrack.bounds.intersects(cyborg.bounds))
				{
					obstacleToTrack.alreadyHit = true;
					obstacleToTrack.rotation = deg2rad(70);
					hitObstacle = 30;	//timer for camera shake and player freeze
					playerHealth -=10;
					
					//add an explosions					
					for(var j:uint = 0; j < Math.ceil(Math.random()*6); j++)
					{
						var expl:Explosion = createExplosion();
						
						if(Math.random() > 0.5)
						{
							expl.scaleX = -1;
						}
						
						expl.x = cyborg.x - 10 - (Math.random() * 80);
						expl.y = cyborg.y - 40 - (Math.random() * 120);
						explosionsOnStage.push(expl);
					}					
					cameraShake();	
				}
				else if(obstacleToTrack.alreadyHit == true)
				{
					obstacleToTrack.x = -obstacleToTrack.width -5; // this will force the remove of the obstacle
				}
				
				if(obstacleToTrack.distance > 0)
				{
					obstacleToTrack.distance -= playerSpeed * elapsed;
				}else{
					obstacleToTrack.x -= (playerSpeed + obstacleToTrack.speed) * elapsed;
				}
				
				if(obstacleToTrack.x < -obstacleToTrack.width)
				{
					obstaclesToAnimate.splice(i,1);
					removeChild(obstacleToTrack);
					obstacleToTrack = null;
				}
			}
		}
		
		private function cameraShake():void
		{
			if(hitObstacle > 0)
			{
				this.x = Math.random() * hitObstacle;
				this.y = Math.random() * hitObstacle;
 			}
			else if(x != 0)
			{
				this.x = 0;
				this.y = 0;
			}			
		}
		
		private function initObstactle():void
		{
			if(obstacleGapCount < 1200)
			{
				obstacleGapCount += playerSpeed * elapsed;
			}else if(obstacleGapCount != 0){
				obstacleGapCount = 0;
				createObstacle(Math.ceil(Math.random()*7), Math.random() * 1000);
			}
		}
		
		private function createObstacle(type:Number, distance:Number):void
		{
			var obstacle:Obstacle = new Obstacle(type, distance, Math.random()*300);
			obstacle.x = stage.stageWidth;
			this.addChild(obstacle);
			
			obstacle.y = int(Math.random() * (gameArea.bottom - obstacle.height - gameArea.top)) + gameArea.top;
			obstacle.postition = "middle";
			
			obstaclesToAnimate.push(obstacle);
		}
		
		private function createExplosion():Explosion
		{
			var explosion:Explosion = new Explosion();
			this.addChild(explosion);
			return explosion;
		}
		
		private function createHeartPowerUp():HeartPowerUp
		{
			var heart:HeartPowerUp = new HeartPowerUp();
			this.addChild(heart);
			heartsOnStage.push(heart);
			heart.x = stage.stageWidth + heart.width; // spawn just off screen
			heart.y = Math.random() * 500;
			return heart;
		}
		
		public function deactivate():void
		{
			this.visible = false;
		}
	}
}