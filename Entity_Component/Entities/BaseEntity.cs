using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using PixelGame.Core.Logging;

namespace PixelGame.Entity_Component
{
    public class BaseEntity : Base
    {
        private Dictionary<string, BaseComponent> components;
        private Dictionary<Type, List<string>> typeIndex;


        public BaseEntity() : base()
        {
            this.components = new Dictionary<string, BaseComponent>();
            this.typeIndex = new Dictionary<Type, List<string>>();

            AddComponentForced(new Transform());
        }
        public BaseEntity(BaseComponent startingComponent) : this()
        {
            AddComponent(startingComponent);
        }
        public BaseEntity(IEnumerable<BaseComponent> startingComponents) : this()
        {
            foreach (BaseComponent component in startingComponents)
                AddComponent(component);
        }
        public BaseEntity(params BaseComponent[] startingComponents) : this()
        {
            foreach (BaseComponent component in startingComponents)
                AddComponent(component);
        }
        ~BaseEntity()
        {
            Dispose();
        }
        public override void Dispose()
        {
            if (!isDisposed)
            {
                foreach (BaseComponent component in components.Values)
                {
                    component?.Dispose();
                }

                base.Dispose();
            }
        }

        public Dictionary<string, BaseComponent> Components
        {
            get => components;
        }


        public Transform Transform
        {
            get => GetComponent<Transform>() as Transform;
        }


        internal void AddComponentForced(BaseComponent component)
        {
            // Cannot add second transform
            Type componentType = component.GetType();

            // Set parentage
            component.Parent = this;
            components.Add(component.Id, component);

            // Insert into the type index
            if (typeIndex.ContainsKey(componentType))
                typeIndex[componentType].Add(component.Id);
            else
                typeIndex[componentType] = new List<string> { component.Id };
        }
        public void AddComponent(BaseComponent component)
        {
            // Cannot add second transform
            Type componentType = component.GetType();

            if (component.IsUnique)
            {
                if (typeIndex.ContainsKey(component.GetType()) && typeIndex[component.GetType()].Count > 0)
                {
                    Logger.Log(new WarningException($"Warning, entity {GetType().Name} ({Id}) already has a component of type {component.GetType().Name}"));
                    return;
                }
            }

            // Set parentage
            component.Parent = this;
            components.Add(component.Id, component);

            // Insert into the type index
            if (typeIndex.ContainsKey(componentType))
                typeIndex[componentType].Add(component.Id);
            else
                typeIndex[componentType] = new List<string> { component.Id };
        }
        public T[] GetComponents<T>() where T : BaseComponent
        {
            return GetComponents(typeof(T)) as T[];
        }
        public T GetComponent<T>() where T : BaseComponent
        {
            return GetComponent(typeof(T)) as T;
        }
        public BaseComponent GetComponent(string id)
        {
            return components[id];
        }
        public BaseComponent[] GetComponents(Type type)
        {
            // If component exists with type
            if (!typeIndex.ContainsKey(type))
                return new BaseComponent[0];

            // Find the components with that type + return array of them
            List<string> componentsWithType = typeIndex[type];
            BaseComponent[] returnList = new BaseComponent[componentsWithType.Count];

            for (int i = 0; i < returnList.Length; i++)
                returnList[i] = components[componentsWithType[i]];

            return returnList;
        }
        public BaseComponent GetComponent(Type type)
        {
            // If one exists with type
            if (!typeIndex.ContainsKey(type))
                return new ComponentSet();

            // Find components with type
            List<string> componentsWithType = typeIndex[type];

            // If just 1 with type then return it
            if (componentsWithType.Count == 1)
                return components[componentsWithType[0]];

            // If multiple, construct a component set and return that
            BaseComponent[] returnList = new BaseComponent[componentsWithType.Count];
            for (int i = 0; i < returnList.Length; i++)
                returnList[i] = components[componentsWithType[i]];

            return new ComponentSet(returnList);
        }


        internal void RemoveComponentForced(string id)
        {
            if (!components.ContainsKey(id))
                return;

            Type removeType = Components[id].GetType();
            components[id].Parent = null;
            typeIndex[removeType].Remove(id);
            components.Remove(id);

            if (typeIndex[removeType].Count == 0)
                typeIndex.Remove(removeType);
        }
        public void RemoveComponent(string id)
        {
            if (!components.ContainsKey(id))
                return;

            if (!components[id].IsRemovable)
            {
                Logger.Log(new WarningException($"Warning, component of type {components[id].GetType().Name} cannot be removed"));
                return;
            }

            Type removeType = Components[id].GetType();
            components[id].Parent = null;
            typeIndex[removeType].Remove(id);
            components.Remove(id);

            if (typeIndex[removeType].Count == 0)
                typeIndex.Remove(removeType);
        }
        public void RemoveComponents(Type type)
        {
            foreach (string id in typeIndex[type])
            {
                if (!components[id].IsRemovable)
                {
                    Logger.Log(new WarningException($"Warning, component of type {components[id].GetType().Name} cannot be removed"));
                    continue;
                }

                components[id].Parent = null;
                components.Remove(id);
            }

            if (typeIndex[type].Count == 0)
                typeIndex.Remove(type);
        }
        public void RemoveComponents<T>()
        {
            RemoveComponents(typeof(T));
        }
    }
}
