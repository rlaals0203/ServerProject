using System.Collections;
using UnityEngine;

public class PvpUI : ContentUI
{
    private void Awake()
    {
        StartCoroutine(WaitRoutine(1f));
    }

    private IEnumerator WaitRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}