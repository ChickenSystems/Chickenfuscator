using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Chickenfuscator
{
    sealed class ChickenSyntaxRewriter : CSharpSyntaxRewriter
    {
        readonly SemanticModel semanticModel;

        public ChickenSyntaxRewriter (SemanticModel semanticModel)
            => this.semanticModel = semanticModel;

        public override SyntaxToken VisitToken (SyntaxToken token)
        {
            if (token.Kind () != SyntaxKind.IdentifierToken)
                return token;

            // TODO: chickenalysis

            return SyntaxFactory.Identifier (
                token.LeadingTrivia,
                "Chicken",
                token.TrailingTrivia);
        }
    }
}