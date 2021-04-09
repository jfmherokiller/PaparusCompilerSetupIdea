using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using PapyrusCompiler;

namespace SkyrimPapCompilerExtensionFramework
{
    [HarmonyPatch(typeof(PapyrusCompiler.Program))]
    [HarmonyPatch("Main")]
    public class AddonInfoPatch
    {
        [HarmonyPrefix]
        static void AddMoreParts()
        {
            Console.WriteLine("Loaded Plugins");
            foreach (var pluginClass in Program.PluginClasses)
            {
                Console.WriteLine($"Loaded {pluginClass.Name} - {pluginClass.Description}");
            }
        }
    }
}