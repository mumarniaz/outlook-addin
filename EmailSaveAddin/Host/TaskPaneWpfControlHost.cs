using EmailSaveAddin.Extensions;
using EmailSaveAddin.Views;
using System.Windows;
using System.Windows.Forms;

namespace EmailSaveAddin.Host
{
    public partial class TaskPaneWpfControlHost : UserControl
    {
        private MainPanel mainPanel;

        public string ContextId { get; set; }
        public TaskPaneWpfControlHost(string contextId)
        {
            InitializeComponent();

            ContextId = contextId;

            CreateControls();
            SetUserControl();
        }

        private void SetUserControl()
        {
            // Broadcast login
            //MessengerHelper.BroadcastMessage(new LoginMessage() { IsLoggedIn = backdropService.IsLoggedIn() }, ContextId);
            SetChild(mainPanel);
        }

        private void CreateControls()
        {
            mainPanel = new MainPanel();
        }

        private void SetChild(UIElement child)
        {
            if (IsHandleCreated)
            {
                this.InvokeIfRequired(() => elementHost.Child = child);
            }
            else
            {
                HandleCreated += (sender, args) => SetChild(child);
            }
        }
    }
}
