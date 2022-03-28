namespace Foosballers.Core.Entities;

internal class PlayerEqualityComparer : IEqualityComparer<Player>
{
    public bool Equals(Player? x, Player? y)
    {
        if (x is null || y is null)
        {
            return false;
        }

        return x.Equals(y);
    }

    public int GetHashCode(Player obj)
    {
        return obj.ToString().GetHashCode();
    }
}