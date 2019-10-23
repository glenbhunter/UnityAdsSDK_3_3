using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] bool testMode = false;

    private string gameId;

    [SerializeField] Button rewardedAdBTN;
    [SerializeField] Button videoAdBTN;

    private const string rewardedAdPlacementID = "rewardedVideo";
    private const string videoAdPlacementID = "video";

    private void Start()
    {
        Debug.Log("Advertisement Version: " + Advertisement.version);

#if UNITY_ANDROID
        gameId = "3118890";
#endif

#if UNITY_IOS
        gameId = "3118891";
#endif

        if(string.IsNullOrEmpty(gameId))
        {
            Debug.LogError("Unable to intialise Unity Ads - gameId does not exist, please ensure you are on the correct platform and have a valid gameId");
            return;
        }

        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
    }

    public void OnClick_ShowRewardedAd()
    {
        if (Advertisement.IsReady(rewardedAdPlacementID))
            Advertisement.Show(rewardedAdPlacementID);
        else
            Debug.Log(rewardedAdPlacementID + " placement, is not ready");
    }

    public void OnClick_ShowVideo()
    {
        if (Advertisement.IsReady(videoAdPlacementID))
            Advertisement.Show(videoAdPlacementID);
        else
            Debug.Log(videoAdPlacementID + " placement, is not ready");
    }

    /// <summary>
    /// Get notified when Ad is successfully cached and ready to play
    /// </summary>
    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("PlacementID: " + placementId + " is ready");

        if(placementId == rewardedAdPlacementID)
        {
            rewardedAdBTN.interactable = true;
        }

        if(placementId == videoAdPlacementID)
        {
            videoAdBTN.interactable = true;
        }
    }

    /// <summary>
    /// Get notified - if there was an error with the ad - before or during
    /// </summary>
    /// <param name="message"></param>
    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError("UHOH! Something when wrong with Unity Ad, error: " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
        Debug.Log("Unity ad/placement: " + placementId + " started");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Optional actions to take when the ad finished, was skipped or failed
        switch (showResult)
        {
            case ShowResult.Finished:
                // do something
                break;
            case ShowResult.Skipped:
                // do something
                break;
            case ShowResult.Failed:
                // do something
                Debug.LogError("OnUnityAdsDidFinish ShowResult.Failed something went wrong");
                break;
        }
    }
}
