using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public static Action<HealthComponent> OnHealthChanged;

    [SerializeField] private int _health = 10;
    [SerializeField] private int _maxHealth = 5;
    
    public int Health => _health;
    public int MaxHealth => 5;
    public bool IsDead => _health <= 0;

    // value may be negative or positive
    public void ChangeHealth(int value)
    {
        if (value < 0)
        {
            if (gameObject.CompareTag("Carrier"))
            {
                Debug.Log("HIT CARRIER");
                Broadcaster.TriggerOnShortAudioRequest(AudioClipType.CarrierHit);
            }
            else if (gameObject.CompareTag("Player"))
            {
                Debug.Log("HIT PLAYER");
                Broadcaster.TriggerOnShortAudioRequest(AudioClipType.PlayerHit);
            }
        }
        else
        {
            Broadcaster.TriggerOnShortAudioRequest(AudioClipType.BringFlowerToCarrier);
        }

        _health = Mathf.Clamp(_health + value, 0, _maxHealth);

        OnHealthChanged?.Invoke(this);

        if (_health <= 0)
        {
            Broadcaster.TriggerOnShortAudioRequest(AudioClipType.CarrierDies);
            gameObject.SetActive(false);
        }
    }
}