using System;
using System.Collections.Generic;
using SmartSaver.Presentation.Models;

namespace SmartSaver.Presentation.Shared.Breadcrumbs
{
    public interface IBreadcrumbService
    {
        event Action BreadcrumbsChanged;

        void InvokeBreadcrumbsChanged();

        public void AddBreadcrumb(string breadcrumbDisplayName, string url);

        public void ClearList();

        IReadOnlyList<Breadcrumb> GetBreadcrumbsList();
    }
}