
#import "ExplosionParticle.h"


@implementation ExplosionParticle

-(id) init
{
	return [self initWithTotalParticles:700];
	
}

-(id) initWithTotalParticles:(NSUInteger) numberOfParticles
{
	if( !(self=[super initWithTotalParticles:numberOfParticles]) )
		return nil;
	
	// duration
	duration = 0.05f;
	
	self.emitterMode = kCCParticleModeGravity;
	
	// Gravity Mode: gravity
	self.gravity = ccp(0,0);
	
	// Gravity Mode: speed of particles
	self.speed = 90;
	self.speedVar = 40;
	
	// Gravity Mode: radial
	self.radialAccel = 0;
	self.radialAccelVar = 0;
	
	// Gravity Mode: tagential
	self.tangentialAccel = 0;
	self.tangentialAccelVar = 0;
	
	// angle
	angle = 90;
	angleVar = 360;
	
	// emitter position
	CGSize winSize = [[CCDirector sharedDirector] winSize];
	self.position = ccp(winSize.width/2, winSize.height/2);
	posVar = CGPointZero;
	
	// life of particles
	life = 5.0f;
	lifeVar = 2;
	
	// size, in pixels
	startSize = 15.0f;
	startSizeVar = 10.0f;
	endSize = kCCParticleStartSizeEqualToEndSize;
	
	// emits per second
	emissionRate = totalParticles/duration;
	
	// color of particles
	startColor.r = 0.7f;
	startColor.g = 0.1f;
	startColor.b = 0.2f;
	startColor.a = 1.0f;
	startColorVar.r = 0.5f;
	startColorVar.g = 0.5f;
	startColorVar.b = 0.5f;
	startColorVar.a = 0.0f;
	endColor.r = 0.5f;
	endColor.g = 0.5f;
	endColor.b = 0.5f;
	endColor.a = 0.0f;
	endColorVar.r = 0.5f;
	endColorVar.g = 0.5f;
	endColorVar.b = 0.5f;
	endColorVar.a = 0.0f;
	
	self.texture = [[CCTextureCache sharedTextureCache] addImage: @"fire.png"];
	self.autoRemoveOnFinish = YES;
	
	// additive
	self.blendAdditive = NO;
	
	return self;
}
@end
