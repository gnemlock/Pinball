using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HoldButton : Button
{

    public void Click()
    {
        GameManager.TriggerRightFlipper();
        StartCoroutine(Release());
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.ReleaseRightFlipper();
    }
}
