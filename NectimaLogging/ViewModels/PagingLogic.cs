using NectimaLogging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NectimaLogging.ViewModels
{
    public class PagingLogic
    {
        private int _next;
        private int _previous;
        private bool _isNext;
        private bool _isPrevious;
        private int _addP;
        private int _pageSize;
        private int _logPage;
        private ILogEntryRepository _logEntryRepository;

        public PagingLogic
            (int next, int previous, bool isNext, bool isPrevious, int addP,
            int pageSize, ILogEntryRepository logEntryRepository)
        {
            _next = next;
            _previous = previous;
            _isNext = isNext;
            _isPrevious = isPrevious;
            _addP = addP;
            _pageSize = pageSize;
            _logEntryRepository = logEntryRepository;

        }
        public PagingLogic(int logPage)
        {
            _logPage = logPage;
        }

        public LogListViewModel LogListViewModel()
        {
            var b = new PagingInfo();
            if (_isNext)
            {
                ++_next;

                return (new LogListViewModel
                {

                    Logs = _logEntryRepository.GetAllLogs
                .OrderBy(p => p.Id)
                .Skip((_next - 1) * _pageSize)
                .Take(_pageSize),
                    PagingInfo = new PagingInfo
                    {
                        AddMorePages = _next,
                        IsNext = _isNext,
                        CurrentPage = _next,
                        ItemsPerPage = _pageSize,
                        TotalItems = _logEntryRepository.GetAllLogs.Count()
                    }
                });

            }
            else if (_isPrevious)
            {
                if (_previous <= 2)
                    _previous = 2;
                _previous--;
                return (new LogListViewModel
                {

                    Logs = _logEntryRepository.GetAllLogs
                .OrderBy(p => p.Id)
                .Skip((_previous - 1) * _pageSize)
                .Take(_pageSize),
                    PagingInfo = new PagingInfo
                    {
                        AddMorePages = _previous,
                        IsPrevious = _isPrevious,
                        CurrentPage = _previous,
                        ItemsPerPage = _pageSize,
                        TotalItems = _logEntryRepository.GetAllLogs.Count()
                    }                  
            }) ;
            }
            return null;
        }

    }
}
