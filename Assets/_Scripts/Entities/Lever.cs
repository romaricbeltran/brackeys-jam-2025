using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private List<Door> _doors = new List<Door>();
    [SerializeField] private bool _areDoorsEnabled = true;

    private void Start()
    {
        foreach (var door in _doors)
        {
            door.gameObject.SetActive(_areDoorsEnabled);
        }
    }

    public void ToggleDoors()
    {
        if(_doors.Count > 0)
        {
            Broadcaster.TriggerOnShortAudioRequest(AudioClipType.ActivateLever);
        }

        foreach(var door in _doors)
        {
            door.gameObject.SetActive(!door.gameObject.activeSelf);
        }
    }
}
