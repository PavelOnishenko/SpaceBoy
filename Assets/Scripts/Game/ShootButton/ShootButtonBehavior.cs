using Assets.Scripts.Edtitor;
using UnityEngine;

public class ShootButtonBehavior : MonoBehaviour, IDesignerConfigurable
{
    [SerializeField] private float destroyTime = 3f;

    private CharacterState stateController;

    private void Start()
    {
        stateController = GameInfo.Instance.Protagonist.GetComponent<CharacterState>();
        Destroy(gameObject, destroyTime);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) CheckInputAction(touch.position);
        }

        if (Input.GetMouseButtonDown(0)) 
            CheckInputAction(Input.mousePosition);
    }

    private void CheckInputAction(Vector2 position)
    {
        var worldPosition = Camera.main.ScreenToWorldPoint(position);
        RaycastHit2D[] hits = Physics2D.RaycastAll(worldPosition, Vector2.zero);

        foreach (var hit in hits)
            if (hit.collider != null && hit.collider.gameObject.name == gameObject.name)
            {
                ProcessInputAction();
                break;
            }
    }

    private void ProcessInputAction()
    {
        stateController.Aim();
        Destroy(gameObject);
    }

    #region FOR EDITOR

    public void ApplyParameters()
    {
        var gameParameters = GameParametersManager.Instance.gameParameters;
        if (gameParameters != null) destroyTime = gameParameters.shootButtonLifetime;
    }

    #endregion

}