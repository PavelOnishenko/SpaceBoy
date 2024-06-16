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
        if (other.gameObject.IsParentWithName("ProtagonistContainer"))
        {
            other.gameObject.GetComponentFromParentByName<CharacterState>(
                IntersceneState.Instance.SelectedProtagonist.ToString()).GetHit(); // todo 3 times?
        }
        else if (other.gameObject.IsParentWithName("EnemyContainer"))
        {
            other.gameObject.GetComponentFromParentByName<CharacterState>(
                IntersceneState.enemyNameByLevel[IntersceneState.Instance.SelectedLevel].ToString()).GetHit(); // todo 3 times?
        }
        Destroy(gameObject);
    }
}