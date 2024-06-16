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
                if(bullet.speed>0)
                {
                    bullet.speed = player.IsFacingLeft ? -bullet.speed : bullet.speed;
                }    
                else
                {
                    bullet.speed = player.IsFacingLeft ? bullet.speed : -bullet.speed;
                }    
                bullet.owner = player;
            }  
        }
    }
}

