using DataLibrary;
using DataLibrary.Models;
using TCGPokellection.Models;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using Android.App.AppSearch;

namespace TCGPokellection.Views;

public partial class SearchPage : ContentPage
{
    #region FIELDS / PROPERTIES
    DataAccess _data = new DataAccess();
    private List<CardsModel> cards;
    public ObservableCollection<CardsModel> Cards { get; set; } = new ObservableCollection<CardsModel>();
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
                SearchResults.SelectedItem = null;
            }
        }
    }
    private string numberOfResults = "";
    public string NumberOfResults
    {
        get => numberOfResults;
        set
        {
            numberOfResults = value;
            OnPropertyChanged(nameof(NumberOfResults));
        }
    }

    private bool results_busy = false;
    private int total_results = 0;
    private int results_limit = 50;
    private int results_offset = 0;
    #endregion




    #region METHODS
    // CONSTRUCTOR
    public SearchPage()
	{
		InitializeComponent();
        BindingContext = this;
    }

    // LOAD DATA
    private async Task LoadData(string search_params)
    {
        string sql_count = $"SELECT CAST(COUNT(*) as char) " +
            $"FROM tcg_cards c, tcg_sets s " +
            $"WHERE c.tcg_cards_name LIKE '%{search_params}%' AND c.tcg_cards_group_id = s.tcg_sets_group_id";

        string result_count = await Task.Run(() => _data.LoadData_Simple(sql_count).Result);
        NumberOfResults = $"{result_count} results";
        total_results = int.Parse(result_count);

        await Task.Delay(250);

        string sql = $"SELECT c.*, " +
            $"s.tcg_sets_name_display AS CardExpansion, " +
            $"s.tcg_sets_series AS CardSet " +
            $"FROM tcg_cards c, tcg_sets s " +
            $"WHERE c.tcg_cards_name LIKE '%{search_params}%' AND c.tcg_cards_group_id = s.tcg_sets_group_id " +
            $"ORDER BY s.tcg_sets_published " + 
            $"LIMIT {results_offset}, {results_limit}";

        var new_cards = new ObservableCollection<CardsModel>(await Task.Run(() => _data.LoadData<CardsModel, dynamic>(sql, new { })));

        foreach(var card in new_cards)
        {
            Cards.Add(card);
        }

        SearchResults.SetBinding(ItemsView.ItemsSourceProperty, nameof(Cards));
        OnPropertyChanged(nameof(Cards));

        LoadingIndicator.IsVisible = false;
        results_offset += results_limit;
        results_busy = false;

        NoResultsLayout.IsVisible = total_results > 0 ? false : true;
        if(total_results == 0) { SearchResultsFooter.Text = ""; }
    }

    // SEARCH ENTERED
    private async void SearchParamsEntry_Completed(object sender, EventArgs e)
    {
        results_busy = true;
        NoResultsLayout.IsVisible = false;

        Cards.Clear();
        OnPropertyChanged(nameof(Cards));

        SearchResultsFooter.Text = "";
        NumberOfResults = "";
        SearchParamsEntry.IsEnabled = false;
        results_offset = 0;

        if (!String.IsNullOrEmpty(SearchParamsEntry.Text))
        {
            LoadingIndicator.IsVisible = true;
            await LoadData(SearchParamsEntry.Text.Trim());
        }
        else
        {
            NoResultsLayout.IsVisible = true;
        }

        SearchParamsEntry.IsEnabled = true;
    }

    // CLEAR TEXT FROM SEARCH BAR TAPPED
    private void ClearSearchText_Tapped(object sender, TappedEventArgs e)
    {
        SearchParamsEntry.Text = string.Empty;
        SearchParamsEntry.Focus();
    }

    // SEARCH TEXT CHANGED
    private void SearchParamsEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        ClearSearchText.IsVisible = SearchParamsEntry.Text.Length > 0 ? true : false;
    }

    // SCROLLED SEARCH RESULTS
    private async void SearchResults_Scrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        if(results_busy == false)
        {
            if(total_results > 0)
            {
                if(Cards.Count < total_results)
                {
                    if(e.LastVisibleItemIndex > Cards.Count - 5)
                    {
                        results_busy = true;
                        SearchResultsFooter.Text = "";
                        Search_LoadingIndicator.IsVisible = true;
                        await LoadData(SearchParamsEntry.Text.Trim());
                        SearchResultsFooter.Text = "";
                        Search_LoadingIndicator.IsVisible = false;
                    }
                }
                else
                if(Cards.Count >= total_results)
                {
                    results_busy = true;
                    SearchResultsFooter.Text = "";
                    await Task.Delay(500);
                    SearchResultsFooter.Text = "End of Results";
                }
            }
        }
    }

    // CARD SELECTED FROM COLLECTION VIEW
    private void Card_Selected()
    {
        AppController.CardSelected = SelectedItem;
        AppController.Cards = Cards.ToList();
        Navigation.PushAsync(new CardDetailsPage());
    }
    #endregion
}