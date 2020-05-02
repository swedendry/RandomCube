﻿using System;

public static class ServerDefine
{
    public const int MAX_GAME_USER = 2;
    public const int MAX_ENTRY_SLOT = 5;
    public const int CUBE_PRICE = 1000;
    public const int CUBE_LV_PRICE = 100;
    public const int CUBE_NEED_SP = 10;
    public const int MONSTER_DIE_SP = 10;
    public const int SLOT_LV_PRICE = 100;

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

    public static int Seq2NeedSP(int seq)
    {
        return seq * CUBE_NEED_SP;
    }
}