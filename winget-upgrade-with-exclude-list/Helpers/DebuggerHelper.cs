using System.Management.Automation;

namespace Winget.Helpers
{
	internal class DebuggerHelper
	{
		internal static void PrintScript(PowerShell powershellInstance, IEnumerable<PSObject> output)
		{
			foreach (var outputItem in output)
			{
				if (outputItem is not null)
				{
					Console.WriteLine(outputItem.BaseObject.ToString());
				}
			}

			if (powershellInstance.HadErrors)
			{
				foreach (var error in powershellInstance.Streams.Error)
				{
					Console.WriteLine(error.ToString());
				}
			}
		}
	}
}
