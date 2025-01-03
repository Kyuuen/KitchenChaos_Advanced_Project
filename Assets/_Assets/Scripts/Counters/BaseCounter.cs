using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlacedHere;
    //public static event EventHandler<OnNewCounterPlacedEventArgs> OnAnyNewCounterPlaced;
    //public class OnNewCounterPlacedEventArgs : EventArgs
    //{
    //    public Transform newPosition;
    //}
    public static void ResetStaticData()
    {
        OnAnyObjectPlacedHere = null;
        //OnAnyNewCounterPlaced = null;
    }

    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform markTransform;
    [SerializeField] private bool moveable;
    private KitchenObject kitchenObject;

    public virtual void Start()
    {
        if (moveable)
            HideMark();
        else GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if(!GameManager.Instance.IsShopping()) HideMark();
    }

    public virtual void Interact(Player player)
    {
        if (GameManager.Instance.IsShopping())
        {
            if (ShopManager.Instance.IsShopMode())
            {
                if (player.HasKitchenObject())
                {
                    ShopManager.Instance.PurchaseCounter(transform);
                    player.GetKitchenObject().DestroySelf();
                }
            }
            else if (ShopManager.Instance.IsArrangeMode())
            {
                //In arrange mode
                if (!ArrangeManager.Instance.HasMarkedCounter())
                {
                    //If there is no marked counter yet
                    //Mark this counter
                    if (moveable)
                    {
                        ArrangeManager.Instance.SetMarkedCounter(transform);
                        ShowMark();
                    }
                    else
                    {
                        Debug.Log("Can't move this one");
                    }
                }
                else //If there is already a marked counter
                {
                    if (ArrangeManager.Instance.GetMarkedCounter() == transform)
                    {//If this counter is the marked one
                        //Unmark it
                        ArrangeManager.Instance.UnsetMarkedCounter();
                    }
                    else //This is not the marked counter (this is the second counter)
                    {
                        ArrangeManager.Instance.SwapCounter(transform);
                    }
                }
            }
        }
    }

    public void HideMark()
    {
        if(markTransform == null)
        {
            //Debug.Log(gameObject);
            return;
        }
        markTransform.gameObject.SetActive(false);
    }

    private void ShowMark()
    {
        markTransform.gameObject.SetActive(true);
    }

    public virtual void InteractAlternate(Player player) { }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
}
