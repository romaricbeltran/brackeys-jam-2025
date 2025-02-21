using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    public GameObject worldPrefab;
    private GameObject currentWorld;

    void Start()
    {
        LoadWorld();
    }

    private void OnEnable()
    {
        Broadcaster.OnGameOver -= RestartWorld;
        Broadcaster.OnGameOver += RestartWorld;
    }

    private void OnDisable()
    {
        Broadcaster.OnGameOver -= RestartWorld;
    }

    private void RestartWorld(GameOverPayLoad gameOverPayLoad)
    {
        Debug.Log($"RestartWorld callback to OnGameOver", this);

        //Destroy the current world then instantiate a new one
        LoadWorld();

        //Find every object with IResettable then reset them
        List<IResettable> resettableObjects = ResettableRegistry.GetAll();
        foreach (var obj in resettableObjects)
        {
            obj.ResetState();
        }
    }

    public void LoadWorld()
    {
        DestroyCurrentWorld();

        currentWorld = Instantiate(worldPrefab);
    }
    
    private void DestroyCurrentWorld()
    {
        if (currentWorld != null)
        {
            Destroy(currentWorld);
        }
    }
}





