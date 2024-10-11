using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    public class MainMenuController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            if (!Pref.isFirstTime)
            {
                GameData.Ins.LoadData();
            }    
            else
            {
                //GameData.Ins.SaveData();
                LevelManager.Ins.Init();
            }
            Pref.isFirstTime = false;
            AudioController.ins.PlayMusic(AudioController.ins.menus);
        }
    }

}
