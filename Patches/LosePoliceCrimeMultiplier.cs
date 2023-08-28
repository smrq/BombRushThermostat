using HarmonyLib;
using Reptile;

namespace BombRushThermostat.Patches
{
	internal static class LosePoliceCrimeMultiplier
	{
		[HarmonyPatch(typeof(WantedManager), "ProcessCrime")]
		[HarmonyPrefix]
		internal static void WantedManager_ProcessCrime_Prefix(ref float ___crimeMultiplierLosePolice)
		{
			___crimeMultiplierLosePolice = 1.0f;
		}
	}
}
