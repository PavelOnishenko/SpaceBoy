using UnityEngine;

public class ShootButtonBehavior : MonoBehaviour
{
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
                CheckTouch(touch.position);
        }

        if (Input.GetMouseButtonDown(0))
            CheckTouch(Input.mousePosition);
    }

    private void CheckTouch(Vector2 position)
    {
        var worldPosition = Camera.main.ScreenToWorldPoint(position);

        var hit = Physics2D.Raycast(worldPosition, Vector2.zero);
        if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            Debug.Log(gameObject.name + " was tapped or clicked.");
    }
}
