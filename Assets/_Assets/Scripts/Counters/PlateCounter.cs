using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private const float SPAWN_PLATE_TIMER_MAX = 4f;
    private float spawnPlateTimer;
    private int plateSpawnedAmount;
    private int plateSpawnedMax = 4;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if(spawnPlateTimer >= SPAWN_PLATE_TIMER_MAX)
        {
            spawnPlateTimer = 0;
            if(GameManager.Instance.IsGamePlaying() && plateSpawnedAmount < plateSpawnedMax)
            {
                plateSpawnedAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        base.Interact(player);
        if (!GameManager.Instance.IsShopping())
        {
            if (!player.HasKitchenObject())
            {
                if (plateSpawnedAmount > 0)
                {
                    plateSpawnedAmount--;

                    KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                    OnPlateRemoved?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
