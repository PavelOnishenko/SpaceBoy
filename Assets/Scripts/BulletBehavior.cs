using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 15f;
    
    private Rigidbody2D rigidbody2DComponent;
    
    void Start()
    {
        rigidbody2DComponent = GetComponent<Rigidbody2D>();
        rigidbody2DComponent.velocity = transform.up * movementSpeed;
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        var otherGameObject = other.gameObject;
        var animator = otherGameObject.GetComponent<Animator>();
        if (animator != null )
        {
            animator.SetTrigger("Die");
        }
        
    }

    private LayerMask ToLayerMask(int layer) => 1 << layer;
}
