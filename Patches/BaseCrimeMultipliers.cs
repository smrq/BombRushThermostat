using HarmonyLib;
using Reptile;

namespace BombRushThermostat.Patches
{
	internal static class BaseCrimeMultipliers
	{
		[HarmonyPatch(typeof(WantedManager), nameof(WantedManager.Init))]
		[HarmonyPostfix]
		internal static void WantedManager_Init_Postfix(ref float[] ___crimeMultipliersAtStars)
		{
			for (var i = 0; i < ___crimeMultipliersAtStars.Length; i++)
			{
				___crimeMultipliersAtStars[i] = 1f;
			}
		}
	}
}
