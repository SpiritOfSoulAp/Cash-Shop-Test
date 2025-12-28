namespace Domain
{
    public class User
    {
        public long Id { get; }
        public int Cash { get; private set; }

        public User(long id, int cash)
        {
            Id = id;
            Cash = cash;
        }

        // Updates the user's cash after a server-confirmed operation.
        public void UpdateCash(int newCash)
        {
            Cash = newCash;
        }
    }
}
