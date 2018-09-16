using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EZAdmobAndroid:EZAdsInterface
{
    public event RewardedVideoFinishHandler RewardedVideoHandler;

    AndroidJavaObject mCurrentActivity;
    AndroidJavaObject mAdmobObject;
    AndroidJavaObject mActivityLayout;

    public EZAdmobAndroid()
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
        AndroidJavaClass admobClass = new AndroidJavaClass("com.ads.util.AdMob");
        if (admobClass == null)
        {
            Debug.Log("find com.ads.util.AdMob fail");
            return;
        }

        mAdmobObject = new AndroidJavaObject("com.ads.util.AdMob", mCurrentActivity, mActivityLayout, true);
    }

    public enAdNetwork getAdNetworkName()
    {
        return enAdNetwork.Admob;
    }

    public bool initAds(params string[] list)
    {
        if(list.Length != 4)
        {
            Debug.LogError("Admob Android InitAds fail");
            return false;
        }

        mAdmobObject.Call("InitAdMob", list[0], list[1], list[2], list[3]);

        return true;
    }

    public bool isBannerReady()
    {
        return mAdmobObject.Call<bool>("IsBannerReady");
    }

    public bool isInterstitialReady()
    {
        return mAdmobObject.Call<bool>("IsInterstitialReady");
    }

    public bool isRewardedVideoReady()
    {
        return mAdmobObject.Call<bool>("IsRewardedVideoReady");
    }

    public bool showBanner(bool isBottom)
    {
        mAdmobObject.Call("ShowBanner", isBottom);
        return true;
    }

    public bool hideBanner()
    {
        mAdmobObject.Call("HideAdBanner");
        return true;
    }

    public bool showInterstitial()
    {
        mAdmobObject.Call("ShowInterstitialAd");
        return true;
    }

    public bool playRewardedVideo()
    {
        mAdmobObject.Call("playRewardAds");
        return true;
    }

    public void onRewardedVideoCallback(List<string> paramList)
    {
        if(paramList.Count != 2)
        {
            return;
        }

        AdmobRewardedItem rewardedItem;
        rewardedItem.strType = paramList[0];
        rewardedItem.iAmount = int.Parse(paramList[1]);

        if(RewardedVideoHandler != null)
        {
            RewardedVideoHandler(rewardedItem);
        }
    }

    public void onStart()
    {

    }

    public void onStop()
    {

    }

    public void onPause()
    {

    }

    public void onResume()
    {

    }
}

