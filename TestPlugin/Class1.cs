using System;
using HarmonyLib;
using PluginLib;
namespace TestPlugin
{
    public class Class1 : IPlugin
    {
        public string Name { get => "hello"; }
        public string Description { get => "Displays hello message."; }
        public void Execute()
        {
            Console.WriteLine("Hello !!!");
        }
    }
    [HarmonyPatch(typeof(PapyrusCompiler.Program))]
    [HarmonyPatch("Main")]
    public class TestPatch
    {
        static void Postfix(string[] args)
        {
            Console.WriteLine("TestPatch");
        }
    }
}