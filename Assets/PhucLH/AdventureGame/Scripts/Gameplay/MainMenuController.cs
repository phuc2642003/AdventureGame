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
            if (!GameData.Ins.IsFirstTime())
            {
                GameData.Ins.LoadData();
            }    
            else
            {
                GameData.Ins.musicVol = AudioController.ins.musicVolume;
                GameData.Ins.soundVol = AudioController.ins.sfxVolume;
                LevelManager.Ins.Init();
                GameData.Ins.SaveData();
                
                Debug.Log("First time loaded");
            }
            
            AudioController.ins.SetMusicVolume(GameData.Ins.musicVol);
            AudioController.ins.SetSoundVolume(GameData.Ins.soundVol);
            
            AudioController.ins.PlayMusic(AudioController.ins.menus);
        }
    }

}
