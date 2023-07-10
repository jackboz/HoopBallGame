using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoopBall
{
    public class BallExitCollider : MonoBehaviour
    {
        ParticleSystem leftBurst;
        ParticleSystem rightBurst;
        BallCenterCollider centerCollider;

        void Awake()
        {
            centerCollider = transform.parent.Find("CenterCollider")?.GetComponent<BallCenterCollider>();
            leftBurst = transform.parent.Find("ConfettiBurstLeft_PS")?.GetComponent<ParticleSystem>();
            rightBurst = transform.parent.Find("ConfettiBurstRight_PS")?.GetComponent<ParticleSystem>();
        }

        void Start()
        {
            if (centerCollider == null)
            {
                Debug.LogError("Can't find child CenterCollider with BallCenterCollider component");
                return;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                centerCollider.isPlayerInsideBall = false;
            }
            if (other.gameObject.CompareTag("Opponent"))
            {
                centerCollider.isOpponentInsideBall = false;
            }
        }

        public void PlayBurst()
        {
            leftBurst?.Play();
            rightBurst?.Play();
        }
    }
}