using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson
{
    public class Weapon
    {
        int _bulletCapacity;
        int _bulletCount;
        public int BulletCapacity
        {
            get => _bulletCapacity;
            set
            {
                if (value > 0 && value >= BulletCount) { _bulletCapacity = value; }
            }
        }
        public int BulletCount
        {
            get => _bulletCount;
            set
            {
                if (value > 0 && value <= BulletCapacity)
                {
                    _bulletCount = value;
                }
            }
        }
        public enum FireMode { Single, Auto, Burst }
        public FireMode fmode;
        public Weapon(int bulletCapacity, int bulletCount, FireMode fireMode)
        {
            BulletCapacity = bulletCapacity;
            BulletCount = bulletCount;
            fmode = fireMode;
        }
        public void Shoot()
        {
            if (BulletCount > 0)
            {
                if (fmode == FireMode.Single)
                {
                    BulletCount--;
                }
                else if (fmode == FireMode.Auto)
                {
                    BulletCount = 0;
                }
                else if (fmode == FireMode.Burst && BulletCount > 2)
                {
                    BulletCount -= 3;
                }

            }
            else
            {
                throw new NotEnougBulletException("Your bullet is zero!");
            }
        }
        public void Fire()
        {
            BulletCount = 0;
        }
        public int GetRemainBulletCount()
        {
            return BulletCapacity - BulletCount;
        }
        public void Reload()
        {
            BulletCount = BulletCapacity;
        }
        public void ChangeFireMode(FireMode fireMode)
        {
            fmode = fireMode;
        }

    }
}
