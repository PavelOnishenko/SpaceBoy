using System.Collections;
using System.Linq;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField] private int countdownTime = 3;
    [SerializeField] private GameObject countdownContainer;

    private GameObject[] digits;
    
    public void Restart() => StartCoroutine(CountdownCoroutine());

    private void Start()
    {
        digits = countdownContainer.GetComponentsInChildren<Transform>(true).Select(x => x.gameObject)
            .Where(x => x.name.Contains("Label")).ToArray();
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        countdownTime = 3;
        countdownContainer.SetActive(true);
        while (countdownTime > 0)
        {
            var activeDigits = digits.Where(x => x.activeSelf).ToArray();
            foreach (var t in activeDigits) t.gameObject.SetActive(false);
            SetActive(countdownTime switch { 3 => "LabelThree", 2 => "LabelTwo", 1 => "LabelOne", _ => null }, true);
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        SetActive("LabelOne", false);
        SetActive("LabelGo", true);
        yield return new WaitForSeconds(1f);
        countdownContainer.SetActive(false);
        GameInfo.Instance.State = GameState.Ongoing;
    }

    private void SetActive(string gameObjectName, bool newValue) => 
        digits.Single(x => x.name == gameObjectName).SetActive(newValue);
}
