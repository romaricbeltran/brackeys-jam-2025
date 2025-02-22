using UnityEngine;

public class FlowerPotion : MonoBehaviour
{
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
        
        //collide with flower, health +1, parent == null, playercollided = true, destroyself
    }
}
