using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    public GameObject worldPrefab;
    private GameObject currentWorld;

    private void OnEnable()
    {
        Broadcaster.OnGameStart -= RestartWorld;
        Broadcaster.OnGameStart += RestartWorld;

        Broadcaster.OnGameOver -= DestroyCurrentWorld;
        Broadcaster.OnGameOver += DestroyCurrentWorld;

        Broadcaster.OnBackToMainPanel -= DestroyCurrentWorld;
        Broadcaster.OnBackToMainPanel += DestroyCurrentWorld;
    }

    private void OnDisable()
    {
        Broadcaster.OnGameStart -= RestartWorld;
        Broadcaster.OnGameOver -= DestroyCurrentWorld;
        Broadcaster.OnBackToMainPanel -= DestroyCurrentWorld;
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

    private void DestroyCurrentWorld(GameOverPayLoad _)
    {
        DestroyCurrentWorld();
    }
}





