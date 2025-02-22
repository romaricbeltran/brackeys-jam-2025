using UnityEngine;

public class FlowerPotion : MonoBehaviour
{

    #region Gino Code

    public void PickUpFLower()
    {
        var tempCarrierRef = GameObject.FindGameObjectWithTag("Carrier");

        if(tempCarrierRef != null)
        {
            var carrierHealthComponent = tempCarrierRef.GetComponent<HealthComponent>();

            if(carrierHealthComponent != null)
            {
                carrierHealthComponent.ChangeHealth(+1);
                Destroy(gameObject);
            }
        }
    }

    #endregion
    
   
    [SerializeField] HealthComponent _healthComponent;
    private bool _playerCollided;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !_playerCollided)
        {
            //put flower to the hold position
            //child to player
            GameObject flowerHolder = GameObject.FindWithTag("FlowerHolder");
            transform.parent = other.transform;
            transform.position = flowerHolder.transform.position;
            _playerCollided = true;
        }
        else if (other.gameObject.CompareTag("Carrier") && _playerCollided)
        {
            _healthComponent.ChangeHealth(1);
            Destroy(gameObject);
        }
    }

}
