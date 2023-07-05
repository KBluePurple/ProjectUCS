[System.Serializable]
public class BossStats {


    public float Hp { get; private set; }
    public float Mp { get; private set; }
    public float Def { get; private set; }
    public float Atk { get; private set; }
    public float MoveSpeed { get; private set; }
    public float JumpPower { get; private set; }
    public float Foc { get; private set; }

    public BossStats(float hp, float mp, float def, float atk, float moveSpeed, float jumpPower, float foc) {
        Hp = hp;
        Mp = mp;
        Def = def;
        Atk = atk;
        MoveSpeed = moveSpeed;
        JumpPower = jumpPower;
        Foc = foc;
    }
}
