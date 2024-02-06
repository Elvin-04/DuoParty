using System.Collections.Generic;
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
            if (_case.GetSpawn() && _case.GetColor() == cardcolors.red)
            {
                //Instantiate(redPlayer, _case.transform.position, Quaternion.identity);
                redPlayer.transform.position = _case.transform.position;
            }
            if (_case.GetSpawn() && _case.GetColor() == cardcolors.green)
            {
                //Instantiate(greenPlayer, _case.transform.position, Quaternion.identity);
                greenPlayer.transform.position = _case.transform.position;
            }
        }
    }

    public List<Case> GetGrid()
    {
        return cases;
    }

    public Case GetSpawnOfColor(cardcolors color)
    {
        foreach(Case _case in cases)
        {
            if (_case.GetSpawn() && _case.GetColor() == color)
            {
                return _case;
            }
        }
        return null;
    }

    public Case GetEndOfColor(cardcolors color)
    {
        foreach (Case _case in cases)
        {
            if (_case.GetEnd() && _case.GetColor() == color)
            {
                return _case;
            }
        }
        return null;
    }

}
