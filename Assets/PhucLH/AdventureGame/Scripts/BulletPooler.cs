using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    public class BulletPooler : Singleton<BulletPooler>
    {
        public List<GameObject> bulletPool;
        public int poolSize = 10;
        public GameObject bulletPrb;
        void Start()
        {
            bulletPool = new List<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject bullet;
                bullet = Instantiate(bulletPrb);
                bullet.SetActive(false);
                bulletPool.Add(bullet);
            }    
        }
        public GameObject GetPooledBullet()
        {
            foreach(GameObject obj in bulletPool)
            {
                if(!obj.activeInHierarchy)
                {
                    return obj;
                }    
            }
            return null;
        }    

    }

}
