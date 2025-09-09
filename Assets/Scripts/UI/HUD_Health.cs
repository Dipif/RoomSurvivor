using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Health : MonoBehaviour
{
    [SerializeField] CharacterStatus target;
    [SerializeField] GameObject healthImagePrefab;
    [SerializeField] GameObject root;
    List<GameObject> healthItems = new List<GameObject>();

    private void Start()
    {
        if (healthImagePrefab == null)
            Debug.LogError("Health Image is not assigned.");
        if (root == null)
            Debug.LogError("Root GameObject is not assigned.");
        target = GameManager.Instance.Player.GetComponent<CharacterStatus>();

        for (int i = 0; i < target.MaxHealth; i++)
        {
            var item = Instantiate(healthImagePrefab, root.transform);
            item.gameObject.SetActive(true);
            healthItems.Add(item.gameObject);
        }
    }

    void OnEnable()
    {
        if (target == null)
            target = FindAnyObjectByType<CharacterStatus>(); // 임시: 에디터에서 연결 권장

        if (target != null)
        {
            target.OnHealthChanged += HandleHealthChanged;
            target.ForceSync(); // 처음 켰을 때 즉시 반영
        }
    }

    void OnDisable()
    {
        if (target != null)
        {
            target.OnHealthChanged -= HandleHealthChanged;
        }
    }

    void HandleHealthChanged(float cur, float max)
    {
        for (int i = 0; i != healthItems.Count; i++)
        {
            if (i < cur)
                healthItems[i].SetActive(true);
            else
                healthItems[i].SetActive(false);
        }
    }
}
