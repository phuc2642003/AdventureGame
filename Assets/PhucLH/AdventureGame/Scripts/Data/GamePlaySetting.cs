using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    [CreateAssetMenu(fileName ="GameplaySetting", menuName ="PhucLH/GameplaySetting")]
    public class GamePlaySetting : ScriptableObject
    {
        public bool isOnMobile;
        public int startingLive;
        public int startBullet;
    }
}

