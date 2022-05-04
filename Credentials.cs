using Meziantou.Framework.Win32;
using TwitchLib.Client;
using TwitchLib.Client.Models;

namespace FlyMark;

public static partial class CredentialManager
{
	const string STR_AppName = "FlyMark";

	/// <summary>
	/// Saves the user name and password on the local machine.
	/// </summary>
	/// <param name="twitchUserName">The Bittrex api key.</param>
	/// <param name="twitchPassword">The Bittrex api secret.</param>
	//! Careful when debugging not to unintentionally reveal secrets!
	public static void SaveKeys(string twitchUserName, string twitchPassword)
	{
		string key = $"{twitchUserName}";
		string secret = $"{twitchPassword}";
		Meziantou.Framework.Win32.CredentialManager.WriteCredential(STR_AppName, key, secret, CredentialPersistence.LocalMachine);
	}

	/// <summary>
	/// Gets the api keys and secrets stored on this machine.
	/// </summary>
	/// <param name="userName">A list of keys separated by the STR_KeySplitter constant</param>
	/// <param name="password">A list of secrets separated by the STR_KeySplitter constant</param>
	private static void GetUserNameAndPassword(out SafeDebugString? userName, out SafeDebugString? password)
	{
		userName = null;
		password = null;
		Credential? credential = Meziantou.Framework.Win32.CredentialManager.ReadCredential(STR_AppName);
		if (credential == null || credential.UserName == null || credential.Password == null)
			return;

		userName = new SafeDebugString(credential.UserName);
		password = new SafeDebugString(credential.Password);
	}

	/// <summary>
	/// Returns whether api credentials are stored on the local machine or not.
	/// </summary>
	public static bool StoredOnLocalMachine()
	{
		GetUserNameAndPassword(out SafeDebugString? userName, out SafeDebugString? password);
		return userName != null && password != null;
	}

	/// <summary>
	/// Deletes the API key and secret stored on the local machine. May throw a Win32Exception 
	/// if the credentials are not found on this machine.
	/// </summary>
	public static void DeleteSecrets()
	{
		Meziantou.Framework.Win32.CredentialManager.DeleteCredential(STR_AppName);
	}

	/// <summary>
	/// Authenticates the TwitchClient using credentials stored on the local machine.
	/// </summary>
	public static void Authenticate(TwitchClient client, string channelName)
	{
		GetUserNameAndPassword(out SafeDebugString? twitchUserName, out SafeDebugString? accessToken);
		if (twitchUserName == null || accessToken == null)
			return;
		var credentials = new ConnectionCredentials(twitchUserName.GetStr(), accessToken.GetStr());
		client.Initialize(credentials, channelName);
	}

}
