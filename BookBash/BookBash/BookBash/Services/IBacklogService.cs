using System.Collections.Generic;
using System.Threading.Tasks;
using BookBash.Models;

namespace BookBash.Services
{
    public interface IBacklogService
    {
        Task<List<BacklogItem>> GetUserBacklog();
        Task DeleteBacklogItem(long itemRecordId);
        Task<List<BacklogStatus>> GetBacklogStatuses();
        Task AddBacklogItem(long bookId, long statusId, double rating);
        Task EditBacklogItem(long recordId, long statusId, double rating, long bookId);
        Task<Book> FindBookByIsbn(string isbn);
    }
}