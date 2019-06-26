using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnOnTimeOut : MonoBehaviour
{
    [SerializeField] float timeOut=1;
    void OnEnable()
    {
        Invoke("Despawn", timeOut);
    }
    void Despawn()
    {
        SimplePool.Despawn(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
