using UnityEngine;

public class ScreenBorders : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.parent.GetComponent<PlayerMovement>().ShipOutOfBorders();
        }
    }
}
