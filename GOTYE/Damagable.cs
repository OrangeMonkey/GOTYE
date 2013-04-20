using OpenTKTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOTYE
{
    abstract class Damagable:SpaceJunk
    {
        public int HP
        {
            protected set;
            get;
        }

        public Damagable(float x, float miny, float maxy, BitmapTexture2D texture, float scale)
            : base(x, miny, maxy, texture, scale)
        {            

        }

        public Damagable(float x, float y, BitmapTexture2D texture, float scale)
            : base(x, y, texture, scale)
        {

        }

        public virtual void Damage(int amount)
        {
            if (HP < amount)
            {
                amount = HP;
            }
            HP = HP - amount;
            if (HP > 0)
            {
                OnDamaged(amount);
            }

            else
            {
                OnKilled();
            }
        }

        protected virtual void OnDamaged(int amount)
        {

        }

        protected virtual void OnKilled()
        {

        }

    }
}
