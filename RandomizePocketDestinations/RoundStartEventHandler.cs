using Smod2;
using Smod2.API;
using Smod2.Events;
using Smod2.Config;

namespace RandomizePocketDestinations.EventHandlers
{
    class RoundStartHandler : IEventRoundStart
    {
		private RandomizePocketDestinationsPlugin plugin;

		public RoundStartHandler(Plugin plugin)
		{
			this.plugin = (RandomizePocketDestinationsPlugin) plugin;
		}

		public void OnRoundStart(Server server)
		{
			foreach (Player player in server.GetPlayers())
			{
				switch (player.Class.ClassType)
				{
					case Classes.SCP_049:
					case Classes.SCP_096:
					case Classes.SCP_106:
					//case Classes.SCP_173:
						this.plugin.SCPSpawnPoints.Add(player.Class.ClassType, player.GetPosition());
						break;
				}
			}
			plugin.Info("SCPSpawnPoints SIZE " + this.plugin.SCPSpawnPoints.Count);
		}
	}

	class PocketDimensionExitHandler : IEventPocketDimensionExit
	{
		private RandomizePocketDestinationsPlugin plugin;

		public PocketDimensionExitHandler(Plugin plugin)
		{
			this.plugin = (RandomizePocketDestinationsPlugin) plugin;
		}

		public void OnPocketDimensionExit(Vector[] possibleExits, out Vector[] possibleExitsOutput)
		{
			//ConfigSetting randomziePocketDestinations = ConfigManager.Manager.ResolveSetting(this.plugin, "randomize_pocket_destinations");
			if (this.plugin.GetConfigBool("randomize_pocket_destinations") && this.plugin.SCPSpawnPoints.Count > 0)
			{
				Vector[] destinations = new Vector[this.plugin.SCPSpawnPoints.Count];
				this.plugin.SCPSpawnPoints.Values.CopyTo(destinations, 0);
				possibleExitsOutput = destinations;
			} else
			{
				possibleExitsOutput = possibleExits;
			}
		}
	}
}
