using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using static System.String;

namespace avalonia_TravelPlanner;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private void ImageComboBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (CountrySelectionComboBox.SelectedItem is ComboBoxItem selectedItem)
        {
            var selectedText = selectedItem.Content?.ToString();

            var imagePaths = new Dictionary<string, string>
            {
                { "Niemcy", "avares://avalonia_TravelPlanner/Assets/germany.png" },
                { "Japonia", "avares://avalonia_TravelPlanner/Assets/japan.png" },
                { "Wielka Brytania", "avares://avalonia_TravelPlanner/Assets/london.png" },
            };
            
            var imagePath = imagePaths[selectedText];

            try
            {
                using var stream = AssetLoader.Open(new Uri(imagePath));
                RespectiveImage.Source = new Bitmap(stream);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Nie znaleziono zdjęcia.\n{exception.Message}");
            }
        }
    }


    private void AddItemToListBox(object? sender, RoutedEventArgs e)
    {
        if (CityToAddToList.Text is string cityToAdd)
        {
            if (cityToAdd.Length != 0)
            {
                CitiesListBox.Items.Add(cityToAdd);
            }
            else
            {
                var popupWindow = new PopupWindow();
                popupWindow.Show();
            }
        }
    }

    private void SubmitData(object? sender, RoutedEventArgs e)
    {
        var result = new FormResult();
        
        var submittedAttractions = new List<string>();
        if (NationalParks.IsChecked == true) submittedAttractions.Add("Parki narodowe");
        if (Monuments.IsChecked == true) submittedAttractions.Add("Zabytki");
        if (HistoricalPlaces.IsChecked == true) submittedAttractions.Add("Miejsca historyczne");

        var submittedTransport = (Airplane.IsChecked == true) ? "Samolot" :
                       (Train.IsChecked == true) ? "Pociąg" :
                       (Car.IsChecked == true) ? "Samochód" : "";

        var submittedData =
            $"""
            Imię: {FirstName.Text}
            Nazwisko: {LastName.Text}
            Kraj podróży: {CountrySelectionComboBox.SelectionBoxItem}
            Atrakcje: {Join(", ", submittedAttractions)}
            Środek transportu: {submittedTransport}
            Miasta: {Join(", ", CitiesListBox.Items)}
            """;

        result.SetData(submittedData);
        result.Show();
    }
}