using SQLite;
using StudentPlanerApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace StudentPlanerApp.Services
{
    public static class DatabaseService
    {
        static SQLiteAsyncConnection _database;

        static async Task Init()
        {
            if (_database != null)
                return;

            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "zadatak.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            await _database.CreateTableAsync<Zadatak>();
        }

        public static async Task<List<Zadatak>> GetZadaciAsync()
        {
            await Init();
            return await _database.Table<Zadatak>().ToListAsync();
        }

        public static async Task AddZadatakAsync(Zadatak zadatak)
        {
            await Init();
            await _database.InsertAsync(zadatak);
        }

        public static async Task DeleteZadatakAsync(Zadatak zadatak)
        {
            await Init();
            await _database.DeleteAsync(zadatak);
        }

        public static async Task UpdateZadatakAsync(Zadatak zadatak)
        {
            await Init();
            await _database.UpdateAsync(zadatak);
        }
    }
}
