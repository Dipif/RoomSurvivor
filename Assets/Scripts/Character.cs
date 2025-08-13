using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class Character : MonoBehaviour
{
    public float speed = 5f;
    Animator animator;

    bool isMoveLock = false;
    public float moveLockTime = 0.5f; // Time to lock movement after an attack
    float moveLockTimer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoveLock)
        {
            moveLockTimer += Time.deltaTime;
            if (moveLockTimer >= moveLockTime)
            {
                isMoveLock = false;
                moveLockTimer = 0f;
            }
        }
    }

    public void Attack()
    {
        Stop();
        isMoveLock = true;
        animator.Play("Attack");
    }
    public void Stop()
    {
        animator.SetBool("IsMoving", false);
    }

    public void Move(Vector3 direction, float deltaTime)
    {
        if (isMoveLock)
        {
            return;
        }
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
