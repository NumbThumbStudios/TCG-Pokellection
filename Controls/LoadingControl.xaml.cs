namespace TCGPokellection.Controls;

public partial class LoadingControl : ContentView
{
    #region METHODS
    // CONSTRUCTOR
    public LoadingControl()
    {
        InitializeComponent();
    }
    #endregion




    #region EVENT HANDLERS
    // BLOCK ALL TAPS
    private void BlockAllTaps_Tapped(object sender, TappedEventArgs e)
    {
        // This function prevents the users from being able to tap through this element...
    }
    #endregion
}