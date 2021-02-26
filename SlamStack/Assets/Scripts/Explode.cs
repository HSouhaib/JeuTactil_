using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] private List<Rigidbody> myRigidbodies;
    [SerializeField] private float explosionPower = 20f;
    [SerializeField] private Vector3 explosionOffSet;
    void Start()
    {
        for (int i = 0; i < myRigidbodies.Count; i++)
        {
            myRigidbodies[i].AddExplosionForce(explosionPower, transform.position + explosionOffSet, 2);
        }
    }

   
    void Update()
    {
        
    }
}
