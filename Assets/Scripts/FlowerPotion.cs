using UnityEngine;

public class FlowerPotion : MonoBehaviour
{
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
}
