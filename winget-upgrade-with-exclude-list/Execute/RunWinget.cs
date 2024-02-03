namespace Winget.Execute
{
	using System.Collections.ObjectModel;
	using System.Management.Automation;

	internal class RunWinget
	{
		private const string TitleWithoutSpaces = "NameIdVersionAvailableSource";

		internal static void RunPowershellScript(string script)
		{
			using (var powershellInstance = PowerShell.Create())
			{
				var cmdAddedpowershellInstance = powershellInstance.AddScript(script);
				var results = cmdAddedpowershellInstance.Invoke();
				var filteredResults = FilterOutputRows(results);
				if (filteredResults is not null) PrintScript(cmdAddedpowershellInstance, filteredResults);
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

		private static Collection<PSObject>? FilterOutputRows(Collection<PSObject> output)
		{
			Collection<PSObject>? outputWithoutPrefix = null;

			for (int i = 0; i < output.Count; i++)
			{
				var row = output[i];
				var stringRow = row.BaseObject.ToString();

				if (stringRow?.Replace(" ", "") == TitleWithoutSpaces)
				{
					outputWithoutPrefix = CleanPrefix(output, i);
					break;
				}
			}

			return outputWithoutPrefix;
		}

		private static Collection<PSObject> CleanPrefix(Collection<PSObject> output, int index)
		{
			for (int i = 0; i <= index; i++)
			{
				output.RemoveAt(0);
			}

			return output;
		}

	}
}
