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
            DatePickerNote.Date = DateTime.Now.Date;
            TimePickerNote.Time = DateTime.Now.TimeOfDay;
        }
        else
        {
            _note = note;
            EntryTitle.Text = note.Title;
            EditorText.Text = note.Text;
            DatePickerNote.Date = note.Date.Date;
            TimePickerNote.Time = note.Date.TimeOfDay;
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        _note.Title = EntryTitle.Text;
        _note.Text = EditorText.Text;
        _note.Date = DatePickerNote.Date + TimePickerNote.Time;

        if (!MainPage.Notes.Contains(_note))
        {
            MainPage.Notes.Add(_note);
        }

        await Navigation.PopAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
