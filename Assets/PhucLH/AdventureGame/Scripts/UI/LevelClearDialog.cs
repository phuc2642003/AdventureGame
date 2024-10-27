using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PhucLH.AdventureGame
{
    public class LevelClearDialog : Dialog
    {
        public Image[] starImgs;
        public Sprite activeStar;
        public Sprite deactiveStar;

        public Text coinText;
        public Text timeCountingTxt;
        public Text bestTimeTxt;

        public override void Show(bool isShow)
        {
            base.Show(isShow);

            if(starImgs != null && starImgs.Length > 0)
            {
                for (int i = 0; i < starImgs.Length; i++)
                {
                    var star = starImgs[i];
                    if (star)
                    {
                        star.sprite = deactiveStar;
                    }
                }

                for (int i = 0; i < GameManager.Ins.GoalStar; i++)
                {
                    var star = starImgs[i];
                    if (star)
                    {
                        star.sprite = activeStar;
                    }
                }
            }

            if (timeCountingTxt)
            {
                timeCountingTxt.text = $"x {Helper.TimeConvert(GameManager.Ins.PlayTime)}";
            }

            if (bestTimeTxt)
            {
                bestTimeTxt.text = $"x {Helper.TimeConvert(GameData.Ins.GetLevelScored(LevelManager.Ins.CurrentLevelId))}";
            }

            if (coinText)
            {
                coinText.text = $"{GameManager.Ins.CurrentCoin}";
            }
            //Time.timeScale = 0f;
        }
        public void Replay()
        {
            Close();
            GameManager.Ins.Replay();
            Time.timeScale = 1f;
        }

        public void NextLevel()
        {
            // Close();
            // GameManager.Ins.NextLevel();
            // Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }
    }
}

