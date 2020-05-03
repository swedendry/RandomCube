using System;

public static class ServerDefine
{
    public const int MAX_GAME_USER = 2;
    public const int MAX_ENTRY_SLOT = 5;
    public const int CUBE_PRICE = 1000;
    public const int CUBE_LV_PRICE = 100;
    public const int CUBE_NEED_SP = 10;
    public const int MONSTER_DIE_SP = 10;
    public const int SLOT_LV_PRICE = 100;
    public const int MONSTER_HP = 10;
    public const int MIN_REWARD_MONEY = 10;
    public const int MAX_REWARD_MONEY = 1000;

    public static int CubeLv2Price(byte lv)
    {
        if (lv < 1)
            return CUBE_PRICE;

        return (int)Math.Pow(2, lv - 1) * CUBE_LV_PRICE;
    }

    public static int SlotLv2Price(byte lv)
    {
        return lv * SLOT_LV_PRICE;
    }

    public static int CubeSeq2NeedSP(int seq)
    {
        return seq * CUBE_NEED_SP;
    }

    public static int MonsterSeq2HP(int seq)
    {
        return MONSTER_HP + (MONSTER_HP * (seq / 15));
    }

    public static float CubeLv2AD(float value, byte lv)
    {
        const float AD_MAX = 200f;
        return Math.Min(AD_MAX, value + (value * ((lv - 1) * 0.1f)));
    }

    public static float CubeLv2AS(float value, byte lv)
    {
        const float AS_MIN = 0.1f;
        return Math.Max(AS_MIN, value - (value * ((lv - 1) * 0.01f)));
    }

    public static float SlotLv2AD(float value, byte lv, byte slotLv)
    {
        const float AD_MAX = 200f;
        return Math.Min(AD_MAX, CubeLv2AD(value, lv) + ((slotLv - 1) * 0.2f));
    }

    public static float SlotLv2AS(float value, byte lv, byte slotLv)
    {
        const float AS_MIN = 0.1f;
        return Math.Max(AS_MIN, CubeLv2AS(value, lv) - ((slotLv - 1) * 0.02f));
    }

    public static int Rank2Money(int rank)
    {
        return MAX_REWARD_MONEY - ((MAX_REWARD_MONEY / 2) * rank);
    }

    public static int Time2Money(float time)
    {
        return MIN_REWARD_MONEY + (MIN_REWARD_MONEY * (int)time);
    }
}