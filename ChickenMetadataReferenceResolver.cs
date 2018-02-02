using System.Collections.Immutable;

using Microsoft.CodeAnalysis;

namespace Chickenfuscator
{
    sealed class ChickenMetadataReferenceResolver : MetadataReferenceResolver
    {
        public override ImmutableArray<PortableExecutableReference> ResolveReference (
            string reference, string baseFilePath, MetadataReferenceProperties properties)
            => ImmutableArray.Create (PortableExecutableReference.CreateFromFile (reference, properties));

        public override bool Equals (object other) => ((object)this).Equals (other);

        public override int GetHashCode () => ((object)this).GetHashCode ();
    }
}