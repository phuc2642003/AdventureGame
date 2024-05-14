using PhucLH.AdventureGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    [CreateAssetMenu(fileName = "Enemy Stat", menuName = "PhucLH/Enemy Stat")]
    public class EnemyStat : ActorStat
    {
        public float chasingSpeed;
    }
}

