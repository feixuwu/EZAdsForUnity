using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EZFacebookAndroid:EZAdsInterface
{
    public event RewardedVideoFinishHandler RewardedVideoHandler;

    AndroidJavaObject mCurrentActivity;
    AndroidJavaObject mFacebookObject;
    AndroidJavaObject mActivityLayout;

    public EZFacebookAndroid()
    {
        AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        if (activityClass == null)
        {
            Debug.Log("find UnityPlayer class fail");
            return;
        }
        mCurrentActivity = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
        if (mCurrentActivity == null)
        {
            Debug.Log("find current activity fail");
            return;
        }

        mActivityLayout = new AndroidJavaObject("android.widget.LinearLayout", mCurrentActivity);
        AndroidJavaClass facebookClass = new AndroidJavaClass("com.ads.util.Facebook");
        if (facebookClass == null)
        {
            Debug.Log("find com.ads.util.Facebook fail");
            return;
        }

        mFacebookObject = new AndroidJavaObject("com.ads.util.Facebook", mCurrentActivity, mActivityLayout, true);
    }

    public enAdNetwork getAdNetworkName()
    {
        return enAdNetwork.Facebook;
    }

    public bool initAds(params string[] list)
    {
        if (list.Length != 3)
        {
            Debug.LogError("facebook initAds fail invalid paramlen:" + list.Length);
            return false;
        }
        mFacebookObject.Call("InitFacebook", list[0], list[1], list[2]);

        return true;
    }

    public bool isBannerReady()
    {
        return mFacebookObject.Call<bool>("IsBannerReady");
    }

    public bool isInterstitialReady()
    {
        return mFacebookObject.Call<bool>("IsInterstitialReady");
    }

    public bool isRewardedVideoReady()
    {
        return mFacebookObject.Call<bool>("IsRewardedVideoReady");
    }

    public bool showBanner(bool isBottom)
    {
        mFacebookObject.Call("ShowBanner", isBottom);
        return true;
    }

    public bool hideBanner()
    {
        mFacebookObject.Call("HideAdBanner");
        return true;
    }

    public bool showInterstitial()
    {
        mFacebookObject.Call("ShowInterstitialAd");
        return true;
    }

    public bool playRewardedVideo()
    {
        mFacebookObject.Call("playRewardAds");
        return true;
    }

    public void onRewardedVideoCallback(List<string> paramList)
    {
        if (RewardedVideoHandler != null)
        {
            RewardedVideoHandler(null);
        }
    }

    public void onStart()
    {

    }

    public void onStop()
    {
        mFacebookObject.Call("OnDestroy");
    }

    public void onPause()
    {

    }

    public void onResume()
    {

    }
}

