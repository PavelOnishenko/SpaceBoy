using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjustment : MonoBehaviour
{
    void Start() {
        float targetAspect = 15f / 9f; // Desired aspect ratio
        float windowAspect = (float)Screen.width / (float)Screen.height;
        Debug.Log($"windowAspect = [{windowAspect}].");
        float scaleHeight = windowAspect / targetAspect;
        Debug.Log($"scaleHeight = [{scaleHeight}].");
        Camera cam = GetComponent<Camera>();

        if (scaleHeight < 1.0f) {  
            Debug.Log("LESS THAN 1.");
            Rect rect = cam.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            Debug.Log($"rect = [{rect}].");
            cam.rect = rect;
        } else {
            Debug.Log("GREATER THAN OR EQUAL TO 1.");
            float scalewidth = 1.0f / scaleHeight;
            Rect rect = cam.rect;
            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;
            Debug.Log($"rect = [{rect}].");
            cam.rect = rect;
        }
    }
}
