using UnityEngine;

public class Turner : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        float rotationAmount = rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward, rotationAmount);
    }
}
