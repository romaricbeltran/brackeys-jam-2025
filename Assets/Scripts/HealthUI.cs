using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private List<Image> _heartsList;
    [SerializeField] private Sprite _fullHeartSprite;
    [SerializeField] private Sprite _emptyHeartSprite;
    [SerializeField] private HealthComponent _healthComponent;
    private void Awake()
    {
        UpdateHealthUI(_healthComponent);
    }

    private void OnEnable()
    {
        HealthComponent.OnHealthChanged -= UpdateHealthUI;
        HealthComponent.OnHealthChanged += UpdateHealthUI;
    }

    private void OnDisable()
    {
        HealthComponent.OnHealthChanged -= UpdateHealthUI;
    }

    public void UpdateHealthUI(HealthComponent healthComponent)
    {
        if (healthComponent.Health > healthComponent.MaxHealth)
            return;
        foreach (Image img in _heartsList)
        {
            img.sprite = _emptyHeartSprite;
        }

        for (int i = 0; i < healthComponent.Health; i++)
        {
            _heartsList[i].sprite = _fullHeartSprite;
        }
    }
}