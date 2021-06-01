using System;
using System.Collections.Generic;
using System.Text;

namespace PixelGame.Entity_Component
{
    public class BaseComponent : Base
    {
        private BaseEntity parent;
        private bool isUnique = false;
        private bool isRemovable = true;

        public BaseComponent() : base()
        {

        }
        ~BaseComponent()
        {
            Dispose();
        }
        public override void Dispose()
        {
            if (!isDisposed)
            {
                Parent.RemoveComponentForced(Id);

                base.Dispose();
            }
        }

        public BaseEntity Parent
        {
            get => parent;
            internal set => parent = value;
        }
        public bool IsUnique
        {
            get => isUnique;
            internal set => isUnique = value;
        }
        public bool IsRemovable
        {
            get => isRemovable;
            internal set => isRemovable = value;
        }
    }
}
