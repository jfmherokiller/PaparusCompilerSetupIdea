using System;
using PCompiler;

namespace SkyrimPapCompilerExtensionFramework
{
    internal class Program
    {
	    public static void Main(string[] args)
        {
            var input = "alert( 'Hello, world!' );";

            var chars = new Antlr.Runtime.ANTLRStringStream(input);
            var lexer = new JavaScriptLexer(chars);
            //lexer.strictMode = false; // do not use js strictMode

            var tokens = new Antlr.Runtime.CommonTokenStream(lexer);
            var parser = new JavaScriptParser(tokens);
            var tree = parser.program();

            Console.WriteLine(tree.Tree.ToStringTree());
            Console.WriteLine(parser.kObjType);
            //Console.Write(myscript);
            //testme2.OutputAST("testme.dot", myscript.kAST);
            PapyrusCompiler.Program.Main(args);
        }
    }
}