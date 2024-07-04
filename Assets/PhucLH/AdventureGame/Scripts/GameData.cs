using JetBrains.Annotations;
using PhucLH.AdventureGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace PhucLH.AdventureGame
{
    public class GameData : Singleton<GameData>
    {
        public int coin;
        public int currentLevelId;
        public float musicVol;
        public float soundVol;
        public int hp;
        public int live;
        public int bullet;
        public int key;
        public List<Vector3> checkPoints;
        public List<bool> levelUnlocked;
        public List<bool> levelPasseds;
        public List<float> playTimes;
        public List<float> completeTimes;

        public override void Awake()
        {
            base.Awake();
        }
        public void SaveData()
        {
            Pref.GameData = JsonUtility.ToJson(this);
        }
        public void LoadData()
        {
            if (string.IsNullOrEmpty(Pref.GameData)) return;

            JsonUtility.FromJsonOverwrite(Pref.GameData, this);
        }
        public T GetValue<T>(List<T> dataList, int index)
        {
            if (dataList == null || dataList.Count <= 0 || index < 0 || index > dataList.Count) { }


            return dataList[index];
        }
        public void UpdateValue<T>(ref List<T> dataList, int index, T value)
        {
            if (dataList == null || index < 0) return;

            if (dataList.Count <= 0 || (dataList.Count > 0 && index >= dataList.Count))
            {
                dataList.Add(value);
            }
            else
            {
                dataList[index] = value;
            }
        }
        #region Level
        public bool GetLevelUnlocked(int id)
        {
            return GetValue<bool>(levelUnlocked, id);
        }
        public void UpdateLevelUnlocked(int id, bool isUnlocked)
        {
            UpdateValue<bool>(ref levelUnlocked, id, isUnlocked);
        }
        public bool GetLevelPassed(int id)
        {
            return GetValue<bool>(levelPasseds, id);
        }
        public void UpdateLevelPassed(int id, bool isPassed)
        {
            UpdateValue<bool>(ref levelPasseds, id, isPassed);
        }
        public float GetLevelScored(int levelId)
        {
            return GetValue<float>(completeTimes, levelId);
        }
        public void UpdateLevelScoredNoChecked(int levelId, float completeTime)
        {
            UpdateValue<float>(ref completeTimes, levelId, completeTime);
        }
        public void UpdateLevelScored(int levelId, float completeTime)
        {
            float oldCompleteTime = GetLevelScored(levelId);
            if (completeTime < oldCompleteTime)
            {
                UpdateValue<float>(ref completeTimes, levelId, completeTime);
            }
        }
        public Vector3 GetCheckPoint(int levelId)
        {
            return GetValue<Vector3>(checkPoints, levelId);
        }
        public void UpdateCheckedPoint(int id, Vector3 checkPoint)
        {
            UpdateValue<Vector3>(ref checkPoints, id, checkPoint);
        }
        public float GetPlayTime(int levelId)
        {
            return GetValue<float>(playTimes, levelId);
        }

        public void UpdatePlayTime(int levelId, float playTime)
        {
            UpdateValue<float>(ref playTimes, levelId, playTime);
        }

        public bool IsLevelUnlocked(int id)
        {
            if (levelUnlocked == null || levelUnlocked.Count <= 0) return false;

            return levelUnlocked[id];
        }

        public bool IsLevelPassed(int id)
        {
            if (levelPasseds == null || levelPasseds.Count <= 0) return false;

            return levelPasseds[id];
        }
        #endregion
    }

}
