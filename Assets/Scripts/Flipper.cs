
using UnityEngine;

public class Flipper : MonoBehaviour
{
    [SerializeField] private Direction direction;
    public HingeJoint hinge;
    public float springSpeed;
    public float springDamper;
    static int id = 1;
    public GameManager gameManager;

    public void Awake()
    {
        // Debug.Log("Flipper " + id++);
        hinge = GetComponent<HingeJoint>();
        hinge.useSpring = true;
    } 

    public void Start()
    {
        if(direction == Direction.left)
        {
            gameManager.AddLeftFlipper(this);
        }
        else
        {
            gameManager.AddRightFlipper(this);
        } 
    }
    #if UNITY_EDITOR
    private void Update()
    {
        KeyCode key = (direction == Direction.left) ? KeyCode.LeftArrow : KeyCode.RightArrow;
        if(Input.GetKeyDown(key))
        {
            Flip();
        }
        else if(Input.GetKeyUp(key))
        {
            Release();
        }
    }
    #endif

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
