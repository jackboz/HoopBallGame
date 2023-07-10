using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoopBall
{
    public class BallCenterCollider : MonoBehaviour
    {
        public bool isPlayerInsideBall = false;
        public bool isOpponentInsideBall = false;

        GameManager gameManager;
        BallExitCollider exitCollider;

        void Awake()
        {
            gameManager = GameObject.Find("/Managers/GameManager")?.GetComponent<GameManager>();
            exitCollider = transform.parent.GetComponentInChildren<BallExitCollider>();
            if (exitCollider == null)
            {
                Debug.Log("Ball center doesn't have parent game object");
            }
        }

        void Start()
        {
            if (gameManager == null)
            {
                Debug.LogError("LevelUIController is not set. Hierarchy should contain /Managers/LevelUIController");
                return;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (!isPlayerInsideBall)
                {
                    gameManager.AddScore(PlayerType.Player, 1);
                    exitCollider.PlayBurst();
                    isPlayerInsideBall = true;
                }
            }
            if (other.gameObject.CompareTag("Opponent"))
            {
                if (!isOpponentInsideBall)
                {
                    gameManager.AddScore(PlayerType.Opponent, 1);
                    exitCollider.PlayBurst();
                    isOpponentInsideBall = true;
                }
            }
        }
    }
}