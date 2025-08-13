using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    public float speed = 5f;
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Stop()
    {
        animator.SetBool("IsMoving", false);
    }

    public void Move(Vector3 direction, float deltaTime)
    {
        if (direction == Vector3.zero)
        {
            Stop();
            return;
        }
        animator.SetBool("IsMoving", true);
        Vector3 next = transform.position + direction * speed * deltaTime;

        Debug.DrawLine(transform.position, transform.position + direction * speed, Color.red);

        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        if (NavMesh.SamplePosition(next, out var hit, 1.0f, NavMesh.AllAreas))
            transform.position = hit.position;
    }
}
