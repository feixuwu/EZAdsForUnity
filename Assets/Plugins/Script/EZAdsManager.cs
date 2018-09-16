using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public static class EZAdsManager
{
    private static Dictionary<enAdNetwork, EZAdsInterface> mAdsDic = new Dictionary<enAdNetwork, EZAdsInterface>();
    private static Dictionary<enAdNetwork, RewardedVideoFinishHandler> mEventDic = new Dictionary<enAdNetwork, RewardedVideoFinishHandler>();
    private static Dictionary<string, enAdNetwork> mAdTypeDic = new Dictionary<string, enAdNetwork>();

    static internal void registerAdObject(EZAdsInterface adObject)
    {
        if (adObject == null)
        {
            Debug.LogError("invalid adobject");
            return;
        }

        try
        {
            mAdsDic.Add(adObject.getAdNetworkName(), adObject);
            mAdTypeDic[adObject.getAdNetworkName().ToString()] = adObject.getAdNetworkName();
        }
        catch(System.Exception e)
        {
            Debug.LogError("registerAdObject fail:" + e);
        }
    }

    static internal bool initAdObject(EZAdsCreateCtx ctx)
    {
        if (!mAdsDic.ContainsKey(ctx.adNetwork))
        {
            Debug.LogError("invalid adnetwork:" + ctx.adNetwork);
            return false;
        }

        EZAdsInterface adObject = mAdsDic[ctx.adNetwork];
        if (adObject == null)
        {
            Debug.LogError("invalid adobject:" + ctx.adNetwork);
            return false;
        }

        try
        {
            return adObject.initAds(ctx.szParamList);
        }
        catch(System.Exception e)
        {
            Debug.LogError("initAdObject fail:" + e);
            return false;
        }
    }

    static internal void callbackRewardedVideo(string strResult)
    {
        if (strResult == null || strResult.Length == 0)
        {
            Debug.Log("rewarded video call back is not valid");
            return;
        }

        try
        {
            List<string> paramList = strResult.Split(new string[] { "," }, StringSplitOptions.None).ToList<string>();
            string strAdNetwork = paramList[0];

            if(!mAdTypeDic.ContainsKey(strAdNetwork) )
            {
                Debug.Log("callbackRewardedVideo adnetwork not valid:" + strAdNetwork);
                return;
            }

            enAdNetwork adNetwork = mAdTypeDic[strAdNetwork];
            if (!mAdsDic.ContainsKey(adNetwork))
            {
                Debug.LogError("callbackRewardedVideo invalid adnetwork:" + strAdNetwork);
                return;
            }

            EZAdsInterface adObject = mAdsDic[adNetwork];
            paramList.RemoveAt(0);

            adObject.onRewardedVideoCallback(paramList);
        }
        catch(System.Exception e)
        {
            Debug.LogError("callbackRewardedVideo error:" + e);
        }
    }

    public static bool isBannerReady(enAdNetwork adNetwork)
    {
        if (!mAdsDic.ContainsKey(adNetwork))
        {
            //Debug.LogError("isBannerReady invalid adnetwork:" + adNetwork);
            return false;
        }

        EZAdsInterface adObject = mAdsDic[adNetwork];
        return adObject.isBannerReady();
    }

    public static bool isInterstitialReady(enAdNetwork adNetwork)
    {
        if (!mAdsDic.ContainsKey(adNetwork))
        {
            //Debug.LogError("isInterstitialReady invalid adnetwork:" + adNetwork);
            return false;
        }

        EZAdsInterface adObject = mAdsDic[adNetwork];
        return adObject.isInterstitialReady();
    }

    public static bool isRewardedVideoReady(enAdNetwork adNetwork)
    {
        if (!mAdsDic.ContainsKey(adNetwork))
        {
            //Debug.LogError("isRewardedVideoReady invalid adnetwork:" + adNetwork);
            return false;
        }

        EZAdsInterface adObject = mAdsDic[adNetwork];
        return adObject.isRewardedVideoReady();
    }

    public static bool showBanner(enAdNetwork adNetwork, bool isBottom)
    {
        if (!mAdsDic.ContainsKey(adNetwork))
        {
            Debug.LogError("showBanner invalid adnetwork:" + adNetwork);
            return false;
        }

        EZAdsInterface adObject = mAdsDic[adNetwork];
        return adObject.showBanner(isBottom);
    }

    public static bool showInterstitial(enAdNetwork adNetwork)
    {
        if (!mAdsDic.ContainsKey(adNetwork))
        {
            Debug.LogError("showInterstitial invalid adnetwork:" + adNetwork);
            return false;
        }

        EZAdsInterface adObject = mAdsDic[adNetwork];
        return adObject.showInterstitial();
    }

    public static bool playRewardedVideo(enAdNetwork adNetwork, RewardedVideoFinishHandler handler)
    {
        if (!mAdsDic.ContainsKey(adNetwork))
        {
            Debug.LogError("showRewardedVideo invalid adnetwork:" + adNetwork);
            return false;
        }

        EZAdsInterface adObject = mAdsDic[adNetwork];

        if(mEventDic.ContainsKey(adNetwork) )
        {
            adObject.RewardedVideoHandler -= mEventDic[adNetwork];
            mEventDic.Remove(adNetwork);
        }
        adObject.RewardedVideoHandler += handler;
        mEventDic.Add(adNetwork, handler);
        return adObject.playRewardedVideo();
    }

    internal static void onStart()
    {
        foreach(var kv in mAdsDic)
        {
            kv.Value.onStart();
        }
    }

    internal static void onPause()
    {
        foreach (var kv in mAdsDic)
        {
            kv.Value.onPause();
        }
    }

    internal static void onResume()
    {
        foreach (var kv in mAdsDic)
        {
            kv.Value.onResume();
        }
    }

    internal static void onStop()
    {
        foreach (var kv in mAdsDic)
        {
            kv.Value.onStop();
        }
    }
}

