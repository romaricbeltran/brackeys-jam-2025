using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public Action<Collision2D> OnCollisionHappened;

    [SerializeField] private LayerMask _layerMask;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the collided GO's layer is present in this layer mask
        if (((1 << collision.gameObject.layer) & _layerMask) != 0)
        {
            // Debug.Log($"CollisionEnter - {gameObject.name} with {collision.gameObject.name}", this);
         
            OnCollisionHappened?.Invoke(collision);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Debug.Log($"CollisionStay - {gameObject.name} with {collision.gameObject.name}", this);

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Debug.Log($"CollisionExit - {gameObject.name} with {collision.gameObject.name}", this);

    }
}