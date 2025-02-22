using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public static Action<HealthComponent> OnHealthChanged;

    [SerializeField] private int _health = 10;
    public int Health => _health;
    public int MaxHealth => 5;
    public bool IsDead => _health <= 0;

    // value may be negative or positive
    public void ChangeHealth(int value)
    {
        if(value < 0)
        {
            Broadcaster.TriggerOnAudioRequest(AudioClipType.CarrierHit);
        }
        else
        {
            Broadcaster.TriggerOnAudioRequest(AudioClipType.BringFlowerToCarrier);
        }

        _health += value;

        OnHealthChanged?.Invoke(this);

        if(_health <= 0)
        {
            Broadcaster.TriggerOnAudioRequest(AudioClipType.CarrierDies);
            gameObject.SetActive(false);
        }
    }


}
