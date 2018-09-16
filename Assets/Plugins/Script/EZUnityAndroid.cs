using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class EZUnityAndroid:EZAdsInterface
{
    public event RewardedVideoFinishHandler RewardedVideoHandler;
    AndroidJavaObject mCurrentActivity;
    AndroidJavaObject mUnityObject;

    public EZUnityAndroid()
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

        AndroidJavaClass unityClass = new AndroidJavaClass("com.ads.util.Unity");
        if (unityClass == null)
        {
            Debug.Log("find com.ads.util.Unity fail");
            return;
        }
    }

    public enAdNetwork getAdNetworkName()
    {
        return enAdNetwork.Unity;
    }

    public bool initAds(params string[] list)
    {
        if(list.Length != 1)
        {
            Debug.LogError("invalid Unity initAds param:" + list.Length);
            return false;
        }

        mUnityObject = new AndroidJavaObject("com.ads.util.Unity", mCurrentActivity, list[0], true);

        return true;
    }

    public bool isBannerReady()
    {
        return false;
    }

    public bool isInterstitialReady()
    {
        return false;
    }

    public bool isRewardedVideoReady()
    {
        return mUnityObject.Call<bool>("IsRewardedVideoReady");
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
        return false;
    }

    public bool playRewardedVideo()
    {
        mUnityObject.Call("PlayRewardVideo");
        
        return true;
    }
    public void onRewardedVideoCallback(List<string> paramList)
    {
        if(RewardedVideoHandler != null)
        {
            RewardedVideoHandler(null);
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
