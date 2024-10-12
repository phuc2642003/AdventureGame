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
                GameData.Ins.musicVol = AudioController.ins.musicVolume;
                GameData.Ins.soundVol = AudioController.ins.sfxVolume;
                
                GameData.Ins.SaveData();
                LevelManager.Ins.Init();
            }
            
            AudioController.ins.SetMusicVolume(GameData.Ins.musicVol);
            AudioController.ins.SetSoundVolume(GameData.Ins.soundVol);
            
            Pref.isFirstTime = false;
            AudioController.ins.PlayMusic(AudioController.ins.menus);
        }
    }

}
