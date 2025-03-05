using System;

namespace CyanKiteUtility
{
    public class CRCCheckHelper
    {
        /// <summary>
        /// 计算CRC16校验码
        /// </summary>
        /// <param name="data">校验数据</param>
        /// <returns></returns>
        public static byte[] GetCRC16(byte[] data)
        {
            ushort register = 0xFFFF;
            ushort poly = 0xA001;

            for (int i = 0; i < data.Length; i++)
            {
                register = (ushort)(register ^ data[i]);

                for (int j = 0; j < 8; j++)
                {
                    bool endwith1 = (register & 0x01) == 0x01;
                    register = (ushort)(register >> 1);
                    if (endwith1)
                    {
                        register = (ushort)(register ^ poly);
                    }
                }
            }
            return BitConverter.GetBytes(register);
        }

        /// <summary>
        /// 确认CRC16校验码
        /// </summary>
        /// <param name="data">校验数据</param>
        /// <param name="dataPosition">数据开始位置</param>
        /// <param name="dataLength">数据长度</param>
        /// <returns></returns>
        public static bool CheckCRC16(byte[] data, int dataPosition, int dataLength)
        {
            ushort register = 0xFFFF;
            ushort poly = 0xA001;

            for (int i = 0; i < dataLength; i++)
            {
                register = (ushort)(register ^ data[i + dataPosition]);

                for (int j = 0; j < 8; j++)
                {
                    bool endwith1 = (register & 0x01) == 0x01;
                    register = (ushort)(register >> 1);
                    if (endwith1)
                    {
                        register = (ushort)(register ^ poly);
                    }
                }
            }
            var result = BitConverter.GetBytes(register);
            return (result[0] == data[dataPosition + dataLength]) && (result[1] == data[dataPosition + dataLength + 1]);
        }
    }
}