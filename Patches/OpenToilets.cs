using HarmonyLib;
using Reptile;
using System.Reflection;

namespace BombRushThermostat.Patches
{
	internal static class OpenToilets
	{
		private static bool skipNextToiletCollider = false;
		private static bool preventNextToiletClose = false;

		[HarmonyPatch(typeof(PublicToilet), nameof(PublicToilet.DoSequence))]
		[HarmonyPrefix]
		internal static bool PublicToilet_DoSequence_Prefix()
		{
			if (skipNextToiletCollider)
			{
				skipNextToiletCollider = false;
				return false;
			}

			var wanted = (bool)typeof(WantedManager).GetField("wanted", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(WantedManager.instance);
			if (!wanted)
			{
				preventNextToiletClose = true;
			}

			return true;
		}

		[HarmonyPatch(typeof(PublicToilet), "CloseDoor")]
		[HarmonyPrefix]
		internal static bool PublicToilet_CloseDoor_Prefix(PublicToilet __instance)
		{
			if (preventNextToiletClose)
			{
				preventNextToiletClose = false;
				skipNextToiletCollider = true;
				var OpenDoor = typeof(PublicToilet).GetMethod("OpenDoor", BindingFlags.Instance | BindingFlags.NonPublic);
				OpenDoor.Invoke(__instance, new object[] { false });
				return false;
			}

			return true;
		}
	}
}
