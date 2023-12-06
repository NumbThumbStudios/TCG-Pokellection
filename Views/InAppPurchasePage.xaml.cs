using Plugin.InAppBilling;
using System;
using System.Diagnostics;
using TCGPokellection.Models;

namespace TCGPokellection.Views;

public partial class InAppPurchasePage : ContentPage
{
    #region FIELDS / PROPERTIES
    public bool AdsRemoved { get; set; }
    #endregion




    #region METHODS
    // CONSTRUCTOR
    public InAppPurchasePage()
	{
		InitializeComponent();
		BindingContext = this;

        Init();
	}

    // INIT
    public void Init()
    {
        SetupButtons();

        LoadingIndicator.IsVisible = false;
    }

    // SETUP UP BUTTONS
    private void SetupButtons()
    {
        if (AppController.IAP_AdsRemoved)
        {
            AdsPurchased_Label.IsVisible = true;
            RemoveAds_Button.IsVisible = false;
        }
        else
        {
            AdsPurchased_Label.IsVisible = false;
            RemoveAds_Button.IsVisible = true;
        }
    }

    // PURCHASE - REMOVE ADS
    public async void Purchase_RemoveAds()
    {
        LoadingIndicator.IsVisible = true;

        await PurchaseItem(AppController.RemoveAdsProductId);

        LoadingIndicator.IsVisible = false;
    }

    // PURCHASE PRODUCTS
    public async Task<bool> PurchaseItem(string productId)
    {
        var billing = CrossInAppBilling.Current;
        try
        {
            var connected = await billing.ConnectAsync();
            if (!connected)
            {
                //we are offline or can't connect, don't try to purchase
                return false;
            }

            //check purchases
            //var purchase = await billing.PurchaseAsync(productId, ItemType.InAppPurchase);
            var purchase = await billing.PurchaseAsync(productId, ItemType.InAppPurchase);

            //possibility that a null came through.
            if (purchase == null)
            {
                //did not purchase
                return false;
            }
            else if (purchase.State == PurchaseState.Purchased)
            {
                // only need to finalize if on Android unless you turn off auto finalize on iOS
                var ack = await CrossInAppBilling.Current.FinalizePurchaseAsync(purchase.TransactionIdentifier);

                AppController.IAP_AdsRemoved = true;
                await DisplayAlert($"Success", $"You have successfully removed ads. Thank you so much for your support!", $"Close");
                SetupButtons();

                return true;
            }
        }
        catch (InAppBillingPurchaseException purchaseEx)
        {
            //Billing Exception handle this based on the type
            Debug.WriteLine("Error: " + purchaseEx);
        }
        catch (Exception ex)
        {
            //Something else has gone wrong, log it
            Debug.WriteLine("Issue connecting: " + ex);
        }
        finally
        {
            await billing.DisconnectAsync();
        }

        return false;
    }
    #endregion




    #region EVENT HANDLERS
    // ON APPEARING
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await AppController.RestoreIAP();

        SetupButtons();
    }

    // REMOVE ADS BUTTON CLICKED
    private void RemoveAds_Button_Clicked(object sender, EventArgs e)
    {
        Purchase_RemoveAds();
    }
    #endregion
}