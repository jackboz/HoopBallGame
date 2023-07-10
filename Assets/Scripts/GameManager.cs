using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HoopBall
{
    public enum PlayerType
    {
        Player,
        Opponent
    }

    public class GameManager : MonoBehaviour
    {
        [SerializeField] LevelUIController levelUIController;
        [SerializeField] InputManager inputManager;

        public int playerScore = 0;
        public int opponentScore = 0;
        public int maxGoals = 5;

        float endingTimer = 0;
        float endingTime = 0.5f;
        bool isEnding = false;

        void Awake()
        {
            if (levelUIController == null)
            {
                Debug.LogError("Set level ui controller");
                return;
            }
            levelUIController.Init(this);
            if (inputManager == null)
            {
                Debug.LogError("Set Input Manager");
                return;
            }
        }

        void Start()
        {
            Time.timeScale = 1.0f;
            if (GameProgressStatic.ContinueGame)
            {
                switch (GameProgressStatic.GameRegime)
                {
                    case GameRegime.SingleNormal:
                    case GameRegime.SingleHard:
                        levelUIController.SwitchToLevelUI1P();
                        break;
                    case GameRegime.Hotseat:
                        levelUIController.SwitchToLevelUI2P();
                        break;
                }
            }
            else
            {
                levelUIController.SwitchToStartUI();
            }
        }

        void Update()
        {
            if (isEnding)
            {
                if (endingTimer > endingTime)
                {
                    SceneManager.LoadScene("EndLevelScene");
                }
                endingTimer += Time.deltaTime;
                return;
            }

            if (playerScore >= maxGoals)
            {
                EndRound(true);
            }
            if (opponentScore >= maxGoals)
            {
                EndRound(false);
            }
        }

        public void AddScore(PlayerType playerType, int number)
        {
            if (playerType == PlayerType.Player)
            {
                playerScore += number;
                levelUIController.ChangeScore(playerType, playerScore);
            }
            if (playerType == PlayerType.Opponent)
            {
                opponentScore += number;
                levelUIController.ChangeScore(playerType, opponentScore);
            }
        }

        public void SwitchToLevel()
        {
            switch (GameProgressStatic.GameRegime)
            {
                case GameRegime.SingleNormal:
                    inputManager.TurnOn(true);
                    break;
                case GameRegime.Hotseat:
                    inputManager.TurnOn(false);
                    break;
            }
        }

        void EndRound(bool firstWin)
        {
            GameProgressStatic.Is1Pwin = firstWin;
            if ((GameProgressStatic.GameRegime == GameRegime.SingleNormal) || (GameProgressStatic.GameRegime == GameRegime.SingleHard))
            {
                if (firstWin)
                {
                    GameProgressStatic.Strike += 1;
                }
                else
                {
                    if (GameProgressStatic.Strike > GameProgressStatic.BestStrike)
                    {
                        GameProgressStatic.BestStrike = GameProgressStatic.Strike;
                    }
                    GameProgressStatic.Strike = 0;
                }
            }
            inputManager.TurnOff();
            Time.timeScale = 0.2f;
            isEnding = true;
        }
    }
}