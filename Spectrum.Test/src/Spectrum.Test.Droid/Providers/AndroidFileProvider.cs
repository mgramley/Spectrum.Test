using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Spectrum.Test.Core.Providers;

namespace Spectrum.Test.Droid.Providers
{
    [ExcludeFromCodeCoverage]
    public class AndroidFileProvider : FileProvider, IFileProvider
    {
        private readonly ISharedPreferences _preferences;

        protected static string CsvMimeType => "text/csv";
        protected override string LocalDirectoryPath { get; }

        protected string ReadPermission => Manifest.Permission.ReadExternalStorage;
        protected string WritePermission => Manifest.Permission.WriteExternalStorage;

        public AndroidFileProvider(ISystemIO systemIO) : base(systemIO)
        {
            LocalDirectoryPath = Application.Context.FilesDir.Path;
            _preferences = Application.Context.GetSharedPreferences("Spectrum.Test.Droid", FileCreationMode.Private);
        }

    }
}
