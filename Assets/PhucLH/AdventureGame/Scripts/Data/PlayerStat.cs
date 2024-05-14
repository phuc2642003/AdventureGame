using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    [CreateAssetMenu(fileName = "Player Stat", menuName ="PhucLH/Player Stat")]
    public class PlayerStat : ActorStat
    {
        public float jumpForce;
        public float flyingSpeed;
        public float ladderSpeed;
        public float swimSpeed;
        public float attackRate;
    }
}

