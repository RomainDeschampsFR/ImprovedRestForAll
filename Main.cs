using MelonLoader;
using UnityEngine;
using Il2CppInterop;
using Il2CppInterop.Runtime.Injection; 
using System.Collections;

namespace ImprovedRestForAll
{
	public class Main : MelonMod
	{
		public override void OnInitializeMelon()
		{
            MelonLogger.Msg($"[{Info.Name}] Version {Info.Version} loaded!");
        }

		public override void OnSceneWasLoaded(int buildIndex, string sceneName)
		{
            /*if (sceneName.ToLowerInvariant().Contains("boot") || sceneName.ToLowerInvariant().Contains("empty")) return;
            if (sceneName.ToLowerInvariant().Contains("menu"))
            {

            }*/
        }

    }
}