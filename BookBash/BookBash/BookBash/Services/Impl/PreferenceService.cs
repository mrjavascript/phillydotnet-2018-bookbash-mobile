using System.Threading.Tasks;
using BookBash.Models;
using SQLite;
using Xamarin.Forms;

namespace BookBash.Services.Impl
{
    public class PreferenceService : IPreferenceService
    {
        private const string DarkTheme = "DARKTHEME";

        /*
         *  Instance data
         */
        private readonly SQLiteAsyncConnection _db;

        public PreferenceService()
        {
            var fileHelper = DependencyService.Get<IFilePathService>();

            // Init the database
            var dbPath = fileHelper.GetLocalFilePath("BookBashPreferencesSQLite.db3");
            _db = new SQLiteAsyncConnection(dbPath);
            _db.CreateTableAsync<Preference>().Wait();
        }


        public async Task<bool> HasDarkModeSet()
        {
            var darkMode = await QueryDarkModePreference();
            return darkMode?.preference_value > 0;
        }

        private async Task<Preference> QueryDarkModePreference()
        {
            var preferences = await _db.QueryAsync<Preference>("SELECT * FROM [Preference]");
            foreach (var preference in preferences)
            {
                if (preference.preference_text.Equals(DarkTheme))
                {
                    return preference;
                }
            }

            return null;
        }

        public async Task SetDarkMode()
        {
            var darkMode = await QueryDarkModePreference();
            if (darkMode == null)
            {
                darkMode = new Preference {preference_text = DarkTheme, preference_value = 1};
                await InsertDarkModePreference(darkMode);
            }
            else
            {
                darkMode.preference_value = 1;
                await UpdateDarkModePreference(darkMode);
            }
        }

        private Task InsertDarkModePreference(Preference darkMode)
        {
            return _db.InsertAsync(darkMode);
        }

        public async Task UnsetDarkMode()
        {
            var darkMode = await QueryDarkModePreference();
            if (darkMode == null)
            {
                darkMode = new Preference { preference_text = DarkTheme, preference_value = 0 };
                await InsertDarkModePreference(darkMode);
            }
            else
            {
                darkMode.preference_value = 0;
                await UpdateDarkModePreference(darkMode);
            }
        }

        private Task UpdateDarkModePreference(Preference darkMode)
        {
            return _db.UpdateAsync(darkMode);
        }
    }
}