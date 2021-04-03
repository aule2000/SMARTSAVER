using System;
using System.Collections.Generic;
using SmartSaver.Presentation.Models;

namespace SmartSaver.Presentation.Shared.Breadcrumbs
{
    public class BreadcrumbService : IBreadcrumbService
    {
        private readonly List<Breadcrumb> _breadcrumbsList = new List<Breadcrumb>();
        public event Action BreadcrumbsChanged;

        public void InvokeBreadcrumbsChanged()
        {
            BreadcrumbsChanged?.Invoke();
        }

        public void AddBreadcrumb(string breadcrumbDisplayName, string url)
        {
            _breadcrumbsList.Add(new Breadcrumb
            {
                BreadcrumbDisplayName = breadcrumbDisplayName,
                Url = url
            });
        }

        public void ClearList()
        {
            _breadcrumbsList.Clear();
        }

        public IReadOnlyList<Breadcrumb> GetBreadcrumbsList()
        {
            return _breadcrumbsList;
        }
    }
}