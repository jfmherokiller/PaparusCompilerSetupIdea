using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using PCompiler;
using PluginLib;

namespace SkyrimPapCompilerExtensionFramework
{
    internal class Program
    {
        public static Harmony PatchHome;
        static Assembly LoadPlugin(string relativePath)
        {
            // Navigate up to the solution root
            string root = typeof(Program).Assembly.Location;

            string pluginLocation = Path.GetFullPath(Path.Combine(root, relativePath.Replace('\\', Path.DirectorySeparatorChar)));
            Console.WriteLine($"Loading commands from: {pluginLocation}");
            Assembly loadContext = Assembly.LoadFrom(pluginLocation);
            return loadContext;
        }

        static IEnumerable<IPlugin> CreateCommands(Assembly assembly)
        {
            int count = 0;

            foreach (var type in assembly.GetTypes())
            {
                if (!typeof(IPlugin).IsAssignableFrom(type)) continue;
                if (!(Activator.CreateInstance(type) is IPlugin result)) continue;
                count++;
                yield return result;
            }

            if (count != 0) yield break;
            var availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
            throw new ApplicationException(
                $"Can't find any type which implements IPlugin in {assembly} from {assembly.Location}.\n" +
                $"Available types: {availableTypes}");
        }
        public static void LoadPluginStuffs(Harmony patchHome)
        {
            var directoryName = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            var BasePaths = Directory.EnumerateFiles(Path.Combine(directoryName,"Plugins"));
            var BasePluginPaths = BasePaths.Where((Pathme)=> Pathme.Contains("dll"));
            var pluginPaths = BasePluginPaths.Where((Pathme)=> Path.GetFileName(Pathme).StartsWith("Plugin_"));
            var commands = pluginPaths.SelectMany(pluginPath =>
            {
                var pluginAssembly = LoadPlugin(pluginPath);
                PatchHome.PatchAll(pluginAssembly);
                return CreateCommands(pluginAssembly);
            }).ToList();
            foreach (var command in commands)
            {
                command.Execute();
            }
        }
        public static void Main(string[] args)
        {
            PatchHome = new Harmony("PapCompiler");
            try
            {
                LoadPluginStuffs(PatchHome);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            PapyrusCompiler.Program.Main(args);
        }
    }
}