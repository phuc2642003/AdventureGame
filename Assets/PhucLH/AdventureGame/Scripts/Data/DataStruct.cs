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
    }
    [System.Serializable]
    public class ShopItem
    {
        public CollectableType type;
        public int price;
        public Sprite preview;
    }

}
