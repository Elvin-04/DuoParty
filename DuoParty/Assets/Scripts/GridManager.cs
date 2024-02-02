using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private List<Case> cases = new List<Case>();
    [SerializeField] private GameObject redPlayer;
    [SerializeField] private GameObject greenPlayer;

    private void Start()
    {
        foreach(Case _case in cases)
        {
            if (_case.GetSpawn() && _case.GetColor() == "Red")
            {
                Instantiate(redPlayer, _case.transform.position, Quaternion.identity);
            }
            if (_case.GetSpawn() && _case.GetColor() == "Green")
            {
                Instantiate(greenPlayer, _case.transform.position, Quaternion.identity);
            }
        }
    }
}
