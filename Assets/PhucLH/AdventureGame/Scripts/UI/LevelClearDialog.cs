using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PhucLH.AdventureGame
{
    public class LevelClearDialog : Dialog
    {
        public Image[] starImgs;
        public Sprite activeStar;
        public Sprite deactiveStar;

        public Text liveCountingTxt;
        public Text hpCountingTxt;
        public Text timeCountingTxt;
        public Text coinCountingTxt;

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

            if (liveCountingTxt)
            {
                liveCountingTxt.text = $"x {GameManager.Ins.CurrentLive}";
            }

            if (hpCountingTxt)
            {
                hpCountingTxt.text = $"x {GameManager.Ins.player.CurHp}";
            }

            if (timeCountingTxt)
            {
                timeCountingTxt.text = $"x {Helper.TimeConvert(GameManager.Ins.PlayTime)}";
            }

            if (coinCountingTxt)
            {
                coinCountingTxt.text = $"x {GameManager.Ins.CurrentCoin}";
            }
        }
        public void Replay()
        {
            Close();
            GameManager.Ins.Replay();
        }

        public void NextLevel()
        {
            Close();
            GameManager.Ins.NextLevel();
        }
    }
}

