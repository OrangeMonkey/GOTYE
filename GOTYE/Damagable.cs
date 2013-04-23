using OpenTKTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

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

        public override bool ShouldRemove(System.Drawing.Rectangle bounds)
        {
            return HP <= 0 || base.ShouldRemove(bounds);
        }

        public virtual void Damage(int amount, Vector2 hitpos, Vector2 force)
        {
            if (HP < amount)
            {
                amount = HP;
            }
            HP = HP - amount;
            OnDamaged(amount, hitpos, force);
            if (HP <= 0)
            {                           
                OnKilled();
            }
        }

        protected virtual void OnDamaged(int amount, Vector2 hitpos, Vector2 force)
        {

        }

        protected virtual void OnKilled()
        {

        }

    }
}
