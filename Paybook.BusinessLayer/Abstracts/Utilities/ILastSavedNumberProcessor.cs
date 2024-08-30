namespace Paybook.BusinessLayer.Abstracts.Utilities
{
    public interface ILastSavedNumberProcessor
    {
        string GetNewNumberByType(string username, string type);
        //void Update(LastSavedNumberModel model);
    }
}
