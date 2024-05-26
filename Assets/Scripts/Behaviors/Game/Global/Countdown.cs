using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;

// todo refactor this with either old or new approach
public class Countdown : MonoBehaviour
{
    [SerializeField] private int countdownTime = 3;

    private GameObject[] digits;
    private bool isCounting;
    private float timeStamp;

    //public void Restart() => StartCoroutine(CountdownCoroutine());
    public void Restart() => LaunchCountdown();

    private void Start()
    {
        digits = GetComponentsInChildren<Transform>(true).Select(x => x.gameObject)
            .Where(x => x.name.Contains("Label")).ToArray();
        //StartCoroutine(CountdownCoroutine());

        // todo this is 26 05 2024
        LaunchCountdown();
    }

    private void LaunchCountdown()
    {
        countdownTime = 3;
        timeStamp = Time.time;
        isCounting = true;
    }

    // todo this is 26 05 2024
    private void Update()
    {
        if (isCounting)
        {
            var currentValue = 3 - Mathf.FloorToInt(Time.time - timeStamp);
            if (countdownTime != currentValue)
            {
                countdownTime = currentValue;
                if (countdownTime == 0)
                {
                    SetActive("LabelOne", false);
                    SetActive("LabelGo", true);
                    GameInfo.Instance.State = GameState.Ongoing;
                }
                else if(countdownTime == -1)
                {
                    SetActive("LabelGo", false);
                    isCounting = false;
                }
                else
                {
                    SetDigitsActive();
                }
            }
        }
    }

    private void SetDigitsActive()
    {
        var activeDigits = digits.Where(x => x.activeSelf).ToArray();
        foreach (var digit in activeDigits) digit.SetActive(false);
        var labelName = countdownTime switch
        {
            3 => "LabelThree", 2 => "LabelTwo", 1 => "LabelOne", 0 => "LabelGo",
            _ => throw new System.Exception($"Can't convert number [{countdownTime}] to label name.")
        };
        SetActive(labelName, true);
    }

    private void SetActive(string gameObjectName, bool newValue) => 
        digits.Single(x => x.name == gameObjectName).SetActive(newValue);

    //private IEnumerator CountdownCoroutine()
    //{
    //    if (isCounting) yield break;
    //    isCounting = true;
    //    countdownTime = 3;
    //    gameObject.SetActive(true);
    //    while (countdownTime > 0)
    //    {
    //        SetDigitsActive();
    //        yield return new WaitForSeconds(1f);
    //        countdownTime--;
    //    }
    //    SetActive("LabelOne", false);
    //    SetActive("LabelGo", true);
    //    yield return new WaitForSeconds(1f);
    //    gameObject.SetActive(false);
    //    isCounting = false;
    //    GameInfo.Instance.State = GameState.Ongoing;
    //}
}
