using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHoldButton : MonoBehaviour
{
    public void Click()
    {
        GameManager.TriggerLeftFlipper();
        StartCoroutine(Release());
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.ReleaseLeftFlipper();
    }
}
