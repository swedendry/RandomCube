using Extension;
using UnityEngine;

namespace UI
{
    public class SlotComponent<T> : UIComponent<T>
    {
        public GameObject selected_obj;
        public GameObject lock_obj;

        public override void Empty()
        {
            base.Empty();

            Selected(false);
            Lock(false);
        }

        public override void Upsert(int index, T data)
        {
            base.Upsert(index, data);

            Lock(IsLock());
        }

        public override void Event()
        {
            if (lock_obj.activeSelf)
                return; //lock 클릭 안됌

            base.Event();
        }

        public virtual void Selected(bool isSelected)
        {
            selected_obj?.SetVisible(isSelected);
        }

        public virtual void Lock(bool isLock)
        {
            lock_obj?.SetVisible(isLock);
        }

        protected virtual bool IsLock()
        {
            return false;
        }
    }
}