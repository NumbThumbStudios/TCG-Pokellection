using DataLibrary.Models;
using DataLibrary;
using TCGPokellection.Models;
using System.Collections.ObjectModel;

namespace TCGPokellection.Views;

public partial class CardsPage : ContentPage
{
    #region FIELDS / PROPERTIES
    private bool isLoadingData = true;
    DataAccess _data = new DataAccess();
    public string HeaderTitle { get; set; }
    public string HeaderImage { get; set; }
    private ObservableCollection<CardsModel> cards;
    public ObservableCollection<CardsModel> Cards
    {
        get => cards;
        set
        {
            cards = value;
            OnPropertyChanged(nameof(Cards));
        }
    }

    private CardsModel selectedItem;
    public CardsModel SelectedItem
    {
        get => selectedItem;
        set
        {
            selectedItem = value;
            OnPropertyChanged(nameof(SelectedItem));

            if (SelectedItem != null)
            {
                Card_Selected();
                CardsCollectionView.SelectedItem = null;
            }
        }
    }
    private int sort_SelectedItem;
    public int Sort_SelectedItem
    {
        get => sort_SelectedItem;
        set
        {
            sort_SelectedItem = value;
            OnPropertyChanged(nameof(Sort_SelectedItem));

            if (Sort_SelectedItem != -1)
            {
                Sort();
            }
        }
    }
    #endregion




    #region METHODS
    // CONSTRUCTOR
    public CardsPage()
    {
        InitializeComponent();
        BindingContext = this;

        LoadData();
    }

    // LOAD DATA
    public async void LoadData()
    {
        LoadingIndicator.IsVisible = true;
        isLoadingData = true;

        Title = AppController.ExpansionSelected.tcg_sets_name_display;
        HeaderImage = AppController.ExpansionSelected.tcg_sets_image_logo;
        OnPropertyChanged(nameof(HeaderImage));

        await Task.Delay(250);

        try
        {
            string sql = $"SELECT c.*, s.tcg_sets_name_display AS CardExpansion, s.tcg_sets_series AS CardSet " +
                $"FROM tcg_cards c, tcg_sets s " +
                $"WHERE s.tcg_sets_group_id = '{AppController.ExpansionSelected.tcg_sets_group_id}' AND c.tcg_cards_group_id = '{AppController.ExpansionSelected.tcg_sets_group_id}' AND tcg_cards_is_product=0";

            Cards = new ObservableCollection<CardsModel>(await Task.Run(() => _data.LoadData<CardsModel, dynamic>(sql, new { })));

            foreach (var card in Cards)
            {
                // Split the cardNumber string to try and find the int card number...
                try
                {
                    string[] s1 = card.tcg_cards_cardNumber.Split("/");
                    card.CardNumber = int.Parse(s1[0]);
                }
                catch
                {
                    card.CardNumber = -1;
                }
            }

            LoadingIndicator.IsVisible = false;
            isLoadingData = false;

            Sort_SelectedItem = 0;
            Sort();

            AppController.Cards = Cards.ToList();
        }
        catch (Exception ex)
        {
            //LoadingLabel.Text = $"Something went wrong, try again later.\n\n{ex}";
        }
    }

    // SORT CARDS
    private async void Sort()
    {

        if (isLoadingData) { return; }

        LoadingIndicator.IsVisible = true;
        await Task.Delay(100);

        switch (Sort_SelectedItem)
        {
            case 0: // Card Number Low to High
                Cards = new ObservableCollection<CardsModel>(Cards.OrderBy(x => x.CardNumber).ToList());
                break;

            case 1: // Card Number High to Low
                Cards = new ObservableCollection<CardsModel>(Cards.OrderByDescending(x => x.CardNumber).ToList());
                break;

            case 2: // A to Z
                Cards = new ObservableCollection<CardsModel>(Cards.OrderBy(x => x.tcg_cards_clean_name).ToList());
                break;

            case 3: // Z to A
                Cards = new ObservableCollection<CardsModel>(Cards.OrderByDescending(x => x.tcg_cards_clean_name).ToList());
                break;

            case 4: // Price Low to High
                Cards = new ObservableCollection<CardsModel>(Cards.OrderBy(x => x.tcg_cards_price_market).ToList());
                break;

            case 5: // Price High to Low
                Cards = new ObservableCollection<CardsModel>(Cards.OrderByDescending(x => x.tcg_cards_price_market).ToList());
                break;
        }

        LoadingIndicator.IsVisible = false;

    }

    // ITEM SELECTED FROM COLLECTION VIEW
    private void Card_Selected()
    {
        AppController.Cards = Cards.ToList();
        AppController.CardSelected = SelectedItem;
        Navigation.PushAsync(new CardDetailsPage());
    }
    #endregion
}