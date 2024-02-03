namespace Winget.Execute
{
	using System.Management.Automation;
	using Winget.Helpers;

	internal class RunWinget
	{
		internal static void RunPowershellScript(string script)
		{
			using (var powershellInstance = PowerShell.Create())
			{
				var cmdAddedpowershellInstance = powershellInstance.AddScript(script);
				var originalResults = cmdAddedpowershellInstance.Invoke();
				if (originalResults is not null)
				{
					var filteredResults = WingetRowsHelper.FilterOutputRows(originalResults);
					if (filteredResults is not null) DebuggerHelper.PrintScript(cmdAddedpowershellInstance, filteredResults);
				}
			}
		}
	}
}
