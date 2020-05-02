using Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI
{
    public class UIContainer : MonoBehaviour
    {
        public Action<string> OnEvent;

        private List<UIComponent> components = new List<UIComponent>();
        private List<UIContainer> containers = new List<UIContainer>();

        protected virtual void Awake()
        {
            components = GetComponentsInChildren<UIComponent>(true).Where(x => x.transform.parent.GetComponentInParent<UIContainer>() == this).ToList();
            components.ForEach(x => { x.OnEvent = Event; });

            containers = GetComponentsInChildren<UIContainer>(true).Where(x => x.transform.parent.GetComponentInParent<UIContainer>() == this).ToList();
            containers.ForEach(x => { x.OnEvent = Event; });
        }

        public virtual void Empty()
        {
            components.ForEach(x => x.Empty());
            containers.ForEach(x => x.Empty());

            gameObject?.SetVisible(false);
        }

        public virtual void Upsert()
        {
            gameObject?.SetVisible(true);
        }

        public virtual void Event(string param)
        {
            OnEvent?.Invoke(param);
        }

        protected virtual T GetUIComponent<T>() where T : UIComponent
        {
            return components.Find(x => x.GetType() == typeof(T)) as T;
        }

        protected virtual List<T> GetUIComponents<T>() where T : UIComponent
        {
            return components.FindAll(x => x.GetType() == typeof(T)).Cast<T>().ToList();
        }

        protected virtual T GetUIContainer<T>() where T : UIContainer
        {
            return containers.Find(x => x.GetType() == typeof(T)) as T;
        }

        protected virtual List<T> GetUIContainers<T>() where T : UIContainer
        {
            return containers.FindAll(x => x.GetType() == typeof(T)).Cast<T>().ToList();
        }
    }

    public class UIContainer<U, T> : UIContainer where U : UIComponent<T>
    {
        public Action<bool, Props<T>> OnEventProps;

        [HideInInspector]
        public List<Props<T>> selectProps = new List<Props<T>>();

        protected override void Awake()
        {
            base.Awake();

            var components = GetUIComponents<U>();
            components.ForEach(x => { x.OnEventProps = Event; });
        }

        public override void Empty()
        {
            base.Empty();

            selectProps.Clear();
        }

        public virtual void Event(bool isSelected, Props<T> props)
        {
            OnEventProps?.Invoke(isSelected, props);
        }
    }
}
