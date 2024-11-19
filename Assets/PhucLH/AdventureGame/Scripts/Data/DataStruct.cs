using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down,
        None
    }

    public enum GameState
    {
        Starting,
        Playing,
        LevelClear,
        LevelFail
    }

    public enum GamePref
    {
        GameData,
        IsFirstTime
    }

    public enum GameTag
    {
        Player,
        Enemy,
        MovingPlatform,
        Thorn,
        Collectable,
        CheckPoint,
        Door,
        DeadZone
    }

    public enum GameScene
    {
        MainMenu,
        Gameplay,
        Level_
    }

    public enum SpriteOrder
    {
        Normal = 5,
        InWater = 1
    }

    public enum PlayerAnimState
    {
        SayHi,
        Walk,
        Jump,
        Onair,
        Land,
        Swim,
        FireBullet,
        Fly,
        FlyOnAir,
        SwimOnDeep,
        OnLadder,
        Dead,
        Idle,
        LadderIIdle,
        HammerAttack,
        GotHit
    }

    public enum EnemyAnimState
    {
        Moving,
        Chasing,
        GotHit,
        Dead
    }

    public enum DetectMethod
    {
        RayCast,
        CircleOverlap
    }

    public enum PlayerCollider
    {
        Default,
        Flying,
        InWater,
        None
    }

    public enum CollectableType
    {
        Hp,
        Live,
        Bullet,
        Key,
        None
    }
    [System.Serializable]
    public class LevelItem
    {
        public int price;
        public Sprite previewSprite;
        public Goal goal;
        public Vector3 firstCheckpoint;
    }
    [System.Serializable]
    public class ShopItem
    {
        public CollectableType type;
        public int price;
        public Sprite preview;
    }
    [System.Serializable]
    public class Goal
    {
        public int timeToGet1Star;
        public int timeToGet2Stars;
        public int timeToGet3Stars;

        public int GetStar(int time)
        {
            if(time<=timeToGet3Stars)
            {
                return 3;
            }    
            else if(time <=timeToGet2Stars)
            {
                return 2;
            }    
            else
            {
                return 1;
            }
        }
    }
    public enum EventID
    {
        None = 0,
        OnLiveChange,
        OnHpChange,
        OnBulletChange,
        OnCoinChange,
        OnKeyChange,
        OnPlayTimeChange
    } 
}
