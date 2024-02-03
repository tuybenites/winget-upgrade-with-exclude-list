using System.Collections.ObjectModel;
using System.Management.Automation;

namespace Winget.Helpers
{
	internal class WingetRowsHelper
	{
		private const string TitleWithoutSpaces = "NameIdVersionAvailableSource";
		private const string UpgradesAvailabeRow = "upgrades available.";
		private const string RequireExplicitTargetingForUpgrade = " require explicit targeting for upgrade";
		private const string HyphenLine = "-------------";

		private static void CleanPrefix(Collection<PSObject> output)
		{
			var index = 0;

			for (var i = 0; i < output.Count; i++)
			{
				var baseObjStringRow = output[i].BaseObject.ToString() ?? "";

				if (baseObjStringRow.Replace(" ", "") == TitleWithoutSpaces)
				{
					index = i;
					break;
				}
			}

			for (int i = 0; i < index; i++)
			{
				output.RemoveAt(0);
			}
		}

		internal static IEnumerable<PSObject> FilterOutputRows(Collection<PSObject> output)
		{
			CleanPrefix(output);

			var cleanOutput = from row in output
							  where row is not null
							  let baseObjStringRow = row.BaseObject.ToString() ?? ""
							  where !string.IsNullOrEmpty(baseObjStringRow)
							  where !baseObjStringRow.Contains(HyphenLine)
							  where !baseObjStringRow.Contains(UpgradesAvailabeRow)
							  where !baseObjStringRow.Contains(RequireExplicitTargetingForUpgrade)
							  select row;

			return cleanOutput;
		}
	}
}
