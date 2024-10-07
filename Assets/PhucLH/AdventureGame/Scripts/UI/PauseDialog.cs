using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    public class PauseDialog : Dialog
    {
        public override void Show(bool isShow)
        {
            base.Show(isShow);
            Time.timeScale = 0f;
        }

        public void Replay()
        {
            Close();
            SceneController.Ins.LoadLevelScene(LevelManager.Ins.CurrentLevelId);
        }

        public override void Close()
        {
            Time.timeScale = 1f;
            base.Close();
        }

        public void Exit()
        {
            Close();
            SceneController.Ins.LoadScene(GameScene.MainMenu.ToString());
        }
        
        public void OpenSetting()
        {
            Close();
            if (GUIManager.Ins.settingDialog)
            {
                GUIManager.Ins.settingDialog.Show(true);
            }
        }
    }
}

