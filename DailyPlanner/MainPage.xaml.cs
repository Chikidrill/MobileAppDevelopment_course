using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;

namespace DailyPlanner;

public partial class MainPage : ContentPage
{
    public static List<Note> Notes { get; set; } = new();

    public MainPage()
    {
        InitializeComponent();
        NotesCollection.ItemsSource = Notes;
    }

    private async void OnAddNoteClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NotePage(null));
    }

    private async void OnEditNoteClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is Note note)
        {
            await Navigation.PushAsync(new NotePage(note));
        }
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        var filtered = Notes
            .Where(n => string.IsNullOrWhiteSpace(e.NewTextValue) ||
                        n.Title.Contains(e.NewTextValue, StringComparison.OrdinalIgnoreCase))
            .ToList();

        NotesCollection.ItemsSource = filtered;
    }
    private async Task ReloadNotesAsync()
    {
        NotesCollection.ItemsSource = await App.Database.GetNotesAsync();
    }

    private async void OnDeleteNoteClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is Note note)
        {
            if (await DisplayAlert("Удаление", $"Удалить \"{note.Title}\"?", "Да", "Нет"))
            {
                await App.Database.DeleteNoteAsync(note);
                await ReloadNotesAsync();
            }
        }
    }
    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        var popup = new SettingsPopup();
        await this.ShowPopupAsync(popup);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        NotesCollection.ItemsSource = await App.Database.GetNotesAsync(); 
    }
}
