using System.Threading.Tasks;

namespace BookBash.Services
{
    public interface IPreferenceService
    {
        Task<bool> HasDarkModeSet();
        Task SetDarkMode();
        Task UnsetDarkMode();
    }
}