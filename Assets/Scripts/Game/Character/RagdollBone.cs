using UnityEngine;

public class RagdollBone : MonoBehaviour
{
    [SerializeField] private float magnitudeThreshold = 2f;

    private Transform parentBone;
    private Vector3 previousPosition;
    private Quaternion previousRotation;

    void Start()
    {
        parentBone = transform.parent;
        if (parentBone != null)
        {
            previousPosition = transform.position;
            previousRotation = transform.rotation;
        }
    }

    void Update()
    {
        if (parentBone != null)
        {
            Vector3 positionDelta = transform.position - previousPosition;
            if (positionDelta.magnitude > magnitudeThreshold)
                return;

            transform.position -= positionDelta;
            parentBone.position += positionDelta;

            Quaternion rotationDelta = transform.rotation * Quaternion.Inverse(previousRotation);
            parentBone.rotation = rotationDelta * parentBone.rotation;
            transform.rotation = transform.rotation * Quaternion.Inverse(rotationDelta);

            previousPosition = transform.position;
            previousRotation = transform.rotation;
        }
    }
}