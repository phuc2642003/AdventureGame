using PhucLH.AdventureGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using UnityEngine.SceneManagement;

namespace PhucLH.AdventureGame
{
    public class GameManager : Singleton<GameManager>
    {
        public GamePlaySetting setting;
        public Player player;
        public FreeParallax map;

        private StateMachine<GameState> fsm;

        private int currentLive;
        private int currentCoin;
        private int currentKey;
        private int currentBullet;
        private float playTime;
        private int goalStar;

        public int CurrentLive { get => currentLive; set => currentLive = value; }
        public int CurrentCoin { get => currentCoin; set => currentCoin = value; }
        public int CurrentKey { get => currentKey; set => currentKey = value; }
        public int CurrentBullet { get => currentBullet; set => currentBullet = value; }
        public int GoalStar { get => goalStar; }
        public StateMachine<GameState> Fsm { get => fsm;}
        public float PlayTime { get => playTime; set => playTime = value; }

        // Start is called before the first frame update
        public override void Awake()
        {
            MakeSingleton(false);
            fsm = StateMachine<GameState>.Initialize(this);
            fsm.ChangeState(GameState.Playing);
        }

        private void LoadData()
        {
            currentLive = setting.startingLive;
            currentBullet = setting.startBullet;

            if(GameData.Ins.live !=0)
            {
                currentLive= GameData.Ins.live;
            }    
            if(GameData.Ins.bullet!=0)
            {
                currentBullet = GameData.Ins.bullet;
            }
            if (GameData.Ins.key != 0)
            {
                currentKey = GameData.Ins.key;
            }
            if (GameData.Ins.hp != 0)
            {
                player.CurHp = GameData.Ins.hp;
            }

            Vector3 checkPoint = GameData.Ins.GetCheckPoint(LevelManager.Ins.CurrentLevelId);
            if(checkPoint!= Vector3.zero)
            {
                player.transform.position = checkPoint;
            }

            float gamePlayTime = GameData.Ins.GetPlayTime(LevelManager.Ins.CurrentLevelId);
            if(gamePlayTime>0)
            {
                playTime = gamePlayTime;
            }    

        }    
        public void BackToCheckPoint()
        {
            player.transform.position = GameData.Ins.GetCheckPoint(LevelManager.Ins.CurrentLevelId);
        }    
        public void Revive()
        {
            currentLive--;
            player.CurHp = player.stat.hp;
            GameData.Ins.hp = player.CurHp;
            GameData.Ins.SaveData();
        }    
        public void AddCoins(int coins)
        {
            currentCoin += coins;
            GameData.Ins.coin += coins;
            GameData.Ins.SaveData();
        }    
        public void Replay()
        {
            SceneController.Ins.LoadLevelScene(LevelManager.Ins.CurrentLevelId);
        }    
        public void SetMapSpeed(float speed)
        {
            if(map)
            {
                map.Speed = speed;
            }    
        }
        #region FSM
        protected void Starting_Enter() { }
        protected void Starting_Update() { }
        protected void Starting_Exit() { }
        protected void Playing_Enter() { }
        protected void Playing_Update() { }
        protected void Playing_Exit() { }
        protected void LevelClear_Enter() { }
        protected void LevelClear_Update() { }
        protected void LevelClear_Exit() { }
        protected void LevelFail_Enter() { }
        protected void LevelFail_Update() { }
        protected void LevelFail_Exit() { }

        #endregion
    }
}

