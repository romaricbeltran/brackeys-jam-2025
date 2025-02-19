using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public static Action<HealthComponent> OnHealthChanged;
    public bool IsDead => _health < 0;

    [SerializeField] private int _health = 10;

    // value may be negative or positive
    public void ChangeHealth(int value)
    {
        _health += value;

        OnHealthChanged?.Invoke(this);
    }
}
