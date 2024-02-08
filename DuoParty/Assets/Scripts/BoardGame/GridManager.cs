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
                //Instantiate(redPlayer, _case.transform.position, Quaternion.identity);
                redPlayer.transform.position = _case.transform.position;
                redPlayer.GetComponent<PlayerMovement>().actCase = _case;
            }
            if (_case.GetSpawn() && _case.GetColor() == "Green")
            {
                //Instantiate(greenPlayer, _case.transform.position, Quaternion.identity);
                greenPlayer.transform.position = _case.transform.position;
                greenPlayer.GetComponent<PlayerMovement>().actCase = _case;
            }
        }

        int x = 1;
        int y = 1;

        foreach(Case _case in cases)
        {
            _case.x = x;
            _case.y = y;
            if (x == 9)
            {
                x = 1;
                y++;
            }
            else x++;
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
            if (_case.GetSpawn() && _case.GetEColor() == color)
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
            if (_case.GetEnd() && _case.GetEColor() == color)
            {
                return _case;
            }
        }
        return null;
    }

}
