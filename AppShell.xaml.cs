using TCGPokellection.Models;
using TCGPokellection.Views;

namespace TCGPokellection;

public partial class AppShell : Shell
{
    #region METHODS
	// CONSTRUCTOR
    public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute("SealedProductList", typeof(SealedProductListPage));
		Routing.RegisterRoute("SealedProductMain", typeof(SealedProductMainPage));
		Routing.RegisterRoute("Sets", typeof(SetsPage));
		Routing.RegisterRoute("Expansions", typeof(ExpansionsPage));
		Routing.RegisterRoute("Cards", typeof(CardsPage));
		Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
    }
    #endregion
}
