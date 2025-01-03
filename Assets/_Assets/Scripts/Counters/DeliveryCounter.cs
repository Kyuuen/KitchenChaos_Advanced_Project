using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public override void Start()
    {
        base.Start();
    }
    public override void Interact(Player player)
    {
        base.Interact(player);
        if (!GameManager.Instance.IsShopping())
        {
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                    player.GetKitchenObject().DestroySelf();
                }
            }
        }
    }
}
