using UnityEngine;
using UnityEngine.InputSystem;

public class HandBehavior : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private Transform joint;
    
    Vector2 rotationInput = Vector2.zero;
    
    void FixedUpdate()
    {
        transform.RotateAround(joint.position, Vector3.forward, -rotationInput.y * rotationSpeed);
    }

    private void OnMove(InputValue value)
    {
        rotationInput = value.Get<Vector2>();
    }
}
