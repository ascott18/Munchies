using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using AndrewScott.SettingsSerializer;
using AndrewScott.SimpleCommandManager;
using System.Runtime.CompilerServices;
using System.IO;


namespace Munchies
{
	internal static class Program
	{
		internal static Settings Settings;
		internal static CommandManager CommandManager;

		public static event EventHandler SizeSettingChanged;

		public static Size ContentSizeSetting
		{
			set
			{
				Settings.SetSetting("ContentSize", value);

				if (SizeSettingChanged != null)
					SizeSettingChanged(null, new EventArgs());
			}
			get
			{
				Settings.DeclareDefault("ContentSize", new Size(640, 480));

				return (Size)Settings.GetSetting("ContentSize");
			}
		}

		/// <summary>
		///     The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			//AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

			// Crash reporting
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			Application.ThreadException += Application_ThreadException;

			// Initialize settings
			Settings =
				new Settings(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Munchies\settings",
				             true);
			Application.ApplicationExit += Application_ApplicationExit;

			// Initialize command manager
			CommandManager = new CommandManager();

			// Run the program
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainWindow());
		}

		private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			UnhandledException(e.Exception);
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			UnhandledException((Exception)e.ExceptionObject);
		}

		private static void UnhandledException(Exception ex)
		{
			DateTime dtLogFileCreated = DateTime.Now;

			try
			{
				StreamWriter sw = new StreamWriter("crash-" + dtLogFileCreated.Year + dtLogFileCreated.Month
				                                   + dtLogFileCreated.Day + "-" + dtLogFileCreated.Hour
				                                   + dtLogFileCreated.Minute + dtLogFileCreated.Second + ".txt");

				sw.WriteLine("### Crash ###");
				sw.WriteLine(ex.ToString());
				sw.Close();
			}
			finally
			{
				Application.Exit();
			}
		}

		private static void Application_ApplicationExit(object sender, EventArgs e)
		{
			Settings.WriteToFile();
		}

		//static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		//{
		//    string dllName = args.Name.Contains(',') ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name.Replace(".dll", "");

		//    dllName = dllName.Replace(".", "_");

		//    if (dllName.EndsWith("_resources")) return null;

		//    byte[] bytes = (byte[])Properties.Resources.ResourceManager.GetObject(dllName);

		//    if (bytes != null)
		//        return Assembly.Load(bytes);
		//    else
		//        return Assembly.GetCallingAssembly();
		//}
	}
}
