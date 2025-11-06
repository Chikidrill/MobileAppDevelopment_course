namespace DailyPlanner;

public partial class NotePage : ContentPage
{
    private Note _note;

    public NotePage(Note note)
    {
        InitializeComponent();

        if (note == null)
        {
            _note = new Note();

            var defaultDate = Preferences.Get("SelectedDate", DateTime.Now);
            DatePickerNote.Date = defaultDate.Date;
            TimePickerNote.Time = defaultDate.TimeOfDay;

        }
        else
        {
            _note = note;
            EntryTitle.Text = note.Title;
            EditorText.Text = note.Text;
            DatePickerNote.Date = note.NoteDate.Date;
            TimePickerNote.Time = note.NoteDate.TimeOfDay;
        }

        BindingContext = _note;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        _note.Title = EntryTitle.Text;
        _note.Text = EditorText.Text;
        _note.NoteDate = DatePickerNote.Date + TimePickerNote.Time;

        if (!MainPage.Notes.Contains(_note))
        {
            MainPage.Notes.Add(_note);
        }

        await App.Database.SaveNoteAsync(_note);

        await DisplayAlert("Успех", "Заметка сохранена!", "OK");
        await Navigation.PopAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
