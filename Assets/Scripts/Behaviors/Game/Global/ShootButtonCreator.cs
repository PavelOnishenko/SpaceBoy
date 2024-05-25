using System.Linq;
using UnityEngine;

public class ShootingButtonCreator : MonoBehaviour
{
    [SerializeField] private GameObject shootButtonPrefab;
    [SerializeField] private GameObject shootButtonPositionsContainer;

    private Transform[] shootButtonPositions;

    private void Start()
    {
        shootButtonPositions = GetComponentsInChildren<Transform>().ToArray();
    }

    public void CreateButton()
    {
        var choice = Random.Range(0, 9);
        var buttonSpawnPoint = shootButtonPositions[choice];
        Instantiate(shootButtonPrefab, buttonSpawnPoint.position, Quaternion.identity);
    }
}
