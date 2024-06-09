using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class HeartsController : MonoBehaviour
{
    [SerializeField] private HeartPlaceholder[] heartPlaceholders;

    private void OnValidate() => heartPlaceholders = GetComponentsInChildren<HeartPlaceholder>(true);

    private void Start() => SetHp(2);

    public void SetHp(int hp)
    {
        ProcessSomeHearts(heartPlaceholder => GetHeartNumber(heartPlaceholder) <= hp, true, false);
        ProcessSomeHearts(heartPlaceholder => GetHeartNumber(heartPlaceholder) > hp, false, true);
    }

    private int GetHeartNumber(HeartPlaceholder heartPlaceholder) => 
        int.Parse(Regex.Match(heartPlaceholder.gameObject.name, @"\d+").Value);

    private void ProcessSomeHearts(Predicate<HeartPlaceholder> condition, bool setFilledActive, bool setEmptyActive)
    {
        var heartsToProcess = heartPlaceholders.Where(x => condition(x)).ToArray();
        foreach (var heartPlaceholder in heartsToProcess)
        {
            var heart = heartPlaceholder.gameObject;
            if (heart.name.Contains("Filled")) heart.SetActive(setFilledActive);
            else if (heart.name.Contains("Empty")) heart.SetActive(setEmptyActive);
        }
    }
}