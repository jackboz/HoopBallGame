using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoopBall
{
    public class Wall : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            Vector3 velocity = other.transform.parent.GetComponent<Rigidbody>().velocity;
            if (other.transform.position.x < 0)
            {
                velocity.x = Mathf.Abs(velocity.x);
            }
            else if (other.transform.position.x > 0)
            {
                velocity.x = -Mathf.Abs(velocity.x);
            }
            other.transform.parent.GetComponent<Rigidbody>().velocity = velocity;
        }
    }
}