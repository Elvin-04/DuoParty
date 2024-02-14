using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<BonusContainer> inventory;

    public void AddItemInInventory(Case currentCase)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].bonus == TypeOfBonus.hammer && currentCase.isHammer)
            {
                inventory[i].addItem();
                return;
            }
            else if(inventory[i].bonus == TypeOfBonus.accessCard && currentCase.isAccessCard)
            {
                inventory[i].addItem();
                return;
            }
        }
    }
}
