using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private List<Image> _heartsList;
    [SerializeField] private Sprite _fullHeartSprite;
    [SerializeField] private Sprite _emptyHeartSprite;

    private void OnEnable()
    {
        HealthComponent.OnHealthChanged -= UpdateHealthUI;
        HealthComponent.OnHealthChanged += UpdateHealthUI;
    }

    private void OnDisable()
    {
        HealthComponent.OnHealthChanged -= UpdateHealthUI;
    }

    private void UpdateHealthUI(HealthComponent healthComponent)
    {
        if (healthComponent.health > maxHealth)
            return;
        foreach (Image img in _heartsList)
        {
            img.sprite = _emptyHeartSprite;
        }

        for (int i = 0; i < healthComponent.health; i++)
        {
            _heartsList[i].sprite = _fullHeartSprite;
        }
    }
}