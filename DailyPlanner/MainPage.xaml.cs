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
    private async void OnDeleteNoteClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is Note note)
        {
            bool confirm = await DisplayAlert(
                "Удаление",
                $"Вы уверены, что хотите удалить заметку \"{note.Title}\"?",
                "Да",
                "Отмена");

            if (confirm)
            {
                MainPage.Notes.Remove(note);
                NotesCollection.ItemsSource = null;
                NotesCollection.ItemsSource = MainPage.Notes;
            }
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        NotesCollection.ItemsSource = null;
        NotesCollection.ItemsSource = Notes;
    }
}
