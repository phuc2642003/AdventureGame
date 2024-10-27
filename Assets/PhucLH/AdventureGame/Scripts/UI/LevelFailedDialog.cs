using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace PhucLH.AdventureGame
{
    public class LevelFailedDialog : Dialog
    {
        public Text m_timeCountingTxt;
        public Text m_coinCountingTxt;

        public override void Show(bool isShow)
        {
            base.Show(isShow);

            if (m_timeCountingTxt)
            {
                m_timeCountingTxt.text = $"{Helper.TimeConvert(GameManager.Ins.PlayTime)}";
            }

            if(m_coinCountingTxt)
            {
                m_coinCountingTxt.text = $"{GameManager.Ins.CurrentCoin}";
            }

            //Time.timeScale = 0f;
        }
        
        public void Replay()
        {
            Close();
            GameManager.Ins.Replay();
            Time.timeScale = 1f;
        }

        public void BackToMenu()
        {
            SceneController.Ins.LoadScene(GameScene.MainMenu.ToString());
            Time.timeScale = 1f;
        }
    }

}
