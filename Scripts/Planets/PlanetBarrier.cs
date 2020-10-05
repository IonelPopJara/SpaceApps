using UnityEngine;

public class PlanetBarrier : MonoBehaviour
{
    public float speed;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.parent.GetComponent<PropulsorManager>().currentSpeed = speed;
        }
    }
}
