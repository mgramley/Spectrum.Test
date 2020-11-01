using MvvmCross.Platforms.Uap.Core;
using MvvmCross.Platforms.Uap.Views;

namespace Spectrum.Test.UWP
{
    sealed partial class App
    {
        public App()
        {
            InitializeComponent();
        }
    }

    public abstract class Spectrum.TestApp : MvxApplication<Setup, Core.App>
    {
    }
}
