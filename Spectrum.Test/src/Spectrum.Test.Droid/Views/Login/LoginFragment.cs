using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Spectrum.Test.Core.ViewModels;
using Spectrum.Test.Core.ViewModels.Main;

namespace Spectrum.Test.Droid.Views.Login
{
    [MvxFragmentPresentation(typeof(MainContainerViewModel), Resource.Id.content_frame, true)]
    public class LoginFragment : BaseFragment<LoginViewModel>
    {
        protected override int FragmentLayoutId => Resource.Layout.fragment_sign_in;
    }
}
