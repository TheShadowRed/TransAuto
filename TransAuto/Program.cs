/*
 * Created by SharpDevelop.
 * User: TheRedLord
 * Date: 8/31/2017
 * Time: 17:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace TransAuto
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//Application.Run(new DatePersonaleAngajati());
			//Application.Run(new CurseForm());
			Application.Run(new MainForm());
		}
		
	}
}
