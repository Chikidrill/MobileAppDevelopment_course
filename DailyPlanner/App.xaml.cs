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
            InitializeComponent();
            ApplySavedSettings();

            // Без Shell — напрямую на главную страницу
            MainPage = new MainPage();
        }

        private void ApplySavedSettings()
        {
            string font = Preferences.Get("Font", "OpenSansRegular");
            double fontSize = Preferences.Get("FontSize", 18.0);
            Application.Current.Resources["AppFontFamily"] = font;
            Application.Current.Resources["AppFontSize"] = fontSize;

            string theme = Preferences.Get("Theme", "Светлая");

            if (theme == "Тёмная")
            {
                Application.Current.Resources["AppBackgroundColor"] = Colors.Black;
                Application.Current.Resources["AppTextColor"] = Colors.White;
                Application.Current.Resources["AppHeaderColor"] = Colors.DarkGray;
            }
            else if (theme == "Фиолетовая")
            {
                Application.Current.Resources["AppBackgroundColor"] = Color.FromArgb("#512BD4");
                Application.Current.Resources["AppTextColor"] = Colors.White;
                Application.Current.Resources["AppHeaderColor"] = Color.FromArgb("#3B1E8A");
            }
            else
            {
                Application.Current.Resources["AppBackgroundColor"] = Colors.White;
                Application.Current.Resources["AppTextColor"] = Colors.Black;
                Application.Current.Resources["AppHeaderColor"] = Color.FromArgb("#EEE");
            }
        }
    }
}
