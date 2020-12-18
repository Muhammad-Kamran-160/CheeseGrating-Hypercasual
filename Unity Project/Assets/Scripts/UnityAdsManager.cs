using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsManager : MonoBehaviour, IUnityAdsListener
{
    public static UnityAdsManager _instance;

    string googlePay_ID = "3915357";
    string appStore_ID = "3915356";
    string myPlacementId = "rewardedVideo";
    bool testMode = true;

    private void Awake ()
    {
        _instance = this;

        DontDestroyOnLoad (gameObject);
    }

    // Start is called before the first frame update
    void Start ()
    {

        Advertisement.AddListener (this);
        Advertisement.Initialize (googlePay_ID, testMode);
        Advertisement.Initialize (appStore_ID, testMode);

        Advertisement.Load ("rewardedVideo");
    }

    public void DisplayInterstitialAD ()
    {
        Advertisement.Show ();
    }

    public void ShowInterstitialAd ()
    {
        Debug.Log ("Showing interstatial");
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady ())
        {
            Debug.Log ("Is ready");

            Advertisement.Show ();
        }
        else
        {
            Debug.Log ("Interstitial ad not ready at the moment! Please try again later!");
        }
    }

    public void ShowRewardedVideo ()
    {
        Debug.Log ("Showing rewarded video");

        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady (myPlacementId))
        {
            Debug.Log ("Is ready");

            // GameplayAllRefs._instance.m_reviveScreenTimer.StopRunTimerRoutine ();
            Advertisement.Show (myPlacementId);
        }
        else
        {
            Debug.Log ("Rewarded video is not ready at the moment! Please try again later!");
        }
    }

    public bool IsRewardedVideoReady ()
    {
        return Advertisement.IsReady (myPlacementId);
    }

    public void DisplayVideoAD ()
    {
        Advertisement.Show (myPlacementId);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish (string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished && placementId == "rewardedVideo")
        {
            // GameplayAllRefs._instance.m_levelController.OnRevive ();
            // Player.instance.OnAdWatched (1f);
            Advertisement.Load ("rewardedVideo");
            Debug.Log ("You get a Reward!!");
            // Reward the user for watching the ad to completio
        }
        else if (showResult == ShowResult.Skipped)
        {

            //Display end

            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning ("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsReady (string placementId)
    {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == myPlacementId)
        {
            // Optional actions to take when the placement becomes ready(For example, enable the rewarded ads button)
        }
    }

    public void OnUnityAdsDidError (string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart (string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    public void AcceptGDPR ()
    {
        MetaData gdprMetaData = new MetaData ("gdpr");
        gdprMetaData.Set ("consent", "true");
        Advertisement.SetMetaData (gdprMetaData);
    }

    public void DeclineGDPR ()
    {
        MetaData gdprMetaData = new MetaData ("gdpr");
        gdprMetaData.Set ("consent", "false");
        Advertisement.SetMetaData (gdprMetaData);

    }

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy ()
    {
        Advertisement.RemoveListener (this);
    }
}