using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> renderers;
    [SerializeField] private Color color = Color.white;
    private Color bColor = Color.black;

    private void Awake()
    {
        renderers.Add(GetChildGameObject(gameObject, "Square").GetComponent<SpriteRenderer>());
        renderers.Add(GetChildGameObject(gameObject, "Square (1)").GetComponent<SpriteRenderer>());
        renderers.Add(GetChildGameObject(gameObject, "Square (2)").GetComponent<SpriteRenderer>());
        renderers.Add(GetChildGameObject(gameObject, "Square (3)").GetComponent<SpriteRenderer>());
    }

    public void ToggleHighlight(bool val)
    {
        if (val)
        {
            foreach (var cote in renderers)
            {
                cote.color = color;
                cote.sortingOrder = 110;
            }
        }
        else
        {
            foreach (var cote in renderers)
            {
                cote.color = bColor;
                cote.sortingOrder = 1;
            }
        }
    }

    public GameObject GetChildGameObject(GameObject fromGameObject, string withName)
    {
        var allKids = fromGameObject.GetComponentsInChildren<Transform>();
        var kid = allKids.FirstOrDefault(k => k.gameObject.name == withName);
        if (kid == null) return null;
        return kid.gameObject;
    }
}
