using Cinemachine;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public InputManager input;
    public PropulsorManager propulsor;
    public Rigidbody rb;
    public CinemachineDollyCart dollyCart; //This is used just to set the rotation

    [Space]

    [Header("Side Thrust")]
    public float sideThrustForce;
    public float fuelRate;
    public float energyRate;

    [Space]

    [Header("Limits (Unused)")]
    public Vector2 limits = new Vector2(5,3);

    private void Update()
    {
        //Set the rotation of the spaceship to the rotation of the dollycart
        rb.rotation = dollyCart.transform.rotation;
    }
    private void FixedUpdate()
    {
        RigidbodyMove(input.GetHorizontal(), input.GetVertical(), sideThrustForce);
    }

    private void LateUpdate()
    {
        //rb.rotation = dollyCart.transform.rotation;
        //rb.MovePosition(dollyCart.transform.position);
    }

    private void RigidbodyMove(float x, float y, float speed)
    {
        rb.AddForce(transform.right * x * 10 * speed * Time.fixedDeltaTime);
        rb.AddForce(transform.up * y * 10 * speed * Time.fixedDeltaTime);

        if(input.GetHorizontal() == 0 || input.GetVertical() == 0) return;
        
        propulsor.Accelerate(0f, fuelRate, energyRate);
    }

    public void DamageShip()
    {
        Debug.Log("Waaaa");
    }

    public void ShipOutOfBorders()
    {
        Debug.Log("Weeeee");
        rb.velocity = Vector3.zero;
    }

#region UnUsed
    private void ClampPosition()
    {
        Vector3 pos = rb.position;

        pos.x = Mathf.Clamp(pos.x, -limits.x, limits.x);
        pos.y = Mathf.Clamp(pos.y, -limits.y, limits.y);

        if(pos.x <= -limits.x || pos.x >= limits.x)
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, rb.velocity.z);
        }
        else if(pos.y <= -limits.y || pos.y >= limits.y)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        }

        rb.position = pos;
    }

/*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(-limits.x, -limits.y, transform.position.z), new Vector3(limits.x, -limits.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(-limits.x, limits.y, transform.position.z), new Vector3(limits.x, limits.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(-limits.x, -limits.y, transform.position.z), new Vector3(-limits.x, limits.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(limits.x, -limits.y, transform.position.z), new Vector3(limits.x, limits.y, transform.position.z));
    }
*/
#endregion

}
