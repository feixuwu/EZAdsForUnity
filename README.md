# EZAdsForUnity

  this plugin try to help you to integrates serveral best game ad networks to your game very easily, just drag the prefab(Assets/Prefab/EZAds.prefab)
to your first game scene, then select the EZAds object, and replace the ad unit info to your own ad units.
then the plugin will automatic load and refresh ads for you, you can check and play ads any time in any place.

# FEATURES: 
★ 4 best game ad networks supported (more coming soon).

★  extremely easy  integrates.

★  automatic refresh ads for you.

if you have any problem, please feel free to email me:feixuwu@outlook.com

# PLATFORM SUPPORTED:
★ Android

★ IOS(comming soon)

# AD TYPES:
★ Banner(Admob,Facebook Audience)

★ Interstitial (text, picture, video)(Admob, ChartBoost,Facebook Audience)

★ Reward Video(Admob, Unity, ChartBoost,Facebook Audience )

# SUPPORTED AD NETWORKS(more comming soon): 
★ AdMob(Banner, Interstitial, Rewarded Video)

★ ChartBoost(Interstitial, Rewarded Video)

★ Unity(Rewarded Video)

★ Facebook Audience(Banner, Interstitial, Rewarded Video)

# Install

1.copy all the project files from Assets to your own project. Or you can export this project to a package, and then import the package
  to your own project.
  
2.drag the EZAds prefab from Assets/Prefab/ to your first scene, and then select this object in your scene, you can replace the ad unit 
 information in the object inspector editor.
 
3.check and show ads.

you can check my demo project, to see how it works.

# API
```csharp
// this is the supported ad network enum
enum enAdNetwork
{
    Admob = 0,
    Chartboost = 1,
    Facebook = 2,
    Unity = 3,   
}

// rewarded video ads finish call back type
public delegate void RewardedVideoFinishHandler(object award);

// admob rewarded video call back type
public struct AdmobRewardedItem
{
    public string strType;
    public int iAmount;
}

// chartboost rewarded video call back type
public struct ChartboostRewardItem
{
    public string strLocation;
    public int iValue;
}

// ads manager, static class
EZAdsManager
   
   // check if the banner is ready, remember not all the ad network support
   public static bool isBannerReady(enAdNetwork adNetwork);
   
   // check if the interstitial ads is ready, remember not all the ad network support
   public static bool isInterstitialReady(enAdNetwork adNetwork);
   
   // check if the rewarded video is ready, remember not all the ad network support
   public static bool isRewardedVideoReady(enAdNetwork adNetwork);
   
   // show the banner, there's two position can show banner, top and bottom
   public static bool showBanner(enAdNetwork adNetwork, bool isBottom);
   
   // hide the banner
   public static void hideBanner(enAdNetwork adNetwork);
   
   // show interstitial ads
   public static bool showInterstitial(enAdNetwork adNetwork);
   
   // show the rewarded video ads, you can set a callback when the ads is finished, so you can do something
   public static bool playRewardedVideo(enAdNetwork adNetwork, RewardedVideoFinishHandler handler);
   
```





     



  

