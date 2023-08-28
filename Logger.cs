namespace BombRushThermostat
{
	internal static class Logger
	{
		private static readonly BepInEx.Logging.ManualLogSource _instance = BepInEx.Logging.Logger.CreateLogSource(nameof(BombRushThermostat));
		internal static BepInEx.Logging.ManualLogSource Instance
		{
			get { return _instance; }
		}
	}
}
