namespace escout.Services.Dependency
{
    public interface IToast
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}
