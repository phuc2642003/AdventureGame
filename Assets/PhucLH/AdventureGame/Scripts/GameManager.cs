using PhucLH.AdventureGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    public class GameManager : Singleton<GameManager>
    {
        public GamePlaySetting setting;
        public Player player;
        public FreeParallax map;

        // Start is called before the first frame update
        public override void Awake()
        {
            MakeSingleton(false);
        }

        public void SetMapSpeed(float speed)
        {
            if(map)
            {
                map.Speed = speed;
            }    
        }    
    }
}

