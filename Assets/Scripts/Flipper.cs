
using UnityEngine;

public class Flipper : MonoBehaviour
{
    [SerializeField] private Direction direction;

    public void Start()
    {
        if(direction == Direction.left)
        {
            GameManager.instance.TriggerLeftFlipper += Flip;
            GameManager.instance.ReleaseLeftFlipper += Release;
        }
        else
        {
            GameManager.instance.TriggerRightFlipper += Flip;
            GameManager.instance.ReleaseRightFlipper += Release;
        }
    }

    public void OnDestroy()
    {
        if(direction == Direction.left)
        {
            GameManager.instance.TriggerLeftFlipper -= Flip;
            GameManager.instance.ReleaseLeftFlipper -= Release;
        }
        else
        {
            GameManager.instance.TriggerRightFlipper -= Flip;
            GameManager.instance.ReleaseRightFlipper -= Release;
        }
    }

    public void Flip()
    {
    }

    public void Release()
    {
    }

    [System.Serializable]
    public enum Direction
    {
        left,
        right
    }
}
