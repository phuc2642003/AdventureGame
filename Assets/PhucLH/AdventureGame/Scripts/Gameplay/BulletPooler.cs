using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    public class BulletPooler : Singleton<BulletPooler>
    {
        public override void Awake()
        {
            MakeSingleton(false);
        }

        public List<GameObject> bulletPool;
        public int poolSize = 15;
        public GameObject bulletPrb;
        void Start()
        {
            bulletPool = new List<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject bullet = Instantiate(bulletPrb,gameObject.transform);
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
