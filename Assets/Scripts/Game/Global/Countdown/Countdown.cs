using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField] private int initialCountdownTime = 3;

    private GameObject[] digits;
    private bool isCounting;
    private int countdownTime;

    public void Restart() => StartCoroutine(CountdownCoroutine());

    private void Start()
    {
        digits = GetComponentsInChildren<Transform>(true).Select(x => x.gameObject)
            .Where(x => x.name.Contains("Label")).ToArray();
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        if (isCounting) yield break;
        isCounting = true;
        countdownTime = initialCountdownTime;
        gameObject.SetActive(true);
        while (countdownTime > 0)
        {
            SetDigitsActive();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        SetActive(CountdownLabelType.LabelOne.ToString(), false);
        SetActive(CountdownLabelType.LabelGo.ToString(), true);
        yield return new WaitForSeconds(1f);
        SetActive(CountdownLabelType.LabelGo.ToString(), false);
        isCounting = false;
        GameInfo.Instance.State = GameState.Ongoing;
    }

    private void SetDigitsActive()
    {
        var activeDigits = digits.Where(x => x.activeSelf).ToArray();
        foreach (var t in activeDigits) t.gameObject.SetActive(false);
        var gameObjectName = countdownTime switch 
        { 
            3 => CountdownLabelType.LabelThree.ToString(), 2 => CountdownLabelType.LabelTwo.ToString(), 
            1 => CountdownLabelType.LabelOne.ToString(), _ => null 
        };
        SetActive(gameObjectName, true);
    }

    private void SetActive(string gameObjectName, bool newValue) =>
        digits.Single(x => x.name == gameObjectName).SetActive(newValue);
}