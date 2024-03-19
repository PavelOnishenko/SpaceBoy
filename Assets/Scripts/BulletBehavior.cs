using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 15f;
    [SerializeField] private LayerMask whatDestroysBullet;
    
    private Rigidbody2D rigidbody2D;
    
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = transform.up * movementSpeed;
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var theObstacle = other.gameObject;
        if ((whatDestroysBullet.value & (1 << theObstacle.layer)) > 0) Destroy(gameObject);
        if (theObstacle.name == "Target") Destroy(theObstacle);
    }
}
