using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoopBall
{
    public class LevelUISwitcher : MonoBehaviour
    {
        [SerializeField] GameObject startMenuPanel;
        [SerializeField] GameObject levelPanel;
        [SerializeField] GameObject secondPlayerButtons;
        [SerializeField] GameObject challengePanel;

        public enum LevelUIState
        {
            Start,
            Level
        }

        void Start()
        {
            if (startMenuPanel == null)
            {
                Debug.LogError("Menu Panel is not set");
            }
            if (levelPanel == null)
            {
                Debug.LogError("Level Panel is not set");
            }
            if (secondPlayerButtons == null)
            {
                Debug.LogError("Group with SecondPlayerButtons is not set");
            }
            if (challengePanel == null)
            {
                Debug.LogError("Challenge Panel is not set");
            }
        }

        public void SwitchUI(LevelUIState levelUIState)
        {
            switch (levelUIState)
            {
                case LevelUIState.Start:
                    startMenuPanel.SetActive(true);
                    levelPanel.SetActive(false);
                    break;
                case LevelUIState.Level:
                    startMenuPanel.SetActive(false);
                    levelPanel.SetActive(true);
                    switch (GameProgressStatic.GameRegime)
                    {
                        case GameRegime.SingleNormal:
                        case GameRegime.SingleHard:
                            secondPlayerButtons.SetActive(false);
                            challengePanel.SetActive(false);
                            break;
                        case GameRegime.Twohands:
                            secondPlayerButtons.SetActive(true);
                            challengePanel.SetActive(true);
                            break;
                        case GameRegime.Hotseat:
                            secondPlayerButtons.SetActive(true);
                            challengePanel.SetActive(false);
                            break;
                    }
                    break;
            }
        }
    }
}