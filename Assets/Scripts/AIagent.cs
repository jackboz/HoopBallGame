﻿using UnityEngine;

namespace HoopBall
{
    public class AIagent : MonoBehaviour
    {
        public bool IsOn = false;
        public float IncorrectProbability = 0.4f;

        [SerializeField] Transform ballTransform;
        Rigidbody _rigidbody;
        InputManager _inputManager;

        bool firstMoveMade = false;
        int move = -1;

        float _AIMoveTimeThreshold = 0.25f; // min time to make next move
        float _AIMoveTimer = 0f;

        public void SetInputController(InputManager inputManager)
        {
            _inputManager = inputManager;
        }

        void Start()
        {
            if (ballTransform == null)
            {
                Debug.LogError("Ball transform is not set");
            }
            _rigidbody = GetComponent<Rigidbody>();
            if (_rigidbody == null)
            {
                Debug.LogError("The Agent doesn't have a rigidbody");
            }
            if (GameProgressStatic.GameRegime == GameRegime.SingleNormal)
            {
                IncorrectProbability = 0.4f;
            }
            else if (GameProgressStatic.GameRegime == GameRegime.SingleHard)
            {
                IncorrectProbability = 0.15f;
            }
        }

        void Update()
        {
            if (!IsOn) return;

            if (!firstMoveMade)
            {
                MakeMove();
            }

            _AIMoveTimer += Time.deltaTime;
            if (_AIMoveTimer > _AIMoveTimeThreshold)
            {
                SelectMove();
                MakeMove();
                _AIMoveTimer = 0f;
            }
        }

        void SelectMove()
        {
            move = 0;
            if (transform.position.y < ballTransform.position.y)
            {
                if (transform.position.x > 0)
                {
                    move = -1;
                }
                if (transform.position.x < 0)
                {
                    move = 1;
                }
            }
            if (Random.Range(0f, 1f) < IncorrectProbability)
            {
                move = -move;
            }
        }

        void MakeMove()
        {
            firstMoveMade = true;
            if (_inputManager == null)
            {
                Debug.LogError("Input Controller is not set");
            }
            if (move == 1)
            {
                _inputManager.MoveRight(_rigidbody);
            }
            if (move == -1)
            {
                _inputManager.MoveLeft(_rigidbody);
            }
        }
    }
}
