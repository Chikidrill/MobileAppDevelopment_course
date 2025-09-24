using SQLite;

namespace DailyPlanner.Data
{
    public class DailyPlannerDB
    {
        private readonly SQLiteAsyncConnection _database;

        public DailyPlannerDB(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _ = _database.CreateTableAsync<Note>(); // асинхронно
        }

        public Task<List<Note>> GetNotesAsync() => _database.Table<Note>().ToListAsync();
        public Task<int> SaveNoteAsync(Note note) => note.Id != 0 ? _database.UpdateAsync(note) : _database.InsertAsync(note);
        public Task<int> DeleteNoteAsync(Note note) => _database.DeleteAsync(note);
    }
}
