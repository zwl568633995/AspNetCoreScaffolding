using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.Utility.Extensions
{
    public static class IntExtension
    {
        /// <summary>
        ///     以字节数组的形式返回指定的带符号整数值。
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>An array of bytes with length 2.</returns>
        public static byte[] GetBytes(this int value)
        {
            return BitConverter.GetBytes(value);
        }

        /// <summary>
        ///     取绝对值
        /// </summary>
        /// <param name="value">A number that is greater than , but less than or equal to .</param>
        /// <returns>A  signed integer, x, such that 0 ? x ?.</returns>
        public static int Abs(this int value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        ///     取最大值
        /// </summary>
        /// <param name="val1">The first of two  signed integers to compare.</param>
        /// <param name="val2">The second of two  signed integers to compare.</param>
        /// <returns>Parameter  or , whichever is larger.</returns>
        public static int Max(this int val1, int val2)
        {
            return Math.Max(val1, val2);
        }

        /// <summary>
        ///     取最小值
        /// </summary>
        /// <param name="val1">The first of two  signed integers to compare.</param>
        /// <param name="val2">The second of two  signed integers to compare.</param>
        /// <returns>Parameter  or , whichever is smaller.</returns>
        public static int Min(this int val1, int val2)
        {
            return Math.Min(val1, val2);
        }

        /// <summary>
        ///     获取数字的正负，返回-1小于0，返回1 大于0
        /// </summary>
        /// <param name="value">A signed number.</param>
        /// <returns>
        ///     A number that indicates the sign of , as shown in the following table.Return value Meaning -1  is less than
        ///     zero. 0  is equal to zero. 1  is greater than zero.
        /// </returns>
        public static int Sign(this int value)
        {
            return Math.Sign(value);
        }
    }
}
