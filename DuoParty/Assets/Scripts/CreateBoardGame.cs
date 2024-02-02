using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine;

public class CreateBoardGame : MonoBehaviour
{
    public int _size;
    public float _coeff;
    public GameObject _gridPrefab;
    public float _distanceBtwCells;
    public List<List<GameObject>> _gridEmpty;

    private void Awake()
    {
        _coeff = 4.5f / (_size / 2);
        _distanceBtwCells = _gridPrefab.transform.localScale.x;
        _gridEmpty = new List<List<GameObject>>(_size);

            //InstantiateGrid(-8f, 4.5f);
    }

/*    private void InstantiateGrid(float startX, float startY)
    {
        for (int i = 0; i <= _size; i++)
        {
            List<GameObject> EmptyToAdd = new List<GameObject>(_size);
            for (int j = 0; j < _size; j++)
            {
                if (i < 9)
                {
                    GameObject c = Instantiate(_gridPrefab, new Vector3(-4f + j * _distanceBtwCells, 3.89f - i * _distanceBtwCells, 0), Quaternion.identity);
                    EmptyToAdd.Add(c);
                }
            }
            if (i < 9)
            {
                _gridEmpty.Add(EmptyToAdd);
            }
        }
    }*/

    public void ScreenMouseRay(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.TryGetComponent<Case>(out Case _case) && _case.GetInteractible())
            {
                Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
            }
        }
    }
}