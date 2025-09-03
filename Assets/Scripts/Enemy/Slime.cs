using UnityEngine;

public class Slime : Enemy
{
    public override void Init()
    {
        base.Init();     // 공통 초기화
        // Slime 고유 초기화
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
