namespace Project.Scripts.MVVM.UI
{
    public interface IWindowBinder
    {
        public void Bind(WindowViewModel viewModel);
        public void Close();
    }
}
