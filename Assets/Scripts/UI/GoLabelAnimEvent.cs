using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoopBall
{
    public class GoLabelAnimEvent : MonoBehaviour
    {
        [SerializeField] GameManager gameManager;

        void Start()
        {
            if (gameManager == null)
            {
                Debug.LogError("Game Manager is not set");
            }
        }

        public void OnAnimationEnd()
        {
            transform.parent.gameObject.SetActive(false);
            gameManager.SwitchToLevel();
        }
    }
}