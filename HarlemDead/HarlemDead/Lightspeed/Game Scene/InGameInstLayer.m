//
//  InGameInstLayer.m
//  SpaceraceJoyride
//
//  Created by Nelson Andre on 12-10-18.
//  Copyright 2012 NetMatch. All rights reserved.
//

#import "InGameInstLayer.h"
#import "SimpleAudioEngine.h"
#import "Globals.h"


@implementation InGameInstLayer


- (id) init
{
	self = [super init];
    
    CCMenuItem *gobackItem;
	if (self)
	{
		CGSize winSize = [[CCDirector sharedDirector] winSize];
        //if we are dealing with iPhone
        if (UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPhone)
        {
            if (winSize.height > 960/2)
            {
                
                CGPoint pos = ccp(0, (1136 - 960)/2/2);
                [self setPosition:pos];
            }
            winSize = CGSizeMake(640*0.5, 960*0.5);
            
            CGSize menuItemSize = CGSizeMake(208, 84);
            
            CCSprite* background = [CCSprite spriteWithFile:@"lg-iginst.png"];
            [self addChild:background z:-1];
            background.position = ccp(winSize.width/2, winSize.height/2);
            
            gobackItem = [CCMenuItem itemWithTarget:self selector:@selector(goback:)];
            [gobackItem setContentSize:menuItemSize];
            [gobackItem setPosition:ccp(winSize.width*0.31, winSize.height*0.1)];
        }
        else if (UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad)
        {
            CCSprite* background = [CCSprite spriteWithFile:@"lg-iginst-ipad.png"];
            CGSize menuItemSize = CGSizeMake(466, 184);
            [self addChild:background z:-1];
            background.position = ccp(winSize.width/2, winSize.height/2);
            
            gobackItem = [CCMenuItem itemWithTarget:self selector:@selector(goback:)];
            [gobackItem setContentSize:menuItemSize];
            [gobackItem setPosition:ccp(winSize.width*0.31, winSize.height*0.1)];
        }
        else {
            return nil;
        }
        
        CCMenu *menu = [CCMenu menuWithItems:gobackItem, nil];
        [menu setPosition: ccp(0, 0)];
        
        [self addChild:menu];
	}
	return self;
}

- (void) goback: (id) sender
{
    [[SimpleAudioEngine sharedEngine] playEffect:MENU_CHANGE_SOUND];
    [self removeFromParentAndCleanup:YES];
    [[CCDirector sharedDirector] resume];
}


@end