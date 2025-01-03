using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShopItemSO : ScriptableObject
{
    public KitchenObjectSO counterSO;
    public int price;
    public enum Item
    {
        BreadContainer,
        CabbageContainer,
        CheeseContainer,
        MeetContainer,
        TomatoContainer,
        CuttingCounter,
        StoveCounter
    }
    public Item item;
    public Transform counterPrefab;
}
