using UnityEngine;

public class PlanetHD : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(Camera.main.transform.position, -Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        Debug.Log("Win");
    }
}
