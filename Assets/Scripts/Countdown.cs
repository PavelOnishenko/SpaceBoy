using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField] private int countdownTime = 3;
    [SerializeField] private GameObject countdownContainer;
    
    public void Restart() => StartCoroutine(CountdownCoroutine());

    private void Start() => StartCoroutine(CountdownCoroutine());

    private IEnumerator CountdownCoroutine()
    {
        var digits = countdownContainer.GetComponentsInChildren<Transform>(true).Select(x => x.gameObject)
            .Where(x => x.name.Contains("Label")).ToArray();

        
        while (countdownTime > 0)
        {
            GameObject[] activeDigits = digits.Where(x => x.activeSelf).ToArray();
            foreach (var t in activeDigits) t.gameObject.SetActive(false);

            var digitToShowName = countdownTime switch { 3 => "LabelThree", 2 => "LabelTwo", 1 => "LabelOne", _ => null };
            var digitToShow = digits.Single(x => x.name == digitToShowName);
            digitToShow.SetActive(true);
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        digits.Single(x => x.name == "LabelOne").SetActive(false);
        digits.Single(x => x.name == "LabelGo").SetActive(true);
        yield return new WaitForSeconds(1f);
        countdownContainer.SetActive(false);
        GameInfo.Instance.State = GameState.Ongoing;
    }
}
