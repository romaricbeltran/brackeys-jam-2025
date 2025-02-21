using UnityEngine;

public class FlowerPotion : MonoBehaviour
{
    [SerializeField] HealthComponent _healthComponent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _healthComponent.ChangeHealth(1);
        }
    }
}
