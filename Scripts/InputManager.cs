using UnityEngine;

public class InputManager : MonoBehaviour
{
    //Check Input and Return Input
    private bool up;
    private bool down;
    private bool left;
    private bool right;

    private bool fire1;

    private bool forwardThrust;

    private float vertical;
    private float horizontal;

    public bool keyNum1;
    public bool keyNum2;
    private bool keyNum3;
    private bool keyNum4;

    private void Start()
    {
        up = down = left = right = false;
    }
    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        forwardThrust = Input.GetKey(KeyCode.Space);

        keyNum1 = Input.GetKeyDown(KeyCode.Alpha1);
        keyNum2 = Input.GetKeyDown(KeyCode.Alpha2);
        keyNum3 = Input.GetKeyDown(KeyCode.Alpha3);
        keyNum4 = Input.GetKeyDown(KeyCode.Alpha4);

        fire1 = Input.GetKeyDown(KeyCode.Mouse0);
    }

    public bool Up()
    {
        return up;
    }

    public bool Down()
    {
        return down;
    }

    public bool Left()
    {
        return left;
    }

    public bool Right()
    {
        return right;
    }

    public float GetHorizontal()
    {
        return horizontal;
    }

    public float GetVertical()
    {
        return vertical;
    }

    public bool ForwardThrust()
    {
        return forwardThrust;
    }

    public bool GetFire1()
    {
        return fire1;
    }

    public bool Num1()
    {
        return keyNum1;
    }

    public bool Num2()
    {
        return keyNum2;
    }

    public bool Num3()
    {
        return keyNum3;
    }

    public bool Num4()
    {
        return keyNum4;
    }
}
