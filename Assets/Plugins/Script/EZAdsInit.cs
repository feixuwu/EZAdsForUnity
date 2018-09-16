using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EZAdsInit : MonoBehaviour {

    public EZAdAndroidSetting Android;

    AndroidJavaObject mCurrentActivity;
    AndroidJavaObject mAdmobObject;
    AndroidJavaObject mActivityLayout;

    // Use this for initialization
    void Start () {

        gameObject.name = "EZAds";
        DontDestroyOnLoad(gameObject);

        EZAdsManager.registerAdObject(new EZAdmobAndroid() );
        EZAdsManager.initAdObject(Android.Admob.toCreateCtx() );

        EZAdsManager.registerAdObject(new EZChartboostAndroid());
        EZAdsManager.initAdObject(Android.Chartboost.toCreateCtx());

        EZAdsManager.registerAdObject(new EZFacebookAndroid());
        EZAdsManager.initAdObject(Android.Facebook.toCreateCtx());

        EZAdsManager.registerAdObject(new EZUnityAndroid());
        EZAdsManager.initAdObject(Android.Unity.toCreateCtx());

        EZAdsManager.onStart();
    }

    private void OnApplicationQuit()
    {
        EZAdsManager.onStop();
    }

    private void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            EZAdsManager.onPause();
        }
        else
        {
            EZAdsManager.onResume();
        }
    }

    void OnRewardedVideoFinish(string strResult)
    {
        Debug.Log("player rewarded video finish:" + strResult);
        EZAdsManager.callbackRewardedVideo(strResult);
    }
}
