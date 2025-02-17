using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    public class LevelManager : Singleton<LevelManager>
    {
        public LevelItem[] levels;
        private int currentLevelId;

        public int CurrentLevelId { get => currentLevelId; set => currentLevelId = value; }

        public LevelItem GetLevel
        {
            get => levels[currentLevelId];
        }

        public void Init()
        {
            if (levels == null || levels.Length == 0)
                return;
            
            for(int i=0; i<levels.Length;i++)
            {
                var level = levels[i];
                if(level !=null)
                {
                    if(i==0)
                    {
                        GameData.Ins.UpdateLevelUnlocked(i, true);
                        GameData.Ins.currentLevelId = i;
                    }    
                    else
                    {
                        GameData.Ins.UpdateLevelUnlocked(i, false);
                    }
                    GameData.Ins.UpdateLevelPassed(i, false);
                    GameData.Ins.UpdatePlayTime(i, 0f);
                    GameData.Ins.UpdateCheckedPoint(i, Vector3.zero);
                    GameData.Ins.UpdateLevelScoredNoChecked(i, 0);
                    GameData.Ins.SaveData();
                }    
            }    
        }
    }
}

