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
        private void Start()
        {
            LoadData();
            StartCoroutine(CamFollowDelay());
            GUIManager.Ins.ShowMobileGamepad(setting.isOnMobile);
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
            
            GUIManager.Ins.UpdateLive(currentLive);
            GUIManager.Ins.UpdateHp(player.CurHp);
            GUIManager.Ins.UpdateCoin(currentCoin);
            GUIManager.Ins.UpdatePlayTime(Helper.TimeConvert(gamePlayTime));
            GUIManager.Ins.UpdateBullet(currentBullet);
            GUIManager.Ins.UpdateKey(currentKey);
            
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
            
            GUIManager.Ins.UpdateCoin(GameData.Ins.coin);
        }    
        public void Replay()
        {
            SceneController.Ins.LoadLevelScene(LevelManager.Ins.CurrentLevelId);
        }    
        public void NextLevel()
        {
            LevelManager.Ins.CurrentLevelId++;
            if(LevelManager.Ins.CurrentLevelId>=SceneManager.sceneCountInBuildSettings -1)
            {
                SceneController.Ins.LoadScene(GameScene.MainMenu.ToString());
            }    
            else
            {
                SceneController.Ins.LoadLevelScene(LevelManager.Ins.CurrentLevelId);
            }    
        }   
        public void SaveCheckPoint()
        {
            GameData.Ins.UpdatePlayTime(LevelManager.Ins.CurrentLevelId, playTime);
            GameData.Ins.UpdateCheckedPoint(LevelManager.Ins.CurrentLevelId, new Vector3(
                player.transform.position.x - 0.5f,
                player.transform.position.y + 0.5f,
                player.transform.position.z
                ));
            GameData.Ins.SaveData();
        }  
        
        public void Gameover()
        {
            fsm.ChangeState(GameState.LevelFail);
        }
        public void LevelFailed()
        {
            fsm.ChangeState(GameState.LevelFail);
            GameData.Ins.UpdateLevelScored(LevelManager.Ins.CurrentLevelId,
                Mathf.RoundToInt(playTime));

            GameData.Ins.SaveData();
            
            if (GUIManager.Ins.lvFailedDialog)
            {
                GUIManager.Ins.lvFailedDialog.Show(true);
            }
        }
        public void LevelClear()
        {
            fsm.ChangeState(GameState.LevelClear);
            GameData.Ins.UpdateLevelScored(LevelManager.Ins.CurrentLevelId,
                Mathf.RoundToInt(playTime));
            goalStar = LevelManager.Ins.GetLevel.goal.GetStar(Mathf.RoundToInt(playTime));
            GameData.Ins.SaveData();

            if (GUIManager.Ins.lvClearedDialog)
            {
                GUIManager.Ins.lvClearedDialog.Show(true);
            }
        }
        private IEnumerator CamFollowDelay()
        {
            yield return new WaitForSeconds(0.3f);
            CameraFollow.ins.target = player.transform;
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
        protected void Playing_Update() {
            if (GameData.Ins.IsLevelPassed(LevelManager.Ins.CurrentLevelId)) return;

            playTime += Time.deltaTime;
            
            GUIManager.Ins.UpdatePlayTime(Helper.TimeConvert(playTime));
        }
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

