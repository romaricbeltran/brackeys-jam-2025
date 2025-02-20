using System;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    public GameObject worldPrefab;
    private GameObject currentWorld;
    //public Player player; // Assign the player in the Inspector

    void Start()
    {
        LoadWorld();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            RestartWorld();
        }
    }

    public void RestartWorld()
    {
        //Destroy the current world then instantiate a new one
        if (currentWorld != null)
        {
            Destroy(currentWorld);
        }
        LoadWorld();
        //player.ResetPlayer(); // Reset player position
        
        //Find every object with IResettable then reset them
        List<IResettable> resettableObjects = ResettableRegistry.GetAll();
        foreach (var obj in resettableObjects)
        {
            obj.ResetState();
        }

    }

    private void LoadWorld()
    {
        currentWorld = Instantiate(worldPrefab);
    }
}





