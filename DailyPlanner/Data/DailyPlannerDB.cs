using SQLite;

namespace DailyPlanner.Data
{
    public class DailyPlannerDB
    {
        private readonly SQLiteAsyncConnection _db;

        public DailyPlannerDB(string dbPath)
        {
            _db = new SQLiteAsyncConnection(dbPath);
            // создаём таблицу асинхронно — не блокируй UI
            _ = _db.CreateTableAsync<Note>();
        }

        public Task<List<Note>> GetNotesAsync()
            => _db.Table<Note>().OrderByDescending(n => n.NoteDate).ToListAsync();

        public Task<Note> GetNoteAsync(int id)
            => _db.FindAsync<Note>(id);

        public Task<int> SaveNoteAsync(Note note)
        {
            if (note.Id != 0)
                return _db.UpdateAsync(note);
            else
            {
                note.CreatedDate = DateTime.Now;
                return _db.InsertAsync(note);
            }
        }

        public Task<int> DeleteNoteAsync(Note note)
        {
            // Лучше удалять по Id явно:
            if (note == null) return Task.FromResult(0);
            return _db.DeleteAsync<Note>(note.Id); // <- удаляем по первичному ключу
        }
    }
}
