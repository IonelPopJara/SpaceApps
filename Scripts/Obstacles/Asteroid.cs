using UnityEngine;

public class Asteroid : MonoBehaviour, IInteractable
{
    public Transform player;
    public GameObject upgradePrefab;
    public int asteroidHealth = 3;

    public float asteroidForce = 25f;
    public bool hasPrefab;

    public float minPlayerDistance = 10f;
    public bool isPlayerNear;

    [Range(0,3)]
    public int Upgrade;


    public Rigidbody rb;

    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>().gameObject.transform;
        isPlayerNear = false;
    }
    private void Update()
    {
        float playerDistance = Vector3.Distance(transform.position, player.position);
        transform.LookAt(Camera.main.transform.position, -Vector3.up);

        //Debug.Log("Player distance:" + playerDistance);

        if(playerDistance < minPlayerDistance) isPlayerNear = true;
        
        if(!isPlayerNear) return;

        Vector3 dir = player.position - transform.position;
        rb.AddForce(dir * asteroidForce * Time.deltaTime, ForceMode.Impulse);
        asteroidForce = 0f;
    }

    public void Interact()
    {
        Debug.Log("Asteroid Interact");
        DamageAsteroid();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.parent.GetComponent<PlayerMovement>().DamageShip();
            hasPrefab = false;
            DestroyAsteroid();
        }
    }

    private void DamageAsteroid()
    {
        asteroidHealth -= 1;
        if(asteroidHealth <= 0) DestroyAsteroid();
    }
    private void DestroyAsteroid()
    {
        if(hasPrefab)
        {
            //En vez de esto, podria activar al tiro la propulsion
            player.GetComponent<PropulsorManager>().EnableUpgrade(Upgrade);
        }
        Destroy(gameObject);
    }
}
