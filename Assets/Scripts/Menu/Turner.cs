using UnityEngine;

public class Turner : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update() => transform.Rotate(Vector3.forward, (float)(rotationSpeed * Time.deltaTime));
}