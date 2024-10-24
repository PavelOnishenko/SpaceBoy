using System.Collections.Generic;
using UnityEngine;

public class HeartsController : MonoBehaviour
{
    [SerializeField] private int maxHp;
    [SerializeField] private GameObject emptyHeartPrefab;
    [SerializeField] private GameObject filledHeartPrefab;
    [SerializeField] private float heartDistance = 5f;
    [SerializeField] private CharacterState characterState; 
    [SerializeField] private bool isProtagonist; 

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
        for (int i = 0; i < maxHp; i++)
        {
            var factor = isProtagonist ? 1 : -1;
            var position = new Vector3(factor * i * heartDistance, 0);
            var empty = Instantiate(emptyHeartPrefab, transform);
            empty.GetComponent<RectTransform>().anchoredPosition = position;
            var filled = Instantiate(filledHeartPrefab, transform);
            filled.GetComponent<RectTransform>().anchoredPosition = position;
            hearts.Add((empty, filled));
        }
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