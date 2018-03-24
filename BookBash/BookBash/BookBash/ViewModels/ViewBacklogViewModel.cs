using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BookBash.Models;
using BookBash.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace BookBash.ViewModels
{
    public class ViewBacklogViewModel : BindableBase, INavigationAware
    {
        //
        //  Instance data
        private bool _showNoRecords;
        private bool _showListView;
        private bool _isLoading;
        private List<BacklogItem> _backlog;
        private string _filter;
        private Color _theme;
        private readonly IBacklogService _backlogService;
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _pageDialogService;
        private readonly IPreferenceService _preferenceService;

        //
        //  Properties
        public Color Theme
        {
            get => _theme;
            set => SetProperty(ref _theme, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public bool ShowNoRecords {
            get => _showNoRecords;
            set => SetProperty(ref _showNoRecords, value);
        }

        public bool ShowListView
        {
            get => _showListView;
            set => SetProperty(ref _showListView, value);
        }

        public string Filter
        {
            get => _filter;
            set => SetProperty(ref _filter, value);
        }

        public List<BacklogItem> Backlog
        {
            get => _backlog;
            set => SetProperty(ref _backlog, value);
        }
        
        //
        //  Commands
        public ICommand AddNewItemCommand { get; set; }
        public ICommand SortBacklogCommand { get; set; }
        public ICommand FilterBacklogCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        //
        //  Constructor
        public ViewBacklogViewModel(INavigationService navigationService, IBacklogService backlogService, IPageDialogService pageDialogService, IPreferenceService preferenceService)
        {
            IsLoading = false;
            _backlogService = backlogService;
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            _preferenceService = preferenceService;

            //
            //  commanding
            AddNewItemCommand = new DelegateCommand(async () => await OnAddItem());
            SortBacklogCommand = new DelegateCommand(OnSortBacklog);
            FilterBacklogCommand = new DelegateCommand(OnFilterBacklog);
            RefreshCommand = new DelegateCommand(async () => await LoadBacklog());
        }

        private async Task LoadBacklog()
        {
            IsLoading = true;
            Backlog = await _backlogService.GetUserBacklog();
            ShowListView = (Backlog != null && Backlog.Count > 0);
            ShowNoRecords = (Backlog == null || Backlog.Count == 0);
            IsLoading = false;
        }

        //
        //  Other
        public async Task OnAddItem()
        {
            var p = new NavigationParameters { { "Title", "Add" }, { "Item", null } };
            await _navigationService.NavigateAsync("AddEditBacklogPage", p);
        }
        public async Task OnEditItem(BacklogItem item)
        {
            var p = new NavigationParameters {{"Title", "Edit"}, {"Item", item}};
            await _navigationService.NavigateAsync("AddEditBacklogPage", p);
        }
        public async Task OnDeleteItem(BacklogItem item)
        {
            /*
             *  Try
             */
            try
            {
                await _backlogService.DeleteBacklogItem(item.RecordId);

                //
                //  Remove from list
                Backlog = Backlog.Where(x => x.RecordId != item.RecordId).ToList();
            }
            catch (Exception ex)
            {
                await _pageDialogService.DisplayAlertAsync("Errors", $"Fail to delete: {ex.Message}", "OK");
            }
        }
        public void OnSortBacklog()
        {
            Backlog = Backlog.OrderByDescending(o => o.Book.BookTitle).ToList();
            ShowListView = (Backlog != null && Backlog.Count > 0);
            ShowNoRecords = (Backlog == null || Backlog.Count == 0);
        }
        public void OnFilterBacklog()
        {
            Backlog = Backlog.Where(o => o.Book.BookTitle.ToLower().StartsWith(Filter.ToLower())).ToList();
            ShowListView = (Backlog != null && Backlog.Count > 0);
            ShowNoRecords = (Backlog == null || Backlog.Count == 0);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            await LoadBacklog();

            //
            //  Set theme here
            Theme = await _preferenceService.HasDarkModeSet() ? Color.SlateGray : Color.White;
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}