using DailyPlanner.Data;

namespace DailyPlanner
{
    public partial class App : Application
    {
        private static DailyPlannerDB database;
        public static DailyPlannerDB Database
        {
            get
            {
                if (database == null)
                {
                    var dbPath = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "dailyplanner.db3");

                    database = new DailyPlannerDB(dbPath); // ← падает тут
                }
                return database;
            }
        }

        public App()
        {
            SQLitePCL.Batteries_V2.Init();
            InitializeComponent();
            MainPage = new AppShell();
        }
    }


}
