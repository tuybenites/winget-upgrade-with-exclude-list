using Winget.Execute;

namespace Winget
{
	public class Program
	{
		public static void Main(string[] args)
		{
			RunWinget.RunPowershellScript("winget upgrade");
		}
	}
}