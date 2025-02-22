using UnityEngine;

public class FlowerPotion : MonoBehaviour
{

    private bool _playerCollided;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Carrier") && _playerCollided)
        {
            var carrierHealthComponent = other.GetComponent<HealthComponent>();

            if(carrierHealthComponent != null)
            {
                carrierHealthComponent.ChangeHealth(+1);
                Destroy(gameObject);
            }
        }
        else if (other.gameObject.CompareTag("Player") && !_playerCollided)
        {
            //put flower to the hold position
            //child to player
            GameObject flowerHolder = GameObject.FindWithTag("FlowerHolder");
            transform.parent = flowerHolder.transform;
            transform.position = flowerHolder.transform.position;
            _playerCollided = true;

        }
    }

}
