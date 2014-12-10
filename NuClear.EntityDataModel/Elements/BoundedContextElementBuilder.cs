using NuClear.Metamodeling.Elements;
using NuClear.Metamodeling.Elements.Identities;

namespace NuClear.EntityDataModel
{
    public sealed class BoundedContextElementBuilder : MetadataElementBuilder<BoundedContextElementBuilder, BoundedContextElement>
    {
        private string _name;

        public BoundedContextElementBuilder Name(string name)
        {
            _name = name;
            return this;
        }

        public BoundedContextElementBuilder Elements(params EntityElement[] elements)
        {
            Childs(elements);
            return this;
        }

        protected override BoundedContextElement Create()
        {
            return new BoundedContextElement(IdBuilder.For<AdvancedSearchIdentity>(_name).AsIdentity(), Features);
        }
    }
}