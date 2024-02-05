using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private List<Case> cases = new List<Case>();
    [SerializeField] private GameObject redPlayer;
    [SerializeField] private GameObject greenPlayer;

    [SerializeField] private int _keyCount;
    [SerializeField] private int _vaccineCount;
    [SerializeField] private int _hammerCount;
    [SerializeField] private int _AccessCardCount;
    [SerializeField] private int _bombCount;
    [SerializeField] private int _ArmouredDoorCount;

    private int _total;

    public List<Case> _borderCases;
    public List<Case> _centerCases;

    private void Start()
    {
        SetBorderList();
        _total = _keyCount + _hammerCount + _AccessCardCount + _bombCount + _ArmouredDoorCount + _vaccineCount;
        for (int i = 0; i <= _total; i++)
        {
            InvestmentElement();
        }

        foreach (Case _case in cases)
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


    public void SetBorderList()
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

    public void InvestmentElement()
    {
        int _rand = Random.Range(0,_centerCases.Count) ;
        if (_centerCases[_rand].IsEmpty())
        {
            if (_keyCount > 0) 
            {
                _centerCases[_rand].Setkey();
                _keyCount--;
            }
            else if (_vaccineCount > 0)
            {
                _centerCases[_rand].Setvaccine();
                _vaccineCount--;
            }
            else if (_hammerCount > 0) 
            {
                _centerCases[_rand].isHammer = true;
                _hammerCount--;
            }
            else if (_AccessCardCount > 0)
            {
                _centerCases[_rand].isAccessCard = true;
                _AccessCardCount--;
            }
            else if (_bombCount > 0)
            {
                _centerCases[_rand].isBomb = true;
                _bombCount--;
            }
            else if (_ArmouredDoorCount > 0)
            {
                _centerCases[_rand].isArmouredDoor = true;
                _ArmouredDoorCount--;
            }
        }
        else
        {
            InvestmentElement();
            return;
        }
    }
}
