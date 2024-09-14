using UnityEngine;
using UnityEngine.Advertisements;

public class AdDisplay : MonoBehaviour
{
    public string myGameIdAndroid = "5689591";
    public string adUnitIdAndroid = "Interstitial_Android";
    private bool testMode = true;

    void Start()
    {
        Advertisement.Initialize(myGameIdAndroid, testMode);
    }

    public void ShowAd()
    {
        Advertisement.Load(adUnitIdAndroid);
        Advertisement.Show(adUnitIdAndroid);
    }
}