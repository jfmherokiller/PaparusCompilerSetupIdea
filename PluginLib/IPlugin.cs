using HarmonyLib;

namespace PluginLib
{
    public interface IPlugin
    {
        string Name { get; }
        string Description { get; }

        public void Execute();
    }
}