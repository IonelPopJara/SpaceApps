using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    private const float G = 667.4f;

    public static List<Attractor> Bodies;

    public Rigidbody rb;

    private void FixedUpdate()
    {
        foreach(Attractor body in Bodies)
        {
            if(body != this) Attract(body);
        }
    }

    private void OnEnable()
    {
        if(Bodies == null) Bodies = new List<Attractor>();

        Bodies.Add(this);
    }

    private void OnDisable()
    {
        Bodies.Remove(this);
    }

    private void Attract(Attractor bodyToAttract)
    {
        Rigidbody rbToAttract = bodyToAttract.rb;

        Vector3 direction = rb.position - rbToAttract.position;
        float distance = direction.magnitude;

        if(distance == 0f) return;

        float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        rbToAttract.AddForce(force);
    }
}
