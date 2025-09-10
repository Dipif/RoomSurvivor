using UnityEngine;

[CreateAssetMenu(menuName = "DamageText/Style")]
public class FloatingTextStyle : ScriptableObject
{
    [Header("Lifetime")]
    public float lifetime = 0.8f;

    [Header("Curves (0..1)")]
    public AnimationCurve alpha = AnimationCurve.EaseInOut(0, 0, 1, 0);   // 예: 살짝 페이드인 후 페이드아웃
    public AnimationCurve scale = AnimationCurve.Linear(0, 1f, 1, 1f);    // 예: 튀었다가 줄어드는 것도 가능
    public AnimationCurve offsetX = AnimationCurve.Linear(0, 0, 1, 0.2f); // 단위: 미터(월드)
    public AnimationCurve offsetY = AnimationCurve.EaseInOut(0, 0, 1, 1f);

    [Header("Color Over Life")]
    public Gradient colorOverLife; // 크리티컬/히트 타입에 따라 다른 스타일 사용 권장

    [Header("Optional Bezier Path (local offsets)")]
    public bool useBezierPath = false;
    public Vector3 p0 = Vector3.zero;        // 시작(보통 (0,0,0))
    public Vector3 p1 = new Vector3(0.1f, 0.6f, 0f);
    public Vector3 p2 = new Vector3(-0.1f, 1.0f, 0f);
    public Vector3 p3 = new Vector3(0f, 1.2f, 0f);
    public AnimationCurve pathT = AnimationCurve.Linear(0, 0, 1, 1); // 진행률 리매핑

    public Vector3 EvaluatePath(float t)
    {
        if (!useBezierPath)
            return new Vector3(offsetX.Evaluate(t), offsetY.Evaluate(t), 0f);

        float u = Mathf.Clamp01(pathT.Evaluate(t));
        // Cubic Bezier
        return Mathf.Pow(1 - u, 3) * p0 + 3 * Mathf.Pow(1 - u, 2) * u * p1 + 3 * (1 - u) * u * u * p2 + u * u * u * p3;
    }
}