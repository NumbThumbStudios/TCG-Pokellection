using Plugin.MauiMTAdmob;
using TCGPokellection.Models;
using TCGPokellection.Views;

namespace TCGPokellection.Controls;

public partial class Popup_CardsPage : ContentView
{
    #region FIELDS / PROPERTIES
    CardsPage cp;
    #endregion




    #region METHODS
    // CONSTRUCTOR
    public Popup_CardsPage()
	{
		InitializeComponent();
        cp = Shell.Current.CurrentPage as CardsPage;

        SetSwitchGraphics();
    }

    // SET VARIANT SWITCH GRAPHIC
    private void SetSwitchGraphics()
    {
        if (AppController.ShowVariants)
        {
            SwitchBackground_Off.IsVisible = false;
            SwitchButton_Off.IsVisible = false;
            SwitchBackground_On.IsVisible = true;
            SwitchButton_On.IsVisible = true;
        }
        else
        {
            SwitchBackground_Off.IsVisible = true;
            SwitchButton_Off.IsVisible = true;
            SwitchBackground_On.IsVisible = false;
            SwitchButton_On.IsVisible = false;
        }
    }
    #endregion




    #region EVENT HANDLERS
    // CLOSE BUTTON CLICKED
    private void CloseButton_Clicked(object sender, EventArgs e)
    {
		cp = Shell.Current.CurrentPage as CardsPage;
		cp.ToggleMenu();
    }

    // OUTER BORDER AREA TOUCHED
    private void OuterBorder_Tapped(object sender, TappedEventArgs e)
    {
        cp = Shell.Current.CurrentPage as CardsPage;
        cp.ToggleMenu();
    }

    // VARIANT SWITCH TAPPED
    private void VariantSwitch_Tapped(object sender, TappedEventArgs e)
    {
        cp = Shell.Current.CurrentPage as CardsPage;
        cp.ToggleVariants();

        SetSwitchGraphics();
    }

    // ADD ALL CARDS TO COLLECTION
    private void AddAllCards_Tapped(object sender, TappedEventArgs e)
    {
        cp = Shell.Current.CurrentPage as CardsPage;
        cp.AddAllCardsToCollection();
    }

    // REMOVE ALL CARDS FROM COLLECTION
    private void RemoveAllCards_Tapped(object sender, TappedEventArgs e)
    {
        cp = Shell.Current.CurrentPage as CardsPage;
        cp.RemoveAllCardsFromCollection();
    }

    // BLOCK ALL TAPS
    private void BlockAllTaps_Tapped(object sender, TappedEventArgs e)
    {
        // This function prevents the users from being able to tap through this element...
    }
    #endregion
}