using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Spectrum.Test.Core.ViewModels.AccountCreation;
using Spectrum.Test.Core.ViewModels.Main;

namespace Spectrum.Test.Droid.Views.NewAccount
{
    [MvxFragmentPresentation(typeof(MainContainerViewModel), Resource.Id.content_frame, true)]
    public class AccountCreationSuccessFragment : BaseFragment<AccountCreationSuccessViewModel>
    {
        protected override int FragmentLayoutId => Resource.Layout.fragment_account_creation_success;
    }
}
