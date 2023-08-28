using BepInEx;
using HarmonyLib;
using BombRushThermostat.Patches;

namespace BombRushThermostat
{
	[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
	public class Plugin : BaseUnityPlugin
	{
		private Harmony harmony;

		private void Awake()
		{
			Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

			harmony = new Harmony("net.smrq.BombRushThermostat");
			harmony.PatchAll(typeof(BaseCrimeMultipliers));
			harmony.PatchAll(typeof(LosePoliceCrimeMultiplier));
			harmony.PatchAll(typeof(OpenToilets));
			harmony.PatchAll(typeof(Retagging));
		}

		private void OnDestroy()
		{
			harmony.UnpatchSelf();
		}
	}
}
