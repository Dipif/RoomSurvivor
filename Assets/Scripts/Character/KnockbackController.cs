using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class KnockbackController : MonoBehaviour
{
    [SerializeField] float sampleRadius = 1.0f;
    [SerializeField] LayerMask groundMask = ~0;
    [SerializeField] MonoBehaviour hasAbilitySource;
    IHasAbility hasAbility;

    Coroutine running;
    public bool InKnockback { get; private set; }

    void Start()
    {
        if (hasAbility == null)
            hasAbility = hasAbilitySource as IHasAbility;
        if (hasAbility == null)
            Debug.LogError("IHasAbility is not assigned in KnockbackController");
    }

    public void ApplyFromSource(Vector3 sourcePos, float distance, float duration)
    {
        Vector3 dir = transform.position - sourcePos;
        dir.y = 0f;
        if (dir.sqrMagnitude < 1e-6f) dir = -transform.forward; 
        ApplyDirection(dir.normalized, distance, duration);
    }

    // 2) 방향 벡터로 직접 호출
    public void ApplyDirection(Vector3 dir, float distance, float duration)
    {
        if (running != null) StopCoroutine(running);
        running = StartCoroutine(Knockback(dir, distance, duration));
    }

    IEnumerator Knockback(Vector3 dir, float distance, float duration)
    {
        InKnockback = true;
        hasAbility.OnAbilityEvent("KnockbackStart");

        Vector3 start = transform.position;
        Vector3 target = start + dir.normalized * distance;

        // 벽/절벽 앞에서 막히면 그 지점까지만
        if (NavMesh.Raycast(start, target, out var hitRay, NavMesh.AllAreas))
            target = hitRay.position;

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float u = Mathf.Clamp01(t / duration);

            // 초반 빠르고 끝에 감쇠하는 easing
            float eased = 1f - Mathf.Pow(1f - u, 3f);
            Vector3 next = Vector3.Lerp(start, target, eased);

            // NavMesh 위로 스냅
            if (NavMesh.SamplePosition(next, out var hit, sampleRadius, NavMesh.AllAreas))
                transform.position = hit.position;
            else
                transform.position = next;

            yield return null;
        }

        // 마지막 보정
        if (NavMesh.SamplePosition(transform.position, out var finalHit, sampleRadius, NavMesh.AllAreas))
            transform.position = finalHit.position;

        hasAbility.OnAbilityEvent("KnockbackEnd");
        InKnockback = false;
    }
}