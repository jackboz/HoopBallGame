using UnityEngine;

namespace HoopBall
{
    public class AIagent : MonoBehaviour
    {
        public bool IsOn = false;
        //public bool MovingAway = true;

        [SerializeField] Transform ballTransform;
        Rigidbody _rigidbody;
        InputManager _inputManager;

        //float prevDistance;
        bool firstMoveMade = false;
        int move = -1;

        float _AIMoveTimeThreshold = 0.25f; // min time to make next move
        float _AIMoveTimer = 0f;
        float _incorrectProbability = 0.4f;

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
            //prevDistance = (ballTransform.position - transform.position).sqrMagnitude;
            if (GameProgressStatic.GameRegime == GameRegime.SingleNormal)
            {
                _incorrectProbability = 0.4f;
            }
            else if (GameProgressStatic.GameRegime == GameRegime.SingleHard)
            {
                _incorrectProbability = 0.15f;
            }
        }

        void Update()
        {
            if (!IsOn) return;

            //float distance = (ballTransform.position - transform.position).sqrMagnitude;
            //Debug.Log(Time.time.ToString() + " distance " + distance);
            //MovingAway = distance > prevDistance;
            //Debug.Log("Moving away " + MovingAway);

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
            if (Random.Range(0f, 1f) < _incorrectProbability)
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
