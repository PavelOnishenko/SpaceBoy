using System;
using System.Linq;
using UnityEngine;

public class HeartsController : MonoBehaviour
{
    [SerializeField] private HeartPlaceholder[] heartPlaceholders;

    private void OnValidate()
    {
        heartPlaceholders = GetComponentsInChildren<HeartPlaceholder>();
    }

    public void SetHp(int hp)
    {
        ProcessSomeHearts(heartPlaceholder => GetHeartNumber(heartPlaceholder) <= hp, true, false);
        ProcessSomeHearts(heartPlaceholder => GetHeartNumber(heartPlaceholder) > hp, false, true);
    }

    private int GetHeartNumber(HeartPlaceholder heartPlaceholder)
    {
        var digitsString = new string(
            heartPlaceholder.gameObject.name.Where(character => character > '0' && character < '9').ToArray());
        var parsed = int.Parse(digitsString);
        return parsed;
    }

    private void ProcessSomeHearts(Predicate<HeartPlaceholder> condition, bool setFilledActive, bool setEmptyActive)
    {
        var heartsToProcess = heartPlaceholders.Where(x => condition(x)).ToArray();
        foreach (var heartPlaceholder in heartsToProcess)
        {
            var heart = heartPlaceholder.gameObject;
            if (heart.name.Contains("Filled"))
                heart.SetActive(setFilledActive);
            else if (heart.name.Contains("Empty"))
                heart.SetActive(setEmptyActive);
        }
    }
}
