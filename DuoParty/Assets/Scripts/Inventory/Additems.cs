using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItems : MonoBehaviour
{
    public GameObject _equipement;
    int index = 0;
    public List<GameObject> _items = new List<GameObject>();

    public void AddInventoryEquipement()
    {

        GameObject item = Instantiate (_equipement, _items[0].transform);

        if (RoundByRound.instance.turn == 0)
        {
            item.tag = "Red";
        }

        if (RoundByRound.instance.turn == 1)
        {
            item.tag = "Green";
        }
    }
}
