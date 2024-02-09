using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<BonusContainer> inventory;

    public void AddItemInInventory(GameObject currentCase)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (!inventory[i].hasItem)
            {
                inventory[i].addItem(currentCase);
                return;
            }
        }
    }
}
