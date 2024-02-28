using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCredits : MonoBehaviour
{
    //[SerializeField] private GameObject _canvasMenu;
    [SerializeField] private List<CanvasGroup> m_objectsToFade = new List<CanvasGroup>();
    [SerializeField] private float m_FadeInDuration, m_fadeOutDuration, m_AttendanceTime, m_WaitBeforeStart;

    public static FadeCredits Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.Log("There is FadeCredits");
            Destroy(this);
            return;
        }

        
    }
    private void OnEnable()
    {
        Debug.Log("Credit");
        StartCoroutine(StartFadeSequence());
    }

    private IEnumerator StartFadeSequence()
    {
        yield return new WaitForSeconds(m_WaitBeforeStart);

        StartCoroutine(FadeSequence());
    }
    private IEnumerator FadeSequence()
    {
        foreach (CanvasGroup canvasGroup in m_objectsToFade)
        {
            yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 1f, m_FadeInDuration));

            yield return new WaitForSeconds(m_AttendanceTime);

            if (canvasGroup != m_objectsToFade[m_objectsToFade.Count - 1])
                yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 0f, m_fadeOutDuration));
        }
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float targetAlpha, float duration)
    {
        float startAlpha = canvasGroup.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
        if (targetAlpha == 0f)
        {
            if (canvasGroup != m_objectsToFade[m_objectsToFade.Count - 1])
            {
                canvasGroup.gameObject.SetActive(false);
            }
        }
    }

    public void ResetCredits()
    {
        StartCoroutine(StartFadeSequence());
        foreach (CanvasGroup canvasGroup in m_objectsToFade)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.gameObject.SetActive(true);
        }
    }
}


