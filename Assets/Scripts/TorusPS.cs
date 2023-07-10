using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusPS : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 rotationEuler = transform.rotation.eulerAngles;
        if (Vector3.Angle(Vector3.up, transform.forward) < 90)
        {
            rotationEuler.x += 180;
            transform.Rotate(rotationEuler);
        }
    }
}
