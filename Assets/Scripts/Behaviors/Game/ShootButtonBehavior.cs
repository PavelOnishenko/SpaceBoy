using UnityEngine;

public class ShootButtonBehavior : MonoBehaviour
{
    [SerializeField] private Animator protagonistAnimator;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
                CheckInputAction(touch.position);
        }

        if (Input.GetMouseButtonDown(0))
            CheckInputAction(Input.mousePosition);
    }

    private void CheckInputAction(Vector2 position)
    {
        var worldPosition = Camera.main.ScreenToWorldPoint(position);

        var hit = Physics2D.Raycast(worldPosition, Vector2.zero);
        if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            ProcessInputAction();
    }

    private void ProcessInputAction()
    {
        Debug.Log(gameObject.name + " was tapped or clicked.");
        protagonistAnimator.SetTrigger("Aim");
    }
}
