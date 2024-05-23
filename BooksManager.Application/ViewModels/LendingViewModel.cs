namespace BooksManager.Application.ViewModels
{
    public class LendingViewModel(Guid id, Guid userId, Guid bookId, DateTime returnDate)
    {
        public Guid Id { get; private set; } = id;
        public Guid UserId { get; private set; } = userId;
        public Guid BookId { get; private set; } = bookId;
        public DateTime ReturnDate { get; private set; } = returnDate;
    }
}
