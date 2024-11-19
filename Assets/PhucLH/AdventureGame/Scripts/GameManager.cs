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

        public int CurrentLive
        {
            get => currentLive;
            set
            {
                currentLive = value;
                this.PostEvent(EventID.OnLiveChange);
            }
        }
        public int CurrentCoin
        {
            get => currentCoin;
            set
            {
                currentCoin = value;
                this.PostEvent(EventID.OnCoinChange);
            }
            
        }
        public int CurrentKey
        {
            get => currentKey;
            set
            {
                currentKey = value;
                this.PostEvent(EventID.OnKeyChange);
            }
        }
        public int CurrentBullet
        {
            get => currentBullet;
            set
            {
                currentBullet = value;
                this.PostEvent(EventID.OnBulletChange);
            }
        }

        public int GoalStar { get => goalStar; }
        public StateMachine<GameState> Fsm { get => fsm;}

        public float PlayTime
        {
            get => playTime;
            set
            {
                playTime = value;
                this.PostEvent(EventID.OnPlayTimeChange);
            }
            
        }

        // Start is called before the first frame update
        public override void Awake()
        {
            MakeSingleton(false);
            fsm = StateMachine<GameState>.Initialize(this);
            fsm.ChangeState(GameState.Playing);
        }
        private void Start()
        {
            this.RegisterListener(EventID.OnBulletChange, (param)=>OnBulletChange());
            this.RegisterListener(EventID.OnCoinChange, (param)=>OnCoinChange());
            this.RegisterListener(EventID.OnHpChange, (param)=>OnHpChange());
            this.RegisterListener(EventID.OnLiveChange, (param)=>OnLiveChange());
            this.RegisterListener(EventID.OnKeyChange, (param)=>OnKeyChange());
            this.RegisterListener(EventID.OnPlayTimeChange, (param)=>GUIManager.Ins.UpdatePlayTime(Helper.TimeConvert(playTime)));
            
            LoadData();
            StartCoroutine(CamFollowDelay());
            GUIManager.Ins.ShowMobileGamepad(setting.isOnMobile);
            AudioController.ins.PlayBackgroundMusic();
        }
        private void OnBulletChange()
        {
            GUIManager.Ins.UpdateBullet(CurrentBullet);
            GameData.Ins.bullet = CurrentBullet;
            GameData.Ins.SaveData();
        }
        private void OnCoinChange()
        {
            GUIManager.Ins.UpdateCoin(CurrentCoin);
        }
        private void OnHpChange()
        {
            GUIManager.Ins.UpdateHp(player.CurHp);
            GameData.Ins.hp = player.CurHp;
            GameData.Ins.SaveData();
        }   
        private void OnLiveChange()
        {
            GUIManager.Ins.UpdateLive(CurrentLive);
            GameData.Ins.live = CurrentLive;
            GameData.Ins.SaveData();
        }
        private void OnKeyChange()
        {
            GUIManager.Ins.UpdateKey(CurrentKey);
            GameData.Ins.key = CurrentKey;
            GameData.Ins.SaveData();
        }
        private void LoadData()
        {
            CurrentLive = setting.startingLive;
            CurrentBullet = setting.startBullet;

            if(GameData.Ins.live !=0)
            {
                CurrentLive= GameData.Ins.live;
            }    
            if(GameData.Ins.bullet!=0)
            {
                CurrentBullet = GameData.Ins.bullet;
            }
            if (GameData.Ins.key != 0)
            {
                CurrentKey = GameData.Ins.key;
            }
            if (GameData.Ins.hp != 0)
            {
                player.CurHp = GameData.Ins.hp;
            }

            CurrentCoin = 0;
            CurrentKey = 0;
            Vector3 checkPoint = GameData.Ins.GetCheckPoint(LevelManager.Ins.CurrentLevelId);
            if(checkPoint!= Vector3.zero)
            {
                player.transform.position = checkPoint;
            }
        }    
        public void BackToCheckPoint()
        {
            player.transform.position = GameData.Ins.GetCheckPoint(LevelManager.Ins.CurrentLevelId);
        }
        
        public void ResetGameplayInfo()
        {
            GameData.Ins.UpdateCheckedPoint(LevelManager.Ins.CurrentLevelId, LevelManager.Ins.GetLevel.firstCheckpoint);
            playTime = 0;
            CurrentCoin = 0;
            GameData.Ins.live = 1;
            GameData.Ins.hp = 3;
            GameData.Ins.bullet = 7;
            GameData.Ins.SaveData();
        }
        public void Revive()
        {
            CurrentLive--;
            player.CurHp = player.stat.hp;
            GameData.Ins.hp = player.CurHp;
            GameData.Ins.live = CurrentLive;
            BackToCheckPoint();
        }    
        public void AddCoins(int coins)
        {
            CurrentCoin += coins;
        }    
        public void Replay()
        {
            SceneController.Ins.LoadLevelScene(LevelManager.Ins.CurrentLevelId);
            ResetGameplayInfo();
            Time.timeScale = 1f;
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
        }  
        
        public void Gameover()
        {
            fsm.ChangeState(GameState.LevelFail);
        }
        public void LevelFailed()
        {
            fsm.ChangeState(GameState.LevelFail);
            GameData.Ins.coin += CurrentCoin;
            if (GUIManager.Ins.lvFailedDialog)
            {
                GUIManager.Ins.lvFailedDialog.Show(true);
            }
            AudioController.ins.PlaySound(AudioController.ins.fail);
            ResetGameplayInfo();
            
        }
        public void LevelClear()
        {
            fsm.ChangeState(GameState.LevelClear);
            GameData.Ins.UpdateLevelScored(LevelManager.Ins.CurrentLevelId,
                Mathf.RoundToInt(playTime));
            goalStar = LevelManager.Ins.GetLevel.goal.GetStar(Mathf.RoundToInt(playTime));
            GameData.Ins.coin += CurrentCoin;
            if (GUIManager.Ins.lvClearedDialog)
            {
                GUIManager.Ins.lvClearedDialog.Show(true);
            }
            AudioController.ins.PlaySound(AudioController.ins.missionComplete);
            ResetGameplayInfo();

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
        public void ReduceBullet()
        {
            CurrentBullet--;
            GameData.Ins.bullet = CurrentBullet;
        }
        #region FSM
        protected void Starting_Enter() { }
        protected void Starting_Update() { }
        protected void Starting_Exit() { }
        protected void Playing_Enter() { }
        protected void Playing_Update() {
            PlayTime += Time.deltaTime;
            
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

