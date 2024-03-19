using UnityEngine;

public class Restart : MonoBehaviour
{
    [SerializeField] private GameObject targetPrefab;

    private GameObject currentTarget;
    private static Vector2 targetCoords;

    void Start()
    {
        currentTarget = GameObject.Find("Target");
        targetCoords = currentTarget.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(currentTarget);
            currentTarget = Instantiate(targetPrefab, targetCoords, Quaternion.identity);
            currentTarget.name = "Target";
        }
    }
}