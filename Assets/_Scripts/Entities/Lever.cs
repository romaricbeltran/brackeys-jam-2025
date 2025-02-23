using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private List<Door> _doors = new List<Door>();
    [SerializeField] private bool _areDoorsOpen = false;

    private SpriteRenderer _leverSpriteRenderer;

    private void Awake()
    {
        _leverSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void ToggleDoors()
    {
        if (_doors.Count > 0)
        {
            Broadcaster.TriggerOnShortAudioRequest(AudioClipType.ActivateLever);
        }
        
        _areDoorsOpen = !_areDoorsOpen;
        
        _leverSpriteRenderer.flipX = _areDoorsOpen;
        
        foreach (var door in _doors)
        {
            door.GetComponent<Animator>().SetBool("isOpen", _areDoorsOpen);
        }
    }
}
