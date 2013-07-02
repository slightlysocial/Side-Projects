//
//  MobClixManager.h
//  MobClixTest
//
//  Created by Mike on 8/22/10.
//  Copyright 2010 Prime31 Studios. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "MobclixAds.h"
#import "MobclixFullScreenAdViewController.h"

typedef enum
{
	MobclixBannerTypeiPhone_320x50,
	MobclixBannerTypeiPhone_300x250,
	MobclixBannerTypeiPad_300x250,
	MobclixBannerTypeiPad_728x90,
	MobclixBannerTypeiPad_120x600,
	MobclixBannerTypeiPad_468x60
} MobclixBannerType;

typedef enum
{
	MobclixAdPositionTopLeft,
	MobclixAdPositionTopCenter,
	MobclixAdPositionTopRight,
	MobclixAdPositionCentered,
	MobclixAdPositionBottomLeft,
	MobclixAdPositionBottomCenter,
	MobclixAdPositionBottomRight
} MobclixAdPosition;


@interface MobclixManager : NSObject <MobclixAdViewDelegate, MobclixFullScreenAdDelegate>
@property (nonatomic, retain) MobclixAdView *adView;
@property (nonatomic, retain) MobclixFullScreenAdViewController *fullScreenAdViewController;
@property (nonatomic) MobclixAdPosition bannerPosition;


+ (MobclixManager*)sharedManager;


- (void)startWithApplicationId:(NSString*)applicationId;

- (void)setRefreshTime:(CGFloat)refreshTime;

- (void)createBanner:(MobclixBannerType)bannerType withPosition:(MobclixAdPosition)position;

- (void)showBanner;

- (void)hideBanner:(BOOL)shouldDestroy;

- (void)requestFullScreenAd;

- (void)displayFullScreenAd;

- (void)requestAndDisplayFullScreenAd;

- (BOOL)isFullScreenAdReady;

@end
