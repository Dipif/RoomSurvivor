using System.Collections.Generic;
using UnityEngine;

public class FloatingTextPool : MonoBehaviour
{
    [SerializeField] FloatingText prefab;
    [SerializeField] int initial = 16;
    [SerializeField] Camera worldCamera;

    Queue<FloatingText> pool = new Queue<FloatingText>();

    void Awake()
    {
        for (int i = 0; i < initial; i++) pool.Enqueue(Create());
    }

    FloatingText Create()
    {
        var inst = Instantiate(prefab, transform);
        inst.gameObject.SetActive(false);
        return inst;
    }

    public void ShowText(Vector3 worldPos, string text, FloatingTextType type)
    {
        if (pool.Count == 0) pool.Enqueue(Create());
        var style = GameManager.Instance.FloatingTextTheme.GetStyle(type);
        var dt = pool.Dequeue();
        dt.Play(text, worldPos, style, worldCamera, Return);
    }

    void Return(FloatingText dt)
    {
        dt.gameObject.SetActive(false);
        pool.Enqueue(dt);
    }
}
