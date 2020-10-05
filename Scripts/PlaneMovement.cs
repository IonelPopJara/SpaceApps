using Cinemachine;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    //Sets the position of the gameplay plane to the position of the dollycart
    public CinemachineDollyCart dollyCart;

    void FixedUpdate()
    {
        transform.position = dollyCart.transform.position;
    }
}
