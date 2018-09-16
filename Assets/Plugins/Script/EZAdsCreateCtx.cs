using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


struct EZAdsCreateCtx
{
    public enAdNetwork adNetwork;
    public string[] szParamList;
}

[Serializable]
public struct EZAdmobConfig
{
    public string AppId;
    public string BannerId;
    public string InterstitialId;
    public string RewardedVideoId;

    internal EZAdsCreateCtx toCreateCtx()
    {
        if (AppId == null) AppId = "";
        if (BannerId == null) BannerId = "";
        if (InterstitialId == null) InterstitialId = "";
        if (RewardedVideoId == null) RewardedVideoId = "";

        EZAdsCreateCtx ret;
        ret.adNetwork = enAdNetwork.Admob;
        ret.szParamList = new string[] { AppId, BannerId, InterstitialId , RewardedVideoId };

        return ret;
    }
}

[Serializable]
public struct EZChartboostConfig
{
    public string AppId;
    public string Signature;

    internal EZAdsCreateCtx toCreateCtx()
    {
        if (AppId == null) AppId = "";
        if (Signature == null) Signature = "";
        
        EZAdsCreateCtx ret;
        ret.adNetwork = enAdNetwork.Chartboost;
        ret.szParamList = new string[] { AppId, Signature};

        return ret;
    }
}

[Serializable]
public struct EZFacebookConfig
{
    public string Banner;
    public string Interstitial;
    public string RewardedVideo;

    internal EZAdsCreateCtx toCreateCtx()
    {
        if (Banner == null) Banner = "";
        if (Interstitial == null) Interstitial = "";
        if (RewardedVideo == null) RewardedVideo = "";

        EZAdsCreateCtx ret;
        ret.adNetwork = enAdNetwork.Facebook;
        ret.szParamList = new string[] { Banner, Interstitial, RewardedVideo};

        return ret;
    }
}

[Serializable]
public struct EZUnityConfig
{
    public string AppId;

    internal EZAdsCreateCtx toCreateCtx()
    {
        if (AppId == null) AppId = "";
        
        EZAdsCreateCtx ret;
        ret.adNetwork = enAdNetwork.Unity;
        ret.szParamList = new string[] { AppId};

        return ret;
    }
}


[Serializable]
public struct EZAdAndroidSetting
{
    public EZAdmobConfig Admob;
    public EZChartboostConfig Chartboost;
    public EZFacebookConfig Facebook;
    public EZUnityConfig Unity;
}

