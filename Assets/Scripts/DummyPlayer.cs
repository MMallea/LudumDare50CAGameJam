using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayer : MonoBehaviour
{
    public Rigidbody[] jointRbs;

    // Start is called before the first frame update
    void Start()
    {
        jointRbs = GetComponentsInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void AddUpwardForce(float amount)
    //{
    //    if (jointRbs.Length == 0)
    //        return;
    //
    //    foreach(Rigidbody rBody in jointRbs)
    //    {
    //
    //    }
    //}
}
