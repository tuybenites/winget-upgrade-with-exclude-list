namespace Winget.Execute
{
	using System.Collections.ObjectModel;
	using System.Management.Automation;

	public class RunWinget
	{
		public static void RunPowershellScript(string script)
		{
			using (var powershellInstance = PowerShell.Create())
			{
				var cmdAddedpowershellInstance = powershellInstance.AddScript(script);
				var results = cmdAddedpowershellInstance.Invoke();
				PrintScript(cmdAddedpowershellInstance, results);
			}
		}

		private static void PrintScript(PowerShell powershellInstance, Collection<PSObject> output)
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
