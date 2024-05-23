using System.ComponentModel;

namespace BooksManager.Core.Enums
{
    public enum Roles
    {
        [Description("Administrator")]
        Administrator = 1,
        [Description("Customer")]
        Customer = 2
    }
}
