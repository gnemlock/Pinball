
using UnityEngine;

public class Flipper : MonoBehaviour
{
    [SerializeField] private Direction direction;
    public HingeJoint hinge;
    public float springSpeed;
    public float springDamper;

    public void Awake()
    {
        hinge = GetComponent<HingeJoint>();
        hinge.useSpring = true;

        if(direction == Direction.left)
        {
            GameManager.TriggerLeftFlipper += this.Flip;
            GameManager.ReleaseLeftFlipper += this.Release;
        }
        else
        {
            GameManager.TriggerRightFlipper += this.Flip;
            GameManager.ReleaseRightFlipper += this.Release;
        }                                            
    }                                                
                                                     
    public void OnDestroy()                          
    {                                                
        if(direction == Direction.left)              
        {                                            
            GameManager.TriggerLeftFlipper -= this.Flip;
            GameManager.ReleaseLeftFlipper -= this.Release;
        }                                         
        else                                      
        {                                         
            GameManager.TriggerRightFlipper -= this.Flip;
            GameManager.ReleaseRightFlipper -= this.Release;
        }
    }

    public void Flip()
    {
        ChangeSpring(60);
    }
    // 0 - 60

    void ChangeSpring(float targetPosition)
    {
        JointSpring hingeSpring = hinge.spring;
        hingeSpring.spring = springSpeed;
        hingeSpring.damper = springDamper;
        hingeSpring.targetPosition = targetPosition;
        hinge.spring = hingeSpring;
    }

    public void Release()
    {
        ChangeSpring(0);
    }

    [System.Serializable]
    public enum Direction
    {
        left,
        right
    }
}
