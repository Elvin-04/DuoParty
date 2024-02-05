using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private List<Case> cases = new List<Case>();
    [SerializeField] private GameObject redPlayer;
    [SerializeField] private GameObject greenPlayer;

    public List<Case> _borderCases;
    public List<Case> _centerCases;

    private void Start()
    {
        foreach(Case _case in cases)
        {
            if (_case.GetSpawn() && _case.GetColor() == "Red")
            {
                //Instantiate(redPlayer, _case.transform.position, Quaternion.identity);
                redPlayer.transform.position = _case.transform.position;
            }
            if (_case.GetSpawn() && _case.GetColor() == "Green")
            {
                //Instantiate(greenPlayer, _case.transform.position, Quaternion.identity);
                greenPlayer.transform.position = _case.transform.position;
            }
            if (_case.GetKey())
            {
                Debug.Log("Key");
            }
            if (_case.GetAccessCard())
            {
                Debug.Log("AccessCard");
            }
            if (_case.GetHammer())
            {
                Debug.Log("Hammer");
            }
            if (_case.GetBomb())
            {
                Debug.Log("Bomb");
            }
            if (_case.GetArmouredDoor())
            {
                Debug.Log("ArmouredDoor");
            }
        }
    }


    public void SetBorderList(List<GameObject> list)
    {
        foreach (Case _case in cases)
        {
            if (_case.GetCoordinatesLength() == "A" || _case.GetCoordinatesLength() == "I"
                || _case.GetCoordinatesWidth() == "1" || _case.GetCoordinatesWidth() == "9")
            {
                _borderCases.Add(_case);
            }
            else
            {
                _centerCases.Add(_case);
            }
        }
    }
}
