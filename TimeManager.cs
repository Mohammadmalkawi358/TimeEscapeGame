using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SlowTime(float duration, float timeScale)
    {
        StopAllCoroutines();
        StartCoroutine(DoSlow(duration, timeScale));
    }

    IEnumerator DoSlow(float duration, float tScale)
    {
        float original = Time.timeScale;
        Time.timeScale = tScale;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = original;
    }
}
