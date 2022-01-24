using BepInEx;
using UnityEngine;
using HarmonyLib;

namespace BehaviourInjector
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class FreeMovePlugin

#if MONO
        : BaseUnityPlugin
#else
        : BepInEx.IL2CPP.BasePlugin
#endif
    {
        public const string GUID = "com.vtvrvxiv.EnableIsDebugBuild";
        public const string NAME = "EnableIsDebugBuild";
        public const string AUTHOR = "vtvrvxiv";
        public const string VERSION = "1.0.0";

#if MONO
        internal void Awake()
        {
            Setup();
        }
#else
        public override void Load()
        {
            Setup();
        }
#endif

        private void Setup()
        {
            Harmony.CreateAndPatchAll(typeof(Hooks));
            Debug.Log("Debug.isDebugBuild == " + Debug.isDebugBuild.ToString());
            //Debug.Log("Debug.developerConsoleVisible == " + Debug.developerConsoleVisible.ToString());
        }
    }


    internal static class Hooks
    {
        [HarmonyPostfix, HarmonyPatch(typeof(UnityEngine.Debug), nameof(UnityEngine.Debug.isDebugBuild), MethodType.Getter)]
        private static void Patch_isDebugBuild(ref bool __result) => __result = true;


        //[HarmonyPostfix, HarmonyPatch(typeof(UnityEngine.Debug), nameof(UnityEngine.Debug.developerConsoleVisible), MethodType.Getter)]
        //private static void Patch_developerConsoleVisible(ref bool __result) => __result = true;
    }
}