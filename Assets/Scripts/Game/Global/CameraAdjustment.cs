using UnityEngine;

public class CameraAdjustment : MonoBehaviour
{
    [SerializeField] private float targetWidthFactor = 16f;
    [SerializeField] private float targetHeightFactor = 9f;
    
    void Start() {
        var targetAspect = targetWidthFactor / targetHeightFactor;
        var windowAspect = Screen.width / (float)Screen.height;
        var scaleHeight = windowAspect / targetAspect;
        var camera = GetComponent<Camera>();
        if (scaleHeight < 1.0f) 
        {
            SetCameraRect(camera, 1.0f, scaleHeight, 0, (1.0f - scaleHeight) / 2.0f);
        } 
        else 
        {
            var scaleWidth = 1.0f / scaleHeight;
            SetCameraRect(camera, scaleWidth, scaleHeight, (1.0f - scaleWidth) / 2.0f, 0);
        }
    }

    void SetCameraRect(Camera cam, float scaleWidth, float scaleHeight, float x, float y)
    {
        var rect = cam.rect;
        rect.width = scaleWidth;
        rect.height = scaleHeight;
        rect.x = x;
        rect.y = y;
        cam.rect = rect;
    }
}