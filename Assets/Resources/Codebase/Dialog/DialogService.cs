public class DialogService
{
    private DialogSystemView _dialogSystemView;
    private DialogEntry _chainedEntry;
    private DialogDatabase _dialogDatabase;
    public DialogService(DialogSystemView dialogSystemView, DialogDatabase dialogDatabase)
    {
        _dialogSystemView = dialogSystemView;
        _dialogSystemView.ClickableZone.onClick.AddListener(ClickableZoneHandler);
        _dialogDatabase = dialogDatabase;
    }

    private void ClickableZoneHandler()
    {
        if (_chainedEntry == null)
            HideView();
        else
            ShowDialog(_chainedEntry);
    }

    private void HideView()
    {
        _dialogSystemView.gameObject.SetActive(false);
    }
    public void ShowDialog(DialogEntry dialogEntry)
    {
        if (_dialogSystemView.gameObject.activeSelf == false)
            _dialogSystemView.gameObject.SetActive(true);

        SetLeftActor(dialogEntry);
        SetRightActor(dialogEntry);

        _dialogSystemView.Text.text = dialogEntry.Text;
        _chainedEntry = dialogEntry.ChainedEntry;
    }

    private void SetRightActor(DialogEntry dialogEntry)
    {
        _dialogSystemView.RightActor.sprite = dialogEntry.RightActor;
        if (dialogEntry.RightActor == null)
            HideRightActor();
        else
            ShowRightActor();
    }

    private void HideRightActor() => _dialogSystemView.RightActor.gameObject.SetActive(false);

    private void ShowRightActor() => _dialogSystemView.RightActor.gameObject.SetActive(true);

    private void SetLeftActor(DialogEntry dialogEntry)
    {
        _dialogSystemView.LeftActor.sprite = dialogEntry.LeftActor;

        if (dialogEntry.LeftActor == null)
            HideLeftActor();
        else
            ShowLeftActor();
    }
    private void ShowLeftActor() => _dialogSystemView.LeftActor.gameObject.SetActive(true);
    private void HideLeftActor() => _dialogSystemView.LeftActor.gameObject.SetActive(false);
}
