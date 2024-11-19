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
                Bullet bullet = bulletObject.GetComponent<Bullet>();
                if (bullet != null)
                {
                    bullet.speed = player.IsFacingLeft ? -Mathf.Abs(bullet.speed) : Mathf.Abs(bullet.speed);
                    bullet.owner = player;
                    bulletObject.SetActive(true);
                    Debug.Log(bulletObject.transform.position);
                    GameManager.Ins.ReduceBullet();
                }
                
            }  
        }
    }
}

