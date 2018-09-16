using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoTest : MonoBehaviour {

    public UnityEngine.UI.Text AdmobBannerText;
    public UnityEngine.UI.Text AdmobInterstitialText;
    public UnityEngine.UI.Text AdmobRewardedVideoText;
    
    public UnityEngine.UI.Text ChartboostInterstitialText;
    public UnityEngine.UI.Text ChartboostRewardedVideoText;

    public UnityEngine.UI.Text FacebookBannerText;
    public UnityEngine.UI.Text FacebookInterstitialText;
    public UnityEngine.UI.Text FacebookRewardedVideoText;
    
    public UnityEngine.UI.Text UnityRewardedVideoText;


    internal struct TextDesc
    {
        internal static TextDesc createTextDesc(UnityEngine.UI.Text p1, enAdNetwork p2)
        {
            TextDesc ret;
            ret.text = p1;
            ret.adNetwork = p2;

            return ret;
        }
        internal UnityEngine.UI.Text text;
        internal enAdNetwork adNetwork;
    }

    
    private TextDesc[] mBannerArray;
    private TextDesc[] mInterstitialArray;
    private TextDesc[] mRewardedVideoArray;


    // Use this for initialization
    void Start()
    {
        mBannerArray = new TextDesc[] { TextDesc.createTextDesc(AdmobBannerText, enAdNetwork.Admob),  TextDesc.createTextDesc(FacebookBannerText, enAdNetwork.Facebook) };
        mInterstitialArray = new TextDesc[] { TextDesc.createTextDesc(AdmobInterstitialText, enAdNetwork.Admob), TextDesc.createTextDesc(ChartboostInterstitialText, enAdNetwork.Chartboost),
        TextDesc.createTextDesc(FacebookInterstitialText, enAdNetwork.Facebook) };
        mRewardedVideoArray = new TextDesc[] { TextDesc .createTextDesc(AdmobRewardedVideoText, enAdNetwork.Admob), TextDesc.createTextDesc(ChartboostRewardedVideoText, enAdNetwork.Chartboost),
        TextDesc.createTextDesc(FacebookRewardedVideoText, enAdNetwork.Facebook), TextDesc.createTextDesc(UnityRewardedVideoText, enAdNetwork.Unity)  };

        for(int i = 0; i < mBannerArray.Length; i++)
        {
            mBannerArray[i].text.color = Color.red;
        }

        for (int i = 0; i < mInterstitialArray.Length; i++)
        {
            mInterstitialArray[i].text.color = Color.red;
        }

        for (int i = 0; i < mRewardedVideoArray.Length; i++)
        {
            mRewardedVideoArray[i].text.color = Color.red;
        }
    }

    public void OnShowBanner()
    {
        EZAdsManager.showBanner(enAdNetwork.Admob, false);
    }

    public void OnShowInterstitial()
    {
        EZAdsManager.showInterstitial(enAdNetwork.Admob);
    }

    void OnAdmobRewardedVideoFinish(object result)
    {
        AdmobRewardedItem rewardItem = (AdmobRewardedItem)result;
        Debug.Log("Finish Admob video:" + rewardItem.strType + "  " + rewardItem.iAmount);
    }

    public void onChartboostShowInterstitial()
    {
        EZAdsManager.showInterstitial(enAdNetwork.Chartboost);
    }

    public void OnAdmobRewardedVideoPlay()
    {
        RewardedVideoFinishHandler handler = OnAdmobRewardedVideoFinish;
        EZAdsManager.playRewardedVideo(enAdNetwork.Admob, handler);
    }

    public void onChartboostRewardedVideoPlay()
    {
        RewardedVideoFinishHandler handler = onChartboostRewardedVideoFinish;
        EZAdsManager.playRewardedVideo(enAdNetwork.Chartboost, handler);
    }

    void onChartboostRewardedVideoFinish(object result)
    {
        ChartboostRewardItem item = (ChartboostRewardItem)result;
        Debug.Log("Chartboost video finish:" + item.strLocation + " value:" + item.iValue);
    }

    public void OnFacebookShowBanner()
    {
        EZAdsManager.showBanner(enAdNetwork.Facebook, true);
    }

    public void OnFacebookShowInterstitial()
    {
        EZAdsManager.showInterstitial(enAdNetwork.Facebook);
    }

    private void onFacebookVideoFinish(object result)
    {
        Debug.Log("Facebook RewardedVideo Finish Play");
    }

    public void OnFacebookPlayVideo()
    {
        EZAdsManager.playRewardedVideo(enAdNetwork.Facebook, onFacebookVideoFinish);
    }

    private void OnUnityFinishPlay(object result)
    {
        Debug.Log("Unity RewardedVideo Finish Play");
    }

    public void OnUnityPlayVideo()
    {
        EZAdsManager.playRewardedVideo(enAdNetwork.Unity, OnUnityFinishPlay);
    }

    // Update is called once per frame
    void Update () {

        for (int i = 0; i < mBannerArray.Length; i++)
        {
            if(EZAdsManager.isBannerReady(mBannerArray[i].adNetwork) )
            {
                mBannerArray[i].text.color = Color.black;
            }
            else
            {
                mBannerArray[i].text.color = Color.red;
            }
        }

        for (int i = 0; i < mInterstitialArray.Length; i++)
        {
            if (EZAdsManager.isInterstitialReady(mInterstitialArray[i].adNetwork))
            {
                mInterstitialArray[i].text.color = Color.black;
            }
            else
            {
                mInterstitialArray[i].text.color = Color.red;
            }
        }

        for (int i = 0; i < mRewardedVideoArray.Length; i++)
        {
            if(EZAdsManager.isRewardedVideoReady(mRewardedVideoArray[i].adNetwork) )
            {
                mRewardedVideoArray[i].text.color = Color.black;
            }
            else
            {
                mRewardedVideoArray[i].text.color = Color.red;
            }
        }
    }
}
