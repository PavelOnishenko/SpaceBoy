using System.Collections;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField] private int countdownTime = 3;
    [SerializeField] private TextMeshProUGUI textMesh;

    private void Start()
    {
        StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        while (countdownTime > 0)
        {
            textMesh.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        textMesh.text = "Go!";

        yield return new WaitForSeconds(1f);
        textMesh.gameObject.SetActive(false);

        GameInfo.Instance.State = GameInfo.GameState.Ongoing;
    }
}
