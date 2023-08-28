using HarmonyLib;
using Reptile;
using System.Reflection;

namespace BombRushThermostat.Patches
{
	[HarmonyPatch]
	internal static class Retagging
	{
		private static GraffitiSpot lastGraffitiSpot = null;
		private static bool retaggedLastGraffitiSpot = false;

		[HarmonyPatch(typeof(Player), nameof(Player.EndGraffitiMode))]
		[HarmonyPrefix]
		internal static void Player_EndGraffitiMode_Prefix(GraffitiSpot graffitiSpot)
		{
			var state = (GraffitiState)typeof(GraffitiSpot).GetField("state", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(graffitiSpot);
			if (state == GraffitiState.FINISHED)
			{
				retaggedLastGraffitiSpot = lastGraffitiSpot == graffitiSpot;
				lastGraffitiSpot = graffitiSpot;
			}
		}

		[HarmonyPatch(typeof(WantedManager), "ProcessCrime")]
		[HarmonyPrefix]
		internal static bool WantedManager_ProcessCrime_Prefix(WantedManager.Crime crime)
		{
			if (crime == WantedManager.Crime.VANDALISM_SMALL ||
				crime == WantedManager.Crime.VANDALISM_MEDIUM ||
				crime == WantedManager.Crime.VANDALISM_LARGE ||
				crime == WantedManager.Crime.VANDALISM_XLARGE)
			{
				if (retaggedLastGraffitiSpot)
				{
					return false;
				}
			}
			return true;
		}
	}
}
