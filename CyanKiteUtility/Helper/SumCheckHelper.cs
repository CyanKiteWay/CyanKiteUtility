using System.Collections.Generic;

namespace CyanKiteUtility
{
    public class SumCheckHelper
    {
        /// <summary>
        /// 计算求和校验码
        /// </summary>
        /// <param name="data">要校验的数据</param>
        /// <param name="startPosition">要校验的数据的开始位置</param>
        /// <param name="dataLength">要校验的数据的长度</param>
        /// <returns></returns>
        public static byte GetSumData(byte[] data, int startPosition, int dataLength)
        {
            int sum_data = 0;
            for (int i = 0; i < dataLength; i++)
            {
                sum_data += data[i + startPosition];
            }
            return (byte)sum_data;
        }

        /// <summary>
        /// 计算求和校验码
        /// </summary>
        /// <param name="dataList">要校验的数据</param>
        public static void GetSumData(List<byte> dataList)
        {
            int sum_data = 0;
            foreach (byte b in dataList)
            {
                sum_data += b;
            }
            dataList.Add((byte)sum_data);
        }


        /// <summary>
        /// 校验求和校验码
        /// </summary>
        /// <param name="data">校验数据</param>
        /// <param name="startPosition">数据开始位置</param>
        /// <param name="dataLength">数据长度</param>
        /// <returns></returns>
        public static bool CheckSumData(byte[] data, int startPosition, int dataLength)
        {
            int sum_data = 0;
            for (int i = startPosition; i < dataLength; i++)
            {
                sum_data += data[i];
            }
            return (byte)sum_data == data[startPosition + dataLength];
        }

        /// <summary>
        /// 校验求和校验码
        /// </summary>
        /// <param name="data">校验数据</param>
        /// <param name="startPosition">数据开始位置</param>
        /// <param name="dataLength">数据长度</param>
        /// <returns></returns>
        public static bool CheckSumData(List<byte> dataList)
        {
            int sum_data = 0;
            for (int i = 0; i < dataList.Count - 1; i++)
            {
                sum_data += dataList[i];
            }
            return (byte)sum_data == dataList[dataList.Count - 1];
        }
    }
}