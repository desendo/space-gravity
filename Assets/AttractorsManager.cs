using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorsManager : MonoBehaviour
{

    static public List<Attractor> attractors = new List<Attractor>(100);

    public static void Add(Attractor attractor)
    {
        attractors.Add(attractor);
    }
    public static void Remove(Attractor attractor)
    {
        attractors.Remove(attractor);
    }
    
    private void FixedUpdate()
    {

        foreach (var attractor in attractors)
        {
            attractor.DoAttract(attractors);
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
