using System;
using System.Diagnostics;
using System.Linq;

namespace FlyMark
{
	public static partial class CredentialManager
	{
		/// <summary>
		/// A simple class to make it harder to accidently reveal secrets while debugging.
		/// </summary>
		private class SafeDebugString
		{
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			string secret;

			internal SafeDebugString(string initialValue)
			{
				secret = initialValue;
			}

			public override string ToString()
			{
				return "Use GetStr() to get the value.";
			}

			internal string GetStr()
			{
				return secret;
			}
		}
	}
}
