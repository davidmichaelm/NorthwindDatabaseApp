using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindDatabaseApp.UI.Menus
{
    /*
     * Usage: Create method returns a cancellation source and starts the loading animation.
     * Cancel the cancellation source once loading is finished.
     */
    public class Loading
    {
        private CancellationToken Token { get; set; }
        private IDisplay Display { get; set; }

        private Loading(IDisplay display, CancellationToken token)
        {
            Token = token;
            Display = display;
            Run();
        }
        
        public static CancellationTokenSource Create(IDisplay display)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var loading = new Loading(display, token);
            return cts;
        }

        private async Task Run()
        {
            while (!Token.IsCancellationRequested)
            {
                Display.ShowMessage("\rFetching... /");
                await Task.Delay(100, Token);
                Display.ShowMessage("\rFetching... —");
                await Task.Delay(100, Token);
                Display.ShowMessage("\rFetching... \\");
                await Task.Delay(100, Token);
                Display.ShowMessage("\rFetching... |");
                await Task.Delay(100, Token);
            }
        }
    }
}