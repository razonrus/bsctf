namespace SearchEngine
{
    public interface ISearch
    {
        User[] Search(string query);
    }

    public class DefaultSearch: ISearch
    {
        public User[] Search(string query)
        {
            return new User[0];
        }
    }
}