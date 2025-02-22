using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    [SerializeField] private Image _healthImagePrefab;
    [SerializeField] private Sprite _fullHearthSprite;
    [SerializeField] private Sprite _emptyHearthSprite;
    [SerializeField] private Transform _imagesParent;

    private List<Image> _currentHearts = new List<Image>();

    private void OnEnable()
    {
        HealthComponent.OnHealthChanged -= UpdateHealthView;
        HealthComponent.OnHealthChanged += UpdateHealthView;

        var tempoGO = GameObject.FindGameObjectWithTag("Carrier");
        var carrierHealthComponent = tempoGO.GetComponent<HealthComponent>();
        UpdateHealthView(carrierHealthComponent);
    }

    private void OnDisable()
    {
        HealthComponent.OnHealthChanged -= UpdateHealthView;
    }

    private void UpdateHealthView(HealthComponent healthComponent)
    {
        if(IsCarrier(healthComponent.transform))
        {
            HandleHearts(healthComponent);
        }
    }

    private void HandleHearts(HealthComponent healthComponent)
    {
        foreach(var heart in _currentHearts)
        {
            Destroy(heart.gameObject);
        }
        _currentHearts.Clear();

        // Full hearts
        for(int i = 0; i < healthComponent.Health; ++i)
        {
            var tempImage = Instantiate(_healthImagePrefab, _imagesParent);
            tempImage.sprite = _fullHearthSprite;
            _currentHearts.Add(tempImage);
        }

        // Empty hearts
        int startIndex = healthComponent.MaxHealth - healthComponent.Health;
        for(int i = startIndex; i > 0; --i)
        {
            var tempImage = Instantiate(_healthImagePrefab, _imagesParent);
            tempImage.sprite = _emptyHearthSprite;
            _currentHearts.Add(tempImage);
        }
    }

    private bool IsCarrier(Transform transform)
    {
        return transform.gameObject.CompareTag("Carrier");
    }

    private bool IsPlayer()
    {
        return transform.gameObject.CompareTag("Player");
    }
}
