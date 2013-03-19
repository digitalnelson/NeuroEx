using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;

namespace NeuroEx.WPF.Behaviors
{
    [Export(typeof(AutoPopulateExportedViewsBehavior))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AutoPopulateExportedViewsBehavior : RegionBehavior, IPartImportsSatisfiedNotification
    {
		public void OnImportsSatisfied()
		{ AddRegisteredViews(); }

        protected override void OnAttach()
        { AddRegisteredViews(); }

        private void AddRegisteredViews()
        {
        	// Make sure this behavior is connected to a region
        	if (Region == null) return;

        	// Loop through our lazy list of all views
        	foreach (var viewEntry in RegisteredViews)
        	{
        		// Check to see if each view is linked to this region
        		if (viewEntry.Metadata.RegionName != Region.Name) continue;

        		// Force our lazy list to load up our view
        		var view = viewEntry.Value;  //TODO: Make this bubble better if there is a problem loading the view

        		// Register the view with the region
        		if (!Region.Views.Contains(view))
        			Region.Add(view);
        	}
        }

    	[ImportMany(AllowRecomposition = true)]
        public Lazy<object, IViewRegionRegistration>[] RegisteredViews { get; set; }
    }
}
