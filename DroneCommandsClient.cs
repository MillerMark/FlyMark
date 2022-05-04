using System;
using FlyMark;
using System.Diagnostics;
using TwitchLib.Client;

namespace FlyMark
{
	static internal class DroneCommands
	{
		private const string STR_DroneCommandsChannelName = "DroneCommands";
		public static TwitchClient DroneCommandsClient { get; private set; } = new TwitchClient();

		public static void Connect()
		{
			CredentialManager.Authenticate(DroneCommandsClient, STR_DroneCommandsChannelName);
			try
			{
				DroneCommandsClient.Connect();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				Debug.WriteLine(ex.StackTrace);
				Debug.WriteLine("");
			}
		}

		public static void Disconnect()
		{
			try
			{
				DroneCommandsClient.Disconnect();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
		}

		public static void Chat(string msg)
		{
			try
			{
				if (!DroneCommandsClient.IsInitialized || !DroneCommandsClient.IsConnected)
				{
					CredentialManager.Authenticate(DroneCommandsClient, STR_DroneCommandsChannelName);
					DroneCommandsClient.Connect();
				}
				DroneCommandsClient.SendMessage(STR_DroneCommandsChannelName, msg);
			}
			catch (Exception ex)
			{
				if (!DroneCommandsClient.IsConnected)
				{
					DroneCommandsClient.Disconnect();
					DroneCommandsClient.Connect();
				}

				Debug.WriteLine(ex.Message);
			}
		}
	}
}
