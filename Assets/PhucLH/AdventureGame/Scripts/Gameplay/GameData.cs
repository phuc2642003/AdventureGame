using JetBrains.Annotations;
using PhucLH.AdventureGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Firebase.Database;
using Firebase.Extensions;

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
        
        private DatabaseReference dbRef;

        public override void Awake()
        {
            base.Awake();
            dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        }

        public void SaveData()
        {
            if (FirebaseAuthentication.Ins.user == null) return;

            GameDataModel gameDataModel = new GameDataModel(this);
            string json = JsonUtility.ToJson(gameDataModel);
            dbRef.Child("users").Child(FirebaseAuthentication.Ins.user.UserId).SetRawJsonValueAsync(json);
        }

        public void LoadData()
        {
            if (FirebaseAuthentication.Ins.user == null) return;

            dbRef.Child("users").Child(FirebaseAuthentication.Ins.user.UserId).GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted || task.IsCanceled) return;

                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    GameDataModel loadedData = JsonUtility.FromJson<GameDataModel>(snapshot.GetRawJsonValue());
                    ApplyData(loadedData);
                }
            });
        }

        private void ApplyData(GameDataModel data)
        {
            coin = data.coin;
            currentLevelId = data.currentLevelId;
            musicVol = data.musicVol;
            soundVol = data.soundVol;
            hp = data.hp;
            live = data.live;
            bullet = data.bullet;
            key = data.key;

            checkPoints.Clear();
            foreach (var point in data.checkPoints)
            {
                checkPoints.Add(point.ToVector3());
            }

            levelUnlocked = new List<bool>(data.levelUnlocked);
            levelPasseds = new List<bool>(data.levelPasseds);
            playTimes = new List<float>(data.playTimes);
            completeTimes = new List<float>(data.completeTimes);
        }

        public bool IsFirstTime()
        {
            bool isFirstTime = false;
            dbRef.Child("users").Child(FirebaseAuthentication.Ins.user.UserId).Child("coin").GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    return;
                }
            
                DataSnapshot snapshot = task.Result;
            
                if (snapshot.Exists)
                {
                    isFirstTime = false;
                }
                else
                {
                    isFirstTime = true;
                }
            });
            return isFirstTime;
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
            SaveData();
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
            if (completeTime < oldCompleteTime || oldCompleteTime == 0)
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
