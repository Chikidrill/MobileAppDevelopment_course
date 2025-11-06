using DailyPlanner.Data;

namespace DailyPlanner
{
    public partial class App : Application
    {
        private static DailyPlannerDB? database;
        public static DailyPlannerDB Database
        {
            get
            {
                if (database == null)
                {
                    var dbPath = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "dailyplanner.db3");

                    database = new DailyPlannerDB(dbPath);
                }
                return database;
            }
        }

        public App()
        {
            SQLitePCL.Batteries_V2.Init();
            ApplySavedSettings();
            InitializeComponent();
        }
        private void ApplySavedSettings()
        {
            // Шрифт и размер
            string font = Preferences.Get("Font", "OpenSansRegular");
            double fontSize = Preferences.Get("FontSize", 18.0);

            Application.Current.Resources["AppFontFamily"] = font;
            Application.Current.Resources["AppFontSize"] = fontSize;

            // Цвета
            string theme = Preferences.Get("Theme", "Светлая");

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
        protected override Window CreateWindow(IActivationState activationState)
        {
            return new Window(new AppShell());
        }
    }
}