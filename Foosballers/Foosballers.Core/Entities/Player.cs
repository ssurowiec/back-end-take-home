namespace Foosballers.Core.Entities;

public class Player : IEquatable<Player>
{
    private string _name;

    private Player(string name)
    {
        _name = name;
    }

    public bool Equals(Player? other)
    {
        return other is not null && _name.Equals(other._name);
    }

    public override string ToString()
    {
        return _name;
    }

    public static implicit operator string(Player player)
        => player._name;

    public static implicit operator Player(string name)
        => new(name);

    public override bool Equals(object obj)
    {
        return Equals(obj as Player);
    }
}