using RandomizePocketDestinations.EventHandlers;
using Smod2;
using Smod2.API;
using Smod2.Attributes;
using Smod2.Events;
using System.Collections.Generic;

namespace RandomizePocketDestinations
{
	[PluginDetails(
		author = "PatPeter",
		name = "RandomizePocketDestinations",
		description = "Randomizes pocket dimension locations.",
		id = "patpeter.randomizepocketdestinations",
		version = "1.0-build3",
		SmodMajor = 2,
		SmodMinor = 0,
		SmodRevision = 0
		)]
	class RandomizePocketDestinationsPlugin : Plugin
	{
		public Dictionary<Classes, Vector> SCPSpawnPoints = new Dictionary<Classes, Vector>();

		public override void OnEnable()
		{
			this.Info("Randomize Pocket Destinations has loaded :)");
			this.Info("randomize_pocket_destinations value: " + this.GetConfigBool("randomize_pocket_destinations"));
		}

		public override void OnDisable()
		{
		}

		public override void Register()
		{
			// Register Events
			this.AddEventHandler(typeof(IEventRoundStart), new RoundStartHandler(this), Priority.Highest);
			this.AddEventHandler(typeof(IEventRoundEnd), new RoundEndHandler(this), Priority.Highest);
			this.AddEventHandler(typeof(IEventPocketDimensionExit), new PocketDimensionExitHandler(this), Priority.Highest);
			// Register config settings
			this.AddConfig(new Smod2.Config.ConfigSetting("randomize_pocket_destinations", true, Smod2.Config.SettingType.BOOL, true, "Randomzies pocket destinations."));
		}
	}
}
