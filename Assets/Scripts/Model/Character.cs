public class Character
{
    public int Health { get; private set; }

    public float MoveSpeed { get; private set; }

    public float JumpVelocity { get; private set; }

    public Character(int health, float maxMoveSpeed, float jumpVelocity)
    {
        Health = health;
        MoveSpeed = maxMoveSpeed;
        JumpVelocity = jumpVelocity;
    }
}