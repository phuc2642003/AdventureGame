using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    public class Collectable : MonoBehaviour
    {
        public CollectableType type;
        public int minBonus;
        public int maxBonus;
        public AudioClip collisionSfx;
        public GameObject destroyVfxPb;

        protected int bonus;
        protected Player player;

        private void Start()
        {
            player = GameManager.Ins.player;
            if (player == null) return;

            bonus = Random.Range(minBonus, maxBonus);

            Init();
        }

        public virtual void Init()
        {
            DestroyWhenLevelPassed();
        }
        protected virtual void TriggerHandle()
        {

        }
        protected void DestroyWhenLevelPassed()
        {
            if (GameData.Ins.IsLevelPassed(LevelManager.Ins.CurrentLevelId))
            {
                Destroy(gameObject);
            }
        }
        public void Trigger()
        {
            TriggerHandle();

            if(destroyVfxPb)
            {
                Instantiate(destroyVfxPb, transform.position, Quaternion.identity);
            }
            //Play sound
            AudioController.ins.PlaySound(collisionSfx);
            
            gameObject.SetActive(false);
        }
    }

}
