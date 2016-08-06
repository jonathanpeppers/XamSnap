using Foundation;
using System;
using UIKit;

namespace XamSnap.iOS
{
    public partial class MessagesController : UITableViewController
    {
        readonly MessageViewModel messageViewModel = ServiceContainer.Resolve<MessageViewModel>();

        public MessagesController(IntPtr handle) : base(handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            TableView.Source = new TableSource();
        }

        public async override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            Title = messageViewModel.Conversation.UserName;
            try
            {
                await messageViewModel.GetMessages();
                TableView.ReloadData();
            }
            catch (Exception exc)
            {
                new UIAlertView("Oops!", exc.Message, null, "Ok").Show();
            }
        }

        class TableSource : UITableViewSource
        {
            const string MyCellName = "MyCell";
            const string TheirCellName = "TheirCell";

            readonly MessageViewModel messageViewModel = ServiceContainer.Resolve<MessageViewModel>();
            readonly ISettings settings = ServiceContainer.Resolve<ISettings>();

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return messageViewModel.Messages == null ? 0 : messageViewModel.Messages.Length;
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var message = messageViewModel.Messages[indexPath.Row];
                bool isMyMessage = message.UserName == settings.User.Name;
                var cell = tableView.DequeueReusableCell(isMyMessage ? MyCellName : TheirCellName);
                cell.TextLabel.Text = message.Text;
                return cell;
            }
        }
    }
}