using System;

namespace Service.Services
{
    public static class ServerDefine
    {
        public const int MAX_GAME_USER = 2;
        public const int MAX_ENTRY_SLOT = 5;
        public const int CUBE_PRICE = 1000;
        public const int CUBE_LV_PRICE = 100;

        public static int Lv2Price(byte lv)
        {
            if (lv < 1)
                return CUBE_PRICE;

            return (int)Math.Pow(2, lv - 1) * CUBE_LV_PRICE;
        }
    }
}
