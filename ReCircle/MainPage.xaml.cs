using ReCircle.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Credentials;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ReCircle
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public static string PreferredUsername = "";
        private string ResourceName = "ReCircle";

        public MainPage()
        {
            this.InitializeComponent();
            DummyData.Setup();
            //TODO: Get rid of all references to DummyData
        }

        public MainPage(String s)
        {
            ;
        }

        private async void Init()
        {
            try
            {
                var x = await Configuration.CheckUpdate();

                if (x.IsUpdate)
                {
                    ExtraInfoMessage.Text = x.UpdateMessage;
                    LinkToUpdate.Content = new Uri(x.Url);
                    await updateAvailableDialog.ShowAsync();
                }

                Configuration.Setup();
                //TODO: Alow for automatic logging in
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An exception occurred trying to find credentials: {ex}");
            }
        }
        private void _lnkToCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CreateAccount));
        }

        private async void _btnLogin_Click(object sender, RoutedEventArgs e)
        {
            await TryLogin(usernameTextBox.Text, passwordTextBox.Password);
        }

        private void NavigateToHomePage()
        {
            var rootFrame = Window.Current.Content as Frame;
            var mainPage = rootFrame.Content as MainPage;
            rootFrame.Navigate(typeof(HomePage));
        }

        private async Task TryLogin(string username, string password)
        {
            try
            {
                var success = await Authentication.Login(username, password);

                if (success)
                {
                    var frame = (Frame)Window.Current.Content;
                    frame.Navigate(typeof(HomePage));
                    return;
                }
                else
                {
                    //prompt login failed
                    LoginFailed.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An exception occurred on attempted login: {ex}");
            }
            return;
        }

        private async Task GetCredentialsAndLogin()
        {
            var loginCredenital = GetCredentialFromLocker();

            if (loginCredenital != null)
            {
                loginCredenital.RetrievePassword();
            }
            else
            {
                //get a credential to then store
                return;
            }
            MainPage mainPage = new MainPage("");

            await mainPage.TryLogin(loginCredenital.UserName, loginCredenital.Password);

        }

        private PasswordCredential GetCredentialFromLocker()
        {
            PasswordCredential credential = null;

            var vault = new PasswordVault();

            try
            {
                var credentialList = vault.FindAllByResource(ResourceName);
                Debug.WriteLine($"Credentials recieved from locker");

                if (credentialList.Count > 0)
                {
                    if (credentialList.Count == 1)
                    {
                        credential = credentialList[0];
                    }
                    else
                    {
                        // When there are multiple usernames,
                        // retrieve the default username. If one doesn't
                        // exist, then display UI to have the user select
                        // a default username.

                        credential = vault.Retrieve(ResourceName, MainPage.PreferredUsername);
                    }
                }

                return credential;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"There was a problem fetching passwords for auto-login: {ex}");
            }
            return credential;

        }

        private void StoreCredentials(string userName, string password)
        {
            Debug.WriteLine($"Storing username: {userName} and password: {password}");
            var vault = new PasswordVault();
            vault.Add(new PasswordCredential(ResourceName, userName, password));
        }

        public bool DeleteCredentials()
        {
            try
            {
                var vault = new PasswordVault();
                vault.Remove(GetCredentialFromLocker());

                if (vault == null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"There was an issue deleting the stored credentials: {ex}");
            }
            return false;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Contact",
                Content = "Have a question?\n\nContact the develeoper of this application at: jrknight@damonteranchhs.com",
                PrimaryButtonText = "Ok"

            };
            await dialog.ShowAsync();
        }

        private async void LinkToUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Uri linkToLaunch = new Uri(LinkToUpdate.Content.ToString());

                var success = await Windows.System.Launcher.LaunchUriAsync(linkToLaunch);

                if (success)
                {
                    Debug.WriteLine("Success opening web browser!");
                }
                else
                {
                    Debug.WriteLine("Couldn't open web browser!");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An exception occurred trying to open the web browser: {ex}");
            }
        }
    }
}
