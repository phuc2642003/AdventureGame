using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    public class FireBullet : MonoBehaviour
    {
        public Player player;
        public Transform firePoint;
        public void Fire()
        {
            if (!player || !firePoint) return;
            var bulletObject = BulletPooler.Ins.GetPooledBullet();
            if(bulletObject!=null)
            {
                bulletObject.transform.position = firePoint.position;
                bulletObject.SetActive(true);
                Bullet bullet = bulletObject.GetComponent<Bullet>();
                if (bullet != null)
                {
                    bullet.speed = player.IsFacingLeft && bullet.speed > 0 ? -bullet.speed : bullet.speed;
  
                    bullet.owner = player;
                    Debug.Log(bulletObject.transform.position);
                    GameManager.Ins.ReduceBullet();
                }
                
            }  
        }
    }
}

