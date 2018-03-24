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
    public class AddEditBacklogViewModel : BindableBase, INavigationAware
    {
        //
        //  Instance Data
        private long _recordId;
        private long _bookId;
        private string _bookName;
        private string _isbn;
        private bool _isAdd;
        private bool _isEdit;
        private bool _showIsbnSearchForm;
        private bool _showRestOfAddForm;
        private double _rating;
        private int _selectedPickerIndex;
        private BacklogStatus _backlogStatus;
        private List<BacklogStatus> _backlogStatuses;
        private Color _theme;
        private readonly INavigationService _navigationService;
        private readonly IBacklogService _backlogService;
        private readonly IPageDialogService _pageDialogService;
        private readonly IPreferenceService _preferenceService;

        //
        //  props
        public Color Theme
        {
            get => _theme;
            set => SetProperty(ref _theme, value);
        }

        public int SelectedPickerIndex
        {
            get => _selectedPickerIndex;
            set => SetProperty(ref _selectedPickerIndex, value);
        }

        public long RecordId
        {
            get => _recordId;
            set => SetProperty(ref _recordId, value);
        }

        public bool ShowIsbnSearchForm
        {
            get => _showIsbnSearchForm;
            set => SetProperty(ref _showIsbnSearchForm, value);
        }

        public bool ShowRestOfAddForm
        {
            get => _showRestOfAddForm;
            set => SetProperty(ref _showRestOfAddForm, value);
        }

        public long BookId
        {
            get => _bookId;
            set => SetProperty(ref _bookId, value);
        }

        public string BookName
        {
            get => _bookName;
            set => SetProperty(ref _bookName, value);
        }

        public string Isbn
        {
            get => _isbn;
            set => SetProperty(ref _isbn, value);
        }

        public bool IsAdd
        {
            get => _isAdd;
            set => SetProperty(ref _isAdd, value);
        }

        public bool IsEdit
        {
            get => _isEdit;
            set => SetProperty(ref _isEdit, value);
        }

        public double Rating
        {
            get => _rating;
            set => SetProperty(ref _rating, value);
        }

        public BacklogStatus BacklogStatus
        {
            get => _backlogStatus;
            set => SetProperty(ref _backlogStatus, value);
        }

        public List<BacklogStatus> BacklogStatuses
        {
            get => _backlogStatuses;
            set => SetProperty(ref _backlogStatuses, value);
        } 

        //
        //  commands
        public ICommand SaveItemCommand { get; set; }
        public ICommand SearchIsbnCommand { get; set; }

        public AddEditBacklogViewModel(INavigationService navigationService, IBacklogService backlogService, IPageDialogService pageDialogService, IPreferenceService preferenceService) // INavigation navigation)
        {
            _navigationService = navigationService;
            _backlogService = backlogService;
            _pageDialogService = pageDialogService;
            _preferenceService = preferenceService;
            SaveItemCommand = new DelegateCommand(async () => await OnSaveItem());
            SearchIsbnCommand = new DelegateCommand(async () => await OnIsbnSearch());
            IsAdd = (RecordId <= 0);
            IsEdit = (RecordId > 0);
        }

        public async Task OnSaveItem()
        {
            try
            {
                if (RecordId > 0)
                {
                    await _backlogService.EditBacklogItem(RecordId,BacklogStatus.TypeId, Rating, BookId);
                }
                else
                {
                    await _backlogService.AddBacklogItem(BookId, BacklogStatus.TypeId, Rating);
                }
                await _pageDialogService.DisplayAlertAsync("Success", "Successful changes", "OK");
            }
            catch (Exception ex)
            {
                await _pageDialogService.DisplayAlertAsync("Errors", $"errors: {ex.Message}", "OK");
            }
            await _navigationService.GoBackAsync();
        }

        public async Task OnIsbnSearch()
        {
            if (string.IsNullOrEmpty(Isbn))
            {
                await _pageDialogService.DisplayAlertAsync("Error", "ISBN is required", "OK");
            }
            else
            {
                try
                {
                    var book = await _backlogService.FindBookByIsbn(Isbn);
                    ShowIsbnSearchForm = false;
                    ShowRestOfAddForm = true;
                    BookId = book.BookId;
                    BookName = book.BookTitle;
                }
                catch (Exception ex)
                {
                    await _pageDialogService.DisplayAlertAsync("Errors", $"errors: {ex.Message}", "OK");
                }
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            BacklogStatuses = await _backlogService.GetBacklogStatuses();
            SelectedPickerIndex = 0;
            if ($"{parameters["Title"]}" == "Add")
            {
                IsAdd = true;
                IsEdit = false;
                ShowIsbnSearchForm = true;
                ShowRestOfAddForm = false;
            }
            else
            {
                IsAdd = false;
                IsEdit = true;
                ShowIsbnSearchForm = false;
                ShowRestOfAddForm = false;

                if (!(parameters["Item"] is BacklogItem data)) return;
                RecordId = data.RecordId;
                BookId = data.Book.BookId;
                BookName = data.Book.BookTitle;
                for (var i=0; i<BacklogStatuses.Count; i++)
                {
                    if (BacklogStatuses.ElementAt(i).TypeId == data.Status.TypeId)
                    {
                        SelectedPickerIndex = i;
                    }
                }
                Rating = data.Rating;
                BacklogStatus = data.Status;
            }

            //
            //  Set theme here
            Theme = await _preferenceService.HasDarkModeSet() ? Color.SlateGray : Color.White;
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}