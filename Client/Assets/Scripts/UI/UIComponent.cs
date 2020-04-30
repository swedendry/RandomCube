using System;
using UnityEngine;

namespace UI
{
    public class Props<T>
    {
        public int index;
        public T data;
    }

    public class UIComponent : MonoBehaviour
    {
        public Action<string> OnEvent;

        public virtual void Empty()
        {
            gameObject?.SetActive(false);
        }

        public virtual void Upsert()
        {

        }

        public virtual void Event(string param)
        {
            OnEvent?.Invoke(param);
        }
    }

    public class UIComponent<T> : UIComponent
    {
        public Action<bool, Props<T>> OnEventProps;

        [HideInInspector]
        public Props<T> props;

        protected bool isSelected;

        public override void Empty()
        {
            isSelected = false;

            gameObject?.SetActive(false);
        }

        public virtual void Upsert(T data)
        {
            Upsert(0, data);
        }

        public virtual void Upsert(int index, T data)
        {
            Empty();

            props = new Props<T>()
            {
                index = index,
                data = data,
            };

            if (data == null)
                return;

            gameObject?.SetActive(true);
        }

        public virtual void Event()
        {
            isSelected = !isSelected;

            OnEventProps?.Invoke(isSelected, props);
        }
    }
}