using System;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    public GameObject worldPrefab;
    private GameObject currentWorld;
    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] private HealthUI _healthUI;
    //public Player player; // Assign  player in the Inspector

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameOverPayLoad gameOverPayLoad = new GameOverPayLoad();
            Broadcaster.TriggerGameOver(gameOverPayLoad);
        }
    }

    private void RestartWorld(GameOverPayLoad gameOverPayLoad)
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

        RestartCarrierHealth();

    }

    private void LoadWorld()
    {
        currentWorld = Instantiate(worldPrefab);
    }

    private void RestartCarrierHealth()
    {
        //This one needs to access the health inside HealthComponent so that it can change the health to default value
        //_healthComponent._health = _healthComponent.MaxHealth;
        _healthUI.UpdateHealthUI(_healthComponent);
        
    }
}





