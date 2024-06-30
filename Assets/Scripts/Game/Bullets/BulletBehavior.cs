using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1000f;
    [SerializeField] private float destroyTime = 3f;

    private Rigidbody2D rigidbody2DComponent;

    void Start()
    {
        rigidbody2DComponent = GetComponent<Rigidbody2D>();
        rigidbody2DComponent.velocity = transform.up * movementSpeed;
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var go = other.gameObject;
        if (go.IsParentWithName("ProtagonistContainer"))
            go.GetComponentFromParentByName<CharacterState>(IntersceneState.Instance.SelectedProtagonist.ToString()).GetHit();
        else if (go.IsParentWithName("EnemyContainer"))
            go.GetComponentFromParentByName<CharacterState>(IntersceneState.Instance.SelectedEnemy.ToString()).GetHit();
        Destroy(gameObject);
    }





    #region FOR EDITOR

    public void ApplyParameters()
    {
        var gameParameters = GameParametersManager.Instance.gameParameters;
        if (gameParameters != null)
        {
            if(gameObject.name.StartsWith("Human")) movementSpeed = gameParameters.humanBulletSpeed;
            else if (gameObject.name.StartsWith("Ai")) movementSpeed = gameParameters.aiBulletSpeed;
            destroyTime = gameParameters.bulletDestructionTime;
        }
    }

    #endregion
}