using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public delegate void RewardedVideoFinishHandler(object award);
public enum enAdNetwork
{
    Admob = 0,
    Chartboost = 1,
    Facebook = 2,
    Unity = 3,
    
}

public struct AdmobRewardedItem
{
    public string strType;
    public int iAmount;
}

public struct ChartboostRewardItem
{
    public string strLocation;
    public int iValue;
}

interface EZAdsInterface
{
    event RewardedVideoFinishHandler RewardedVideoHandler;

    enAdNetwork getAdNetworkName();
    bool initAds(params string[] list);
    bool isBannerReady();
    bool isInterstitialReady();
    bool isRewardedVideoReady();

    bool showBanner(bool isBottom);
    bool hideBanner();
    bool showInterstitial();
    bool playRewardedVideo();
    void onRewardedVideoCallback(List<string> paramList);
    void onStart();
    void onStop();
    void onPause();
    void onResume();
}
