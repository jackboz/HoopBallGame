using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace HoopBall
{
    [RequireComponent(typeof(LevelUISwitcher))]
    public class LevelUIController : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI playerScoreLabel;
        [SerializeField] TextMeshProUGUI opponentScoreLabel;

        GameManager gameManager;
        LevelUISwitcher levelUISwitcher;

        bool isInit = false;

        public void Init(GameManager gameManager)
        {
            levelUISwitcher = GetComponent<LevelUISwitcher>();
            this.gameManager = gameManager;
            isInit = true;
        }

        void Start()
        {
            if (!isInit)
            {
                Debug.LogError("Level UI Controller hasn't being initialized");
                return;
            }
            if (levelUISwitcher == null)
            {
                Debug.LogError("Level UI Switcher is not set");
                return;
            }
            if (gameManager == null)
            {
                Debug.LogError("Game Manager is not set");
                return;
            }
            if (playerScoreLabel == null)
            {
                Debug.LogWarning("Player score label is not set");
                return;
            }
            if (opponentScoreLabel == null)
            {
                Debug.LogWarning("Opponent score label is not set");
                return;
            }
        }

        public void SwitchToStartUI()
        {
            levelUISwitcher.SwitchUI(LevelUISwitcher.LevelUIState.Start);
        }

        public void SwitchToLevelUI2P()
        {
            GameProgressStatic.GameRegime = GameRegime.Hotseat;
            levelUISwitcher.SwitchUI(LevelUISwitcher.LevelUIState.Level);
            //gameManager.SwitchToLevel(); called from Go label animation
        }

        public void SwitchToLevelUI1P()
        {
            GameProgressStatic.GameRegime = GameRegime.SingleNormal;
            levelUISwitcher.SwitchUI(LevelUISwitcher.LevelUIState.Level);
            //gameManager.SwitchToLevel(); called from Go label animation
        }

        public void SwitchToLevelUI1PHard()
        {
            GameProgressStatic.GameRegime = GameRegime.SingleHard;
            levelUISwitcher.SwitchUI(LevelUISwitcher.LevelUIState.Level);
            //gameManager.SwitchToLevel(); called from Go label animation
        }

        public void ChangeScore(PlayerType playerType, int number)
        {
            if (playerType == PlayerType.Player)
            {
                playerScoreLabel.SetText(number.ToString());
            }
            if (playerType == PlayerType.Opponent)
            {
                opponentScoreLabel.SetText(number.ToString());
            }
        }
    }
}