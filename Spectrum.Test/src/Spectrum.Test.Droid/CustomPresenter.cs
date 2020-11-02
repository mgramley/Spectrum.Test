using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters;
using MvvmCross.ViewModels;
using Spectrum.Test.Core.ViewModels.PresentationHints;

namespace Spectrum.Test.Droid
{
    internal class CustomPresenter : MvxAndroidViewPresenter
    {
        public CustomPresenter(IEnumerable<Assembly> androidViewAssemblies) : base(androidViewAssemblies)
        {
        }

        public override Task<bool> ChangePresentation(MvxPresentationHint hint)
        {
            if (hint is PopBackStackHint)
            {
                this?.CurrentFragmentManager?.PopBackStack();
            }
            return base.ChangePresentation(hint);
        }
    }
}
