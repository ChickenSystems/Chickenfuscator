using System;
using System.Linq;
using System.IO;

using Microsoft.CodeAnalysis.CSharp;

namespace Chickenfuscator
{
    sealed class Program
    {
        static int Main (string [] args)
        {
            var commandLine = CSharpCommandLineParser.Default.Parse (
                args,
                Environment.CurrentDirectory,
                System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory ());

            var compilation = CSharpCompilation.Create (
                commandLine.CompilationName
                    ?? Path.GetFileNameWithoutExtension (
                        commandLine.OutputFileName),
                commandLine.SourceFiles.Select (file =>
                    CSharpSyntaxTree.ParseText (
                        File.ReadAllText (file.Path),
                        commandLine.ParseOptions,
                        file.Path)),
                references: commandLine.ResolveMetadataReferences (
                    new ChickenMetadataReferenceResolver ()),
                options: commandLine.CompilationOptions);

            foreach (var tree in compilation.SyntaxTrees) {
                var chickenRewriter = new ChickenSyntaxRewriter (
                    compilation.GetSemanticModel (
                        tree,
                        ignoreAccessibility: true));

                var chickenRoot = chickenRewriter.Visit (tree.GetRoot ());

                chickenRoot.WriteTo (Console.Out);
                Console.WriteLine ();
            }

            return 0;
        }
    }
}