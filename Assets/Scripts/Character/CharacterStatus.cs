using UnityEngine;

public class CharacterStatus : StatusBase
{
    public float CurrentHealth = 100f;
    public float MaxHealth = 100f;
    public float AttackDamage = 10f;
    public float Speed = 5f;
    public Vector3 MoveDirection { get; set; } = Vector3.zero;
}
