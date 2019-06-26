using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Transform selfTransform;
    private void Awake()
    {

        selfTransform = transform;
        rb = GetComponent<Rigidbody2D>();
        AttractorsManager.Add(this);
    }
    private void OnDestroy()
    {
        AttractorsManager.Remove(this);
    }
    public void DoAttract(List<Attractor> attractors)
    {
        
        foreach (var att in attractors)
        {
            if(att!=this)
                //if(att.rb.mass/rb.mass < 2f)
                    Attract(att);
        }
    }
    public Vector2 CalculateForce(Attractor objToattract)
    {
        Vector2 dir = selfTransform.position - objToattract.selfTransform.position;
        float sqrDistance = dir.sqrMagnitude;
        if (sqrDistance == 0f)
            return Vector2.zero;


        float forceMagnitude = (rb.mass * objToattract.rb.mass) / sqrDistance;

        Vector2 force = dir.normalized * forceMagnitude;
        return force;

    }
    public Vector2 CalculateForce(Attractor objToattract,Vector2 selfPos, Vector2 objectPos)
    {
        Vector2 dir = selfPos - objectPos;
        float sqrDistance = dir.sqrMagnitude;
        if (sqrDistance == 0f)
            return Vector2.zero;


        float forceMagnitude = (rb.mass * objToattract.rb.mass) / sqrDistance;

        Vector2 force = dir.normalized * forceMagnitude;
        return force;

    }

    void Attract(Attractor objToattract)
    {
        Vector2 force = CalculateForce(objToattract);
        objToattract.rb.AddForce(force);
        
    }
}
