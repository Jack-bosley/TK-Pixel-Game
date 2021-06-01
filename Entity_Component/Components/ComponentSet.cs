using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PixelGame.Entity_Component
{
    public class ComponentSet : BaseComponent, IEnumerable<BaseComponent>
    {
        private List<BaseComponent> components;

        public ComponentSet() : base()
        {
            this.components = new List<BaseComponent>();
        }
        public ComponentSet(BaseComponent _components) : base()
        {
            Components.Add(_components);
        }
        public ComponentSet(IEnumerable<BaseComponent> _components) : base()
        {
            Components.AddRange(_components);
        }


        public List<BaseComponent> Components
        {
            get => components;
            set => components = value;
        }


        public IEnumerator<BaseComponent> GetEnumerator()
        {
            return components.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return components.GetEnumerator();
        }
    }
}
