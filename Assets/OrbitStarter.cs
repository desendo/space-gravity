using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class OrbitStarter : MonoBehaviour
{
    public Attractor orbitParent;
    
    bool orbitStarted = false;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (!orbitStarted && orbitParent!=null)
        {
            Vector2 deltaR = transform.position - orbitParent.selfTransform.position;
            Vector2 orbitVelocity = Vector2.Perpendicular( deltaR).normalized * Mathf.Sqrt(orbitParent.rb.mass / deltaR.magnitude);
            rb.velocity = orbitVelocity;
            orbitStarted = true;
        }
    }


}
