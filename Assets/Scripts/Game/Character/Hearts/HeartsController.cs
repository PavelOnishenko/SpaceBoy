using System.Collections.Generic;
using UnityEngine;

public class HeartsController : MonoBehaviour
{
    [SerializeField] private int maxHp;
    [SerializeField] private GameObject emptyHeartPrefab;
    [SerializeField] private GameObject filledHeartPrefab;
    [SerializeField] private float heartDistance = 5f;
    [SerializeField] private float rowHeight = 10f;  // Distance between rows
    [SerializeField] private Rect heartArea;  // Defines the rectangular area for hearts
    [SerializeField] private CharacterState characterState; 

    private List<(GameObject empty, GameObject filled)> hearts = new List<(GameObject empty, GameObject filled)>();
    private CharacterDependentFeatures characterDependentFeatures;

    private void Start()
    {
        var characterDependentTransform = IntersceneState.GetCharacterDependentTransform(characterState.transform, characterState.isProtagonist);
        characterDependentFeatures = characterDependentTransform.GetComponent<CharacterDependentFeatures>();
        maxHp = characterDependentFeatures.InitialHp;
        CreateHearts();
        SetHp(maxHp);
    }

    private void CreateHearts()
    {
        int heartsPerRow = Mathf.FloorToInt(heartArea.width / heartDistance);  // Calculate how many hearts fit per row
        for (int i = 0; i < maxHp; i++)
        {
            int row = i / heartsPerRow;  // Determine the current row
            int column = i % heartsPerRow;  // Determine the position in the row
            Vector3 position = CalculateHeartPosition(row, column);

            GameObject empty = Instantiate(emptyHeartPrefab, transform);
            empty.GetComponent<RectTransform>().anchoredPosition = position;

            GameObject filled = Instantiate(filledHeartPrefab, transform);
            filled.GetComponent<RectTransform>().anchoredPosition = position;

            hearts.Add((empty, filled));
        }
    }

    private Vector3 CalculateHeartPosition(int row, int column)
    {
        // Start at the top-left corner of the heartArea and arrange hearts within it
        float x = heartArea.xMin + column * heartDistance;
        float y = heartArea.yMax - row * rowHeight;  // Move downwards as row increases
        return new Vector3(x, y, 0);
    }

    public void SetHp(int hp)
    {
        for (int i = 0; i < maxHp; i++)
        {
            if (i < hp)
            {
                hearts[i].empty.SetActive(false);
                hearts[i].filled.SetActive(true);
            }
            else
            {
                hearts[i].empty.SetActive(true);
                hearts[i].filled.SetActive(false);
            }
        }
    }
}