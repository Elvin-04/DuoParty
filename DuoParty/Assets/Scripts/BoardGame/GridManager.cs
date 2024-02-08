using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private List<Case> cases = new List<Case>();
    [SerializeField] private GameObject redPlayer;
    [SerializeField] private GameObject greenPlayer;

    [SerializeField] private int _keyCount;
    [SerializeField] private int _vaccineRedCount;
    [SerializeField] private int _vaccineGreenCount;
    [SerializeField] private int _hammerCount;
    [SerializeField] private int _AccessCardCount;
    [SerializeField] private int _bombCount;
    [SerializeField] private int _ArmouredDoorCount;
    [SerializeField] private int _spawnCount;
    [SerializeField] private int _endCount;

    private int _total;
    private int _totalBase;

    public List<Case> _borderCases;
    public List<Case> _centerCases;

    private void Start()
    {
        SetBorderList();

        _total = _keyCount + _hammerCount + _AccessCardCount + _bombCount + _ArmouredDoorCount + _vaccineRedCount + _vaccineGreenCount;
        for (int i = 0; i <= _total; i++)
        {
            InvestmentElement();
        }
        _totalBase = _spawnCount + _endCount;
        for (int i = 0; i <= _totalBase; i++)
        {
            InvestmentBaseElement();
        }

        foreach (Case _case in cases)
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
        int _rand = Random.Range(0, _centerCases.Count);
        if (_centerCases[_rand].IsEmpty())
        {
            if (_keyCount > 0)
            {
                _centerCases[_rand].SetKey();
                _centerCases.Remove(_centerCases[_rand]);
                _keyCount--;
            }
            else if (_vaccineRedCount > 0)
            {
                _centerCases[_rand].SetVaccineRed();
                _centerCases.Remove(_centerCases[_rand]);
                _vaccineRedCount--;
            }
            else if (_vaccineGreenCount > 0)
            {
                _centerCases[_rand].SetVaccineGreen();
                _centerCases.Remove(_centerCases[_rand]);
                _vaccineGreenCount--;
            }
            else if (_hammerCount > 0)
            {
                _centerCases[_rand].isHammer = true;
                _centerCases.Remove(_centerCases[_rand]);
                _hammerCount--;
            }
            else if (_AccessCardCount > 0)
            {
                _centerCases[_rand].isAccessCard = true;
                _centerCases.Remove(_centerCases[_rand]);
                _AccessCardCount--;
            }
            else if (_bombCount > 0)
            {
                _centerCases[_rand].isBomb = true;
                _centerCases.Remove(_centerCases[_rand]);
                _bombCount--;
            }
            else if (_ArmouredDoorCount > 0)
            {
                _centerCases[_rand].isArmouredDoor = true;
                _centerCases.Remove(_centerCases[_rand]);
                _ArmouredDoorCount--;
            }
        }
        else
        {
            InvestmentElement();
            return;
        }
    }

    public void InvestmentBaseElement()
    {
        int _rand = Random.Range(0, _borderCases.Count);
        if (_borderCases[_rand].IsEmpty())
        {
            if (_spawnCount == 2) 
            {
                _borderCases[_rand].SetSpawnRed();
                _borderCases.Remove(_borderCases[_rand]);
                _spawnCount--;
            }
            else if (_spawnCount == 1)
            {
                _borderCases[_rand].SetSpawnGreen();
                _borderCases.Remove(_borderCases[_rand]);
                _spawnCount--;
            }
            else if (_endCount == 2)
            {
                _borderCases[_rand].SetEndRed();
                _borderCases.Remove(_borderCases[_rand]);
                _endCount--;
            }
            else if (_endCount == 1)
            {
                _borderCases[_rand].SetEndGreen();
                _borderCases.Remove(_borderCases[_rand]);
                _endCount--;
            }
        }
        else
        {
            InvestmentBaseElement();
            return;
        }
    }
}