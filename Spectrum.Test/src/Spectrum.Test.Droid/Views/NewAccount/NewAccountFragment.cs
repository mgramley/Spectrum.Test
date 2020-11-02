using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.ViewModels;
using Spectrum.Test.Core.ViewModels;
using Spectrum.Test.Core.ViewModels.AccountCreation;
using Spectrum.Test.Core.ViewModels.Main;
using DialogFragment = AndroidX.Fragment.App.DialogFragment;

namespace Spectrum.Test.Droid.Views.NewAccount
{
    [MvxFragmentPresentation(typeof(MainContainerViewModel), Resource.Id.content_frame, true)]
    public class NewAccountFragment : BaseFragment<NewAccountViewModel>, DatePickerDialog.IOnDateSetListener
    {
        private EditText _dateEditText;

        protected override int FragmentLayoutId => Resource.Layout.fragment_new_account;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var rootView = base.OnCreateView(inflater, container, savedInstanceState);

            _dateEditText = rootView?.FindViewById<EditText>(Resource.Id.new_account_service_date_edit);
            if (_dateEditText != null)
            {
                _dateEditText.Focusable = false;
                _dateEditText.Click += (o, e) =>
                {
                    var dialog = new DatePickerDialogFragment(Activity, Convert.ToDateTime(_dateEditText.Text), this);
                    dialog.Show(ChildFragmentManager, "date");
                };
            }

            return rootView;
        }

        private class DatePickerDialogFragment : DialogFragment
        {
            private readonly Context _context;
            private DateTime _date;
            private readonly DatePickerDialog.IOnDateSetListener _listener;

            public DatePickerDialogFragment(Context context, DateTime date, DatePickerDialog.IOnDateSetListener listener)
            {
                _context = context;
                _date = date;
                _listener = listener;
            }

            public override Dialog OnCreateDialog(Bundle savedInstanceState)
            {
                var dialog = new DatePickerDialog(_context, _listener, _date.Year, _date.Month - 1, _date.Day);
                return dialog;
            }
        }

        public void OnDateSet(Android.Widget.DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            _dateEditText.Text = new DateTime(year, monthOfYear + 1, dayOfMonth).Date.ToShortDateString();
        }

    }


}
