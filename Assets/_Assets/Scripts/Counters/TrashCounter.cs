using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyObjectTrashed;

    new public static void ResetStaticData()
    {
        OnAnyObjectTrashed = null;
    }
    public override void Interact(Player player)
    {
        base.Interact(player);
        if (!GameManager.Instance.IsShopping())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().DestroySelf();

                OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

