using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;

    FloatingTextStyle style;
    System.Action<FloatingText> onComplete;
    float elasped;
    Vector3 origin;
    Camera _camera;

    public void Play(float value, Vector3 origin, FloatingTextStyle style, Camera camera, System.Action<FloatingText> onComplete)
    {
        textMeshPro.text = value.ToString();
        this.style = style;
        this.onComplete = onComplete;
        this.origin = origin;
        _camera = camera;
        elasped = 0f;

        transform.position = origin;
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (elasped >= style.lifetime)
        {
            onComplete?.Invoke(this);
            return;
        }
        elasped += Time.deltaTime;

        float t = elasped / style.lifetime;
        Color color = style.colorOverLife.Evaluate(t);
        color.a *= style.alpha.Evaluate(t);
        textMeshPro.color = color;

        Vector3 worldPosition = origin + style.EvaluatePath(t);
        Vector3 screenPosition = RectTransformUtility.WorldToScreenPoint(_camera, worldPosition);
        transform.position = screenPosition;
        float scale = style.scale.Evaluate(t);
        //if (_camera != null)
        //    transform.rotation = Quaternion.LookRotation(transform.position - _camera.transform.position);
    }
}
