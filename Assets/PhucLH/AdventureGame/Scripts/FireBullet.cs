using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    public class FireBullet : MonoBehaviour
    {
        public Player player;
        public Transform firePoint;
        public Bullet bulletPb;

        private float currentSpeed;

        public void Fire()
        {
            if (!bulletPb || !player || !firePoint) return;

            currentSpeed = player.IsFacingLeft ? -bulletPb.speed : bulletPb.speed;
            var bulletClone = Instantiate(bulletPb, firePoint.position, Quaternion.identity);
            bulletClone.speed = currentSpeed;
            bulletClone.owner = player;
        }
    }
}

