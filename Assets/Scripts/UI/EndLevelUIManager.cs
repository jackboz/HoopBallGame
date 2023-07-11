using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace HoopBall
{
    public class EndLevelUIManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI winLabel;
        [SerializeField] GameObject strikePanel;
        TextMeshProUGUI strikeLabel;
        TextMeshProUGUI strikeValue;

        void Start()
        {
            if (winLabel == null)
            {
                Debug.LogError("Win label is not set");
            }
            if (strikePanel == null)
            {
                Debug.LogError("Strike Panel is not set");
            }
            strikeLabel = strikePanel.transform.Find("StrikeLabel")?.GetComponent<TextMeshProUGUI>();
            if (strikeLabel == null)
            {
                Debug.LogError("Strike Panel does not have StrikeLabel TextMeshProUGUI child");
            }
            strikeValue = strikePanel.transform.Find("StrikeValue")?.GetComponent<TextMeshProUGUI>();
            if (strikeValue == null)
            {
                Debug.LogError("Strike Panel does not have StrikeValue TextMeshProUGUI child");
            }

            switch (GameProgressStatic.GameRegime)
            {
                case GameRegime.SingleNormal:
                case GameRegime.SingleHard:
                    if (GameProgressStatic.Is1Pwin)
                    {
                        winLabel.SetText("WIN!");
                        strikeValue.SetText(GameProgressStatic.Strike.ToString());
                    }
                    else
                    {
                        string levelType = GameProgressStatic.GameRegime == GameRegime.SingleNormal ? "NORMAL" : "HARD";
                        winLabel.SetText("FAILED!");
                        strikeLabel.SetText("Your best strike (" + levelType + ")");
                        int bestStrike = GameProgressStatic.GameRegime == GameRegime.SingleNormal ? GameProgressStatic.StrikeBest : GameProgressStatic.StrikeBestHard;
                        strikeValue.SetText(bestStrike.ToString());
                    }
                    break;
                case GameRegime.Hotseat:
                    if (GameProgressStatic.Is1Pwin)
                    {
                        strikeLabel.SetText("1ST PLAYER");
                    }
                    else
                    {
                        strikeLabel.SetText("2ND PLAYER!");
                    }
                    strikeValue.SetText(GameProgressStatic.Player1Wins.ToString() + ":" + GameProgressStatic.Player2Wins.ToString());
                    break;
                case GameRegime.Twohands:
                    winLabel.SetText("WIN!");
                    strikeLabel.SetText("Time is " + GameProgressStatic.TwoHandTime.ToString("F1") + "s");
                    strikeValue.SetText("Your best " + GameProgressStatic.TwoHandTimeBest.ToString("F1") + "s");
                    break;
            }
        }

        public void ContinueGame()
        {
            GameProgressStatic.ContinueGame = true;
            SceneManager.LoadScene("Level");
        }

        public void RestartGame()
        {
            if ((GameProgressStatic.GameRegime == GameRegime.SingleNormal) && (GameProgressStatic.Strike > GameProgressStatic.StrikeBest))
            {
                GameProgressStatic.StrikeBest = GameProgressStatic.Strike;
            }
            if ((GameProgressStatic.GameRegime == GameRegime.SingleHard) && (GameProgressStatic.Strike > GameProgressStatic.StrikeBestHard))
            {
                GameProgressStatic.StrikeBestHard = GameProgressStatic.Strike;
            }
            GameProgressStatic.Strike = 0;
            GameProgressStatic.ContinueGame = false;
            SceneManager.LoadScene("Level");
        }
    }
}
