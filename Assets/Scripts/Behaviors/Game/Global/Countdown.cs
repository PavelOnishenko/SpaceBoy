using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField] private int countdownTime = 3;

    private GameObject[] digits;
    private bool isCounting;

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
        countdownTime = 3;
        gameObject.SetActive(true);
        while (countdownTime > 0)
        {
            SetDigitsActive();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        SetActive("LabelOne", false);
        SetActive("LabelGo", true);
        yield return new WaitForSeconds(1f);
        SetActive("LabelGo", false);
        isCounting = false;
        GameInfo.Instance.State = GameState.Ongoing;
    }


    private void SetDigitsActive()
    {
        var activeDigits = digits.Where(x => x.activeSelf).ToArray();
        foreach (var t in activeDigits) t.gameObject.SetActive(false);
        SetActive(countdownTime switch { 3 => "LabelThree", 2 => "LabelTwo", 1 => "LabelOne", _ => null }, true);
    }

    private void SetActive(string gameObjectName, bool newValue) =>
        digits.Single(x => x.name == gameObjectName).SetActive(newValue);
}