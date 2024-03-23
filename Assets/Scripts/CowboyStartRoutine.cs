using UnityEngine;

public class CowboyStartRoutine : MonoBehaviour
{
    void Start()
    {
        var animator = GetComponent<Animator>();
        animator.Play("CowboyIdle");
    }
}
