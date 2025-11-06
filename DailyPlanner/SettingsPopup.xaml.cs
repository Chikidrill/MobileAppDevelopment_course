using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Storage; // для Preferences

namespace DailyPlanner;

public partial class SettingsPopup : Popup
{
    public SettingsPopup()
    {
        InitializeComponent();

        // Загружаем сохранённые значения
        LoadSettings();
    }

    private void LoadSettings()
    {
        // Дата (по умолчанию — сегодня)
        var savedDate = Preferences.Get("SelectedDate", DateTime.Now);
        DatePickerControl.Date = savedDate;

        // Тема
        var theme = Preferences.Get("Theme", "Светлая");
        ThemePicker.SelectedItem = theme;

        // Шрифт
        var font = Preferences.Get("Font", "OpenSansRegular");
        FontPicker.SelectedItem = font;

        // Размер
        var size = Preferences.Get("FontSize", 18.0);
        FontSizeSlider.Value = size;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Сохраняем настройки
        Preferences.Set("SelectedDate", DatePickerControl.Date);
        Preferences.Set("Theme", ThemePicker.SelectedItem?.ToString() ?? "Светлая");
        Preferences.Set("Font", FontPicker.SelectedItem?.ToString() ?? "OpenSansRegular");
        Preferences.Set("FontSize", FontSizeSlider.Value);

        // Применим оформление к приложению
        ApplyStyle();

        await CloseAsync();
    }

    private void ApplyStyle()
    {
        string theme = Preferences.Get("Theme", "Светлая");
        string font = Preferences.Get("Font", "OpenSansRegular");
        double fontSize = Preferences.Get("FontSize", 18.0);

        Application.Current.Resources["AppFontFamily"] = font;
        Application.Current.Resources["AppFontSize"] = fontSize;

        if (theme == "Тёмная")
        {
            Application.Current.Resources["AppBackgroundColor"] = Colors.Black;
            Application.Current.Resources["AppTextColor"] = Colors.White;
        }
        else if (theme == "Фиолетовая")
        {
            Application.Current.Resources["AppBackgroundColor"] = Color.FromArgb("#512BD4");
            Application.Current.Resources["AppTextColor"] = Colors.White;
        }
        else
        {
            Application.Current.Resources["AppBackgroundColor"] = Colors.White;
            Application.Current.Resources["AppTextColor"] = Colors.Black;
        }
    }

    private void OnResetClicked(object sender, EventArgs e)
    {
        Preferences.Clear(); // Удаляем все настройки
        DatePickerControl.Date = DateTime.Now;
        ThemePicker.SelectedItem = "Светлая";
        FontPicker.SelectedItem = "OpenSansRegular";
        FontSizeSlider.Value = 18;

        ApplyStyle(); // возвращаем стандартное оформление
    }
}
