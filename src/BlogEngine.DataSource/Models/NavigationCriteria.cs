namespace BlogEngine.DataSource.Models;
public class NavigationCriteria
{
    public int Take { get; set; } = 20;
    public int Skip { get; set; } = 0;
    public DateCreationOrderDirection OrderDirection { get; set; } = DateCreationOrderDirection.Descending;
}

public enum DateCreationOrderDirection
{
    Ascending,
    Descending
}
