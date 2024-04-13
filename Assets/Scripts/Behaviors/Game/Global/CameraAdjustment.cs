using UnityEngine;

public class CameraAdjustment : MonoBehaviour
{
    [SerializeField] private float targetWidthFactor = 16f;
    [SerializeField] private float targetHeightFactor = 9f;
    
    void Start() {
        var targetAspect = targetWidthFactor / targetHeightFactor;
        var windowAspect = Screen.width / (float)Screen.height;
        var scaleHeight = windowAspect / targetAspect;
        var cam = GetComponent<Camera>();

        if (scaleHeight < 1.0f) {  
            var rect = cam.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            cam.rect = rect;
        } else {
            var scaleWidth = 1.0f / scaleHeight;
            var rect = cam.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            cam.rect = rect;
        }
    }
}
