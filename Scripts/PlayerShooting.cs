using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("References")]
    public InputManager input;
    public PropulsorManager propulsor;
    public LayerMask canShoot;

    [Header("Parameters")]
    public float maxShootingDistance;

    private void Update()
    {
        Vector3 coord = new Vector3(Screen.width/2, Screen.height/2, 0f);
        Ray ray = Camera.main.ScreenPointToRay(coord);

        if(input.GetFire1())
        {
            RaycastShoot(ray);
            return;
        } 

        Debug.DrawRay(ray.origin, ray.direction * maxShootingDistance, Color.green);
    }

    private void RaycastShoot(Ray ray)
    {
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, maxShootingDistance, canShoot))
        {
            print("Hola");
            Debug.DrawRay(ray.origin, ray.direction * maxShootingDistance, Color.red);
            Debug.Log($"Obstacle hit: {hit.collider.name}");
            hit.collider.GetComponent<IInteractable>()?.Interact();
            //propulsor.AddEnergy(-2f);
        }
    }
}
