using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class GoogleAdMobManager : MonoBehaviour
{
    public GameManager gameManager;

    public int rewardedCheck;

    private void Awake()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) => {  });

        rewardedCheck = 0;
        Debug.Log("rewardedCheck:0");
    }
    
    public void Start()
    {
        LoadInterstitialAd();
    }

    #if UNITY_ANDROID
        private string _adUnitId = "ca-app-pub-4493483254988632/8868781639";
    #elif UNITY_IPHONE
        private string _adUnitId = "unused";
    #else
        private string _adUnitId = "unused";
    #endif

    private InterstitialAd _interstitialAd;

    public void LoadInterstitialAd()
    {
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        var adRequest = new AdRequest();

        InterstitialAd.Load(_adUnitId, adRequest,
        (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Interstitial ad failed to load an ad" + "with error : " + error);
                
                return;
            }

            Debug.Log("Interstitial ad loaded with response : "
                        + ad.GetResponseInfo());

            _interstitialAd = ad;
        });
    }

    public void ShowInterstitialAd()
    {
            if (_interstitialAd != null && _interstitialAd.CanShowAd())
            {
                AudioManager.instance.PlaySfx(AudioManager.Sfx.ClickButton);
                
                Debug.Log("Showing interstitial ad.");
                _interstitialAd.Show();

                rewardedCheck = 1;
                Debug.Log("rewardedCheck:1");

                GameManager.Instance.GetReward();
            }
            else
            {
                Debug.LogError("Interstitial ad is not ready yet.");

                GameManager.Instance.AdIsNotReadyYet();
            }
    }

    private void RegisterEventHandler(InterstitialAd interstitialAd)
    {
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("AdIsClosed");
        };

        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            //LoadRewardedAd();
        };
    }
}