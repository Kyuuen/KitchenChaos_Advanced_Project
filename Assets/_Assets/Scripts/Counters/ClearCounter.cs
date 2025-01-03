using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        base.Interact(player);
        if (!GameManager.Instance.IsShopping())
        {
            if (!HasKitchenObject())
            {   // This counter does not have a kitchen object
                if (player.HasKitchenObject())
                {   // Player has a kitchen object
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
                else
                {
                    // Player does not have a kitchen object
                }
            }
            else
            {
                if (player.HasKitchenObject())
                {
                    // Player has a kitchen object
                    if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {
                        //Player holding a plate
                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        {
                            GetKitchenObject().DestroySelf();
                        }
                    }
                    else
                    {
                        if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                        {
                            // Counter holding a plate
                            if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                            {
                                player.GetKitchenObject().DestroySelf();
                            }
                        }
                    }
                }
                else
                {
                    // Player does not have a kitchen object
                    GetKitchenObject().SetKitchenObjectParent(player);
                }
            }
        }

    }
}

