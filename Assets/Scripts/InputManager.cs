using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoopBall
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] Rigidbody _playerRb;
        [SerializeField] Rigidbody _opponentRb;
        [SerializeField] AIagent _AIagent;
        [SerializeField] float _force = 2f;
        [SerializeField] float _YLimit = 2.85f;

        public bool IsOn = false;
        public bool IsAIOn = true;

        Animator _playerAnim;
        Animator _opponentAnim;
        ParticleSystem _playerTailPS;
        ParticleSystem _opponentTailPS;

        public void TurnOn(bool AI)
        {
            IsOn = true;
            IsAIOn = AI;
            if (AI)
            {
                _AIagent.IsOn = true;
                _AIagent.SetInputController(this);
                if (GameProgressStatic.GameRegime == GameRegime.SingleNormal)
                {
                    _AIagent.IncorrectProbability = 0.4f;
                }
                else if (GameProgressStatic.GameRegime == GameRegime.SingleHard)
                {
                    _AIagent.IncorrectProbability = 0.15f;
                }
            }
        }
        public void TurnOff()
        {
            IsOn = false;
            IsAIOn = false;
            _AIagent.IsOn = false;
        }

        void Start()
        {
            if (_playerRb == null)
            {
                Debug.LogError("Input Player RigidBody is not set");
            }
            _playerRb.isKinematic = true;
            if (_opponentRb == null)
            {
                Debug.LogError("Input Opponent RigidBody is not set");
            }
            _opponentRb.isKinematic = true;
            _playerAnim = _playerRb.GetComponentInChildren<Animator>();
            if (_playerAnim == null)
            {
                Debug.LogWarning("Player does not have animator component");
            }
            _opponentAnim = _opponentRb.GetComponentInChildren<Animator>();
            if (_opponentAnim == null)
            {
                Debug.LogWarning("Opponent does not have animator component");
            }
            _playerTailPS = _playerRb.transform.Find("TailPS")?.GetComponent<ParticleSystem>();
            if (_playerTailPS == null)
            {
                Debug.LogWarning("Player Tail PS is not set");
            }
            _opponentTailPS = _opponentRb.transform.Find("TailPS")?.GetComponent<ParticleSystem>();
            if (_opponentTailPS == null)
            {
                Debug.LogWarning("Opponent Tail PS is not set");
            }
            if (_AIagent == null)
            {
                Debug.LogWarning("AIAgent component is not set");
            }
        }

        void Update()
        {
            if (!IsOn) return;

            if (Input.GetKeyDown(KeyCode.D))
            {
                MoveRight(_playerRb);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                MoveLeft(_playerRb);
            }

            if (!IsAIOn)
            {
                if (Input.GetKeyDown("right"))
                {
                    MoveRight(_opponentRb);
                }

                if (Input.GetKeyDown("left"))
                {
                    MoveLeft(_opponentRb);
                }
            }
        }

        public void MoveLeft(Rigidbody rb)
        {
            if (rb.transform.position.y > _YLimit) return;

            rb.isKinematic = false;
            rb.AddForce(new Vector3(-1, 2, 0) * _force, ForceMode.Impulse);
            if (rb.CompareTag("Player"))
            {
                _playerAnim?.Play("PressAnimation");
                _playerTailPS.Play();
            }
            else
            {
                _opponentAnim?.Play("PressAnimation");
                _opponentTailPS?.Play();
            }
        }

        public void MoveRight(Rigidbody rb)
        {
            if (rb.transform.position.y > _YLimit) return;

            rb.isKinematic = false;
            rb.AddForce(new Vector3(1, 2, 0) * _force, ForceMode.Impulse);
            if (rb.CompareTag("Player"))
            {
                _playerAnim?.Play("PressAnimation");
                _playerTailPS?.Play();
            }
            else
            {
                _opponentAnim?.Play("PressAnimation");
                _opponentTailPS?.Play();
            }
        }
    }
}