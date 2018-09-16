using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EZChartboostAndroid:EZAdsInterface
{
    AndroidJavaObject mCurrentActivity;
    AndroidJavaClass mChartboostClass;
    AndroidJavaObject mChartboostObject;

    public event RewardedVideoFinishHandler RewardedVideoHandler;

    public EZChartboostAndroid()
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

        mChartboostClass = new AndroidJavaClass("com.ads.util.ChartBoost");
        if (mChartboostClass == null)
        {
            Debug.Log("find com.ads.util.ChartBoost fail");
            return;
        }
    }

    public enAdNetwork getAdNetworkName()
    {
        return enAdNetwork.Chartboost;
    }

    public bool initAds(params string[] list)
    {
        try
        {
            if(list.Length != 2)
            {
                Debug.LogError("chartboost param invalid");
                return false;
            }
            mChartboostObject = new AndroidJavaObject("com.ads.util.ChartBoost", mCurrentActivity, list[0], list[1], true);
        }
        catch(System.Exception e)
        {
            Debug.LogError("create chartboost object fail:" + e);
            return false;
        }
        
        return true;
    }

    public bool isBannerReady()
    {
        return false;
    }

    public bool isInterstitialReady()
    {
        return mChartboostObject.Call<bool>("IsInterstitialReady");
    }

    public bool isRewardedVideoReady()
    {
        return mChartboostObject.Call<bool>("IsRewardedVideoReady");
    }

    public bool showBanner(bool isBottom)
    {
        return false;
    }

    public bool hideBanner()
    {
        return false;
    }

    public bool showInterstitial()
    {
        mChartboostObject.Call("ShowInterstitalAds");
        return true;
    }

    public bool playRewardedVideo()
    {
        mChartboostObject.Call("PlayRewardedVideo");
        return true;
    }

    public void onRewardedVideoCallback(List<string> paramList)
    {
        if(paramList.Count != 2)
        {
            Debug.LogError("invalid onRewardedVideoCallback param count:" + paramList.Count);
            return;
        }

        if (RewardedVideoHandler != null)
        {
            ChartboostRewardItem item;
            item.strLocation = paramList[0];
            item.iValue = int.Parse(paramList[1]);

            RewardedVideoHandler(item);
        }
    }

    public void onStart()
    {
        mChartboostObject.Call("OnStart");
    }

    public void onStop()
    {
        mChartboostObject.Call("onStop");
    }

    public void onPause()
    {
        mChartboostObject.Call("onPause");
    }

    public void onResume()
    {
        mChartboostObject.Call("onResume");
    }
}

