/*
Cyborg has:
stand
run
runBack
squat
jump
fall
fallEnd
*/
package objects
{
	import dragonBones.Armature;
	import dragonBones.factorys.StarlingFactory;
	
	import flash.events.Event;
	
	import starling.display.Sprite;
	import starling.events.Event;
	import starling.utils.deg2rad;
	
	public class CyborgController extends Sprite
	{
		[Embed(source = "../../assets/cyborg.png", mimeType = "application/octet-stream")]  
		private static const ResourcesData:Class;
		
		private var factory:StarlingFactory
		private var armature:Armature;
		
		private var particlesToAnimate:Vector.<Particle>;
		
		public function CyborgController()
		{
			super();
			
			factory = new StarlingFactory(); 
			factory.addEventListener(flash.events.Event.COMPLETE, textureCompleteHandler);
			factory.parseData(new ResourcesData());
		}
		
		private function textureCompleteHandler(event:flash.events.Event):void
		{
			// TODO Auto-generated method stub
			armature = factory.buildArmature("cyborg");
			addChild(armature.display as Sprite);
			
			particlesToAnimate = new Vector.<Particle>;
			
			armature.animation.gotoAndPlay('run');
			
			addEventListener(starling.events.Event.ENTER_FRAME, onFrame);
		}
		
		private function createParticles():void
		{
			var count:int = 5;
			
			while(count > 0)
			{
				count--;
				
				var particle:Particle = new Particle();
				this.addChild(particle);
				
				particle.x = Math.random() * 40 - 20;
				particle.y = Math.random() * 40;
				
				particle.speedX = Math.random() * 2 + 1;
				particle.speedY = Math.random() * 5;
				particle.spin = Math.random() *15;
				
				particle.scaleX = particle.scaleY = Math.random() * 0.3 + 0.3;
				
				particlesToAnimate.push(particle);
			}		
		}
		
		private function onFrame(e:starling.events.Event):void
		{
			armature.update();	// this will play the animation
			createParticles();
			animateParticles();
		}
		
		private function animateParticles():void
		{
			for(var i:uint = 0; i < particlesToAnimate.length; i++)
			{
				var particleToTrack:Particle = particlesToAnimate[i];
				
				if(particleToTrack)
				{
					particleToTrack.scaleX -= 0.03;
					particleToTrack.scaleY = particleToTrack.scaleX;
					
					particleToTrack.y -= particleToTrack.speedY;
					particleToTrack.speedY -= particleToTrack.speedY * 0.2;
					
					particleToTrack.rotation += deg2rad(particleToTrack.spin);
					particleToTrack.spin *= 1.1;
					
					if(particleToTrack.scaleY <= 0.02)
					{
						particlesToAnimate.splice(i,1);
						this.removeChild(particleToTrack);
						particleToTrack = null;
					}
				}
			}
		}
		
		public function stand():void
		{
			armature.animation.gotoAndPlay('stand');
		}
		
		public function jump():void
		{
			armature.animation.gotoAndPlay('jump');
		}
		
		public function run():void
		{
			armature.animation.gotoAndPlay('run');			
		}
		
		public function fall():void
		{
			armature.animation.gotoAndPlay('fall');
		}
	}
}