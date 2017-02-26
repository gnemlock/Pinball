using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightTriggerEvent : MonoBehaviour
{
    public void Hold()
    {
        GameManager.ReleaseRightFlipper();
    }                    
                         
    public void Release()
    {                    
        GameManager.ReleaseRightFlipper();
    }
}
