using System;
using PCompiler;

namespace SkyrimPapCompilerExtensionFramework
{
    internal class Program
    {
	    public static void Main(string[] args)
        {
            //Console.Write(myscript);
            //testme2.OutputAST("testme.dot", myscript.kAST);
            PapyrusCompiler.Program.Main(args);
        }
    }
}