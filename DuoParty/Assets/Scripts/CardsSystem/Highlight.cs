using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Highlight : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> renderers;
    [SerializeField] private List<Image> images;
    private Color bColor = Color.black;
    public Color redColor = Color.red;
    private Color grayColor;

    private void Awake()
    {
        if (GetChildGameObject(gameObject, "Square").GetComponent<SpriteRenderer>() != null)
        {
            renderers.Add(GetChildGameObject(gameObject, "Square").GetComponent<SpriteRenderer>());
            renderers.Add(GetChildGameObject(gameObject, "Square (1)").GetComponent<SpriteRenderer>());
            renderers.Add(GetChildGameObject(gameObject, "Square (2)").GetComponent<SpriteRenderer>());
            renderers.Add(GetChildGameObject(gameObject, "Square (3)").GetComponent<SpriteRenderer>());
        }
        else
        {
            images.Add(GetChildGameObject(gameObject, "Square").GetComponent<Image>());
            images.Add(GetChildGameObject(gameObject, "Square (1)").GetComponent<Image>());
            images.Add(GetChildGameObject(gameObject, "Square (2)").GetComponent<Image>());
            images.Add(GetChildGameObject(gameObject, "Square (3)").GetComponent<Image>());
            grayColor = images[0].color;
        }
        
    }

    public void ToggleHighlight(bool val, Color highlightColor)
    {
        if (val)
        {
            foreach (var cote in renderers)
            {
                cote.color = highlightColor;
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

    public void StartRedBlink()
    {
        foreach (var cote in renderers)
        {
            cote.sortingOrder = 110;
        }
        StartCoroutine(RedBlink(0.5f));
        foreach (var cote in renderers)
        {
            cote.sortingOrder = 1;
        }
    }

    IEnumerator RedBlink(float totalTime)
    {
        float time = 0f;
        while (time / totalTime < 0.1f)
        {
            time += Time.deltaTime;
            foreach (var cote in renderers)
            {
                cote.color = new Color(1 + time / totalTime * 6, 0, 0, 1);
                yield return new WaitForEndOfFrame();
            }
        }
        while (time / totalTime < 0.2f)
        {
            time += Time.deltaTime;
            foreach (var cote in renderers)
            {
                cote.color = new Color(1 - time / totalTime * 6, 0, 0, 1);
                yield return new WaitForEndOfFrame();
            }
        }
        while (time / totalTime < 0.3f)
        {
            time += Time.deltaTime;
            foreach (var cote in renderers)
            {
                cote.color = new Color(1 + time / totalTime * 6, 0, 0, 1);
                yield return new WaitForEndOfFrame();
            }
        }
        while (time / totalTime < 0.4f)
        {
            time += Time.deltaTime;
            foreach (var cote in renderers)
            {
                cote.color = new Color(1 - time / totalTime * 6, 0, 0, 1);
                yield return new WaitForEndOfFrame();
            }
        }
    }


    public void StartGreenBlink()
    {
        StartCoroutine(GreenBlink(0.5f));
    }

    IEnumerator GreenBlink(float totalTime)
    {
        float time = 0f;
        while (time / totalTime < 0.1f)
        {
            time += Time.deltaTime;
            foreach (var cote in images)
            {
                cote.color = new Color(0, 1 + time / totalTime * 6, 0, 1);
                yield return new WaitForEndOfFrame();
            }
        }
        while (time / totalTime < 0.2f)
        {
            time += Time.deltaTime;
            foreach (var cote in images)
            {
                cote.color = /*new Color(0, 1 - time / totalTime * 6, 0, 1)*/ grayColor;
                yield return new WaitForEndOfFrame();
            }
        }
        while (time / totalTime < 0.3f)
        {
            time += Time.deltaTime;
            foreach (var cote in images)
            {
                cote.color = new Color(0, 1 + time / totalTime * 6, 0, 1);
                yield return new WaitForEndOfFrame();
            }
        }
        while (time / totalTime < 0.4f)
        {
            time += Time.deltaTime;
            foreach (var cote in images)
            {
                cote.color = /*new Color(0, 1 - time / totalTime * 6, 0, 1)*/ grayColor;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
