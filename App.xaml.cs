using Plugin.InAppBilling;
using System.Diagnostics;
using TCGPokellection.Models;

namespace TCGPokellection;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		MainPage = new AppShell();

		AppController.Init();
	}
}
