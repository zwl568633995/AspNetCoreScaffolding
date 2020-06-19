using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;


namespace Kay.Framework.Utility.Extensions
{
    public static class StringExtension
    {
        #region 格式化

        /// <summary>
        ///     用指定对象的字符串表示形式替换指定字符串中的一个或多个格式项
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The object to format.</param>
        /// <returns>A copy of  in which any format items are replaced by the string representation of .</returns>
        public static string Format(this string format, object arg0)
        {
            return string.Format(format, arg0);
        }

        #endregion

        #region 计算

        /// <summary>
        ///     指定超长字符使用指定字符代替
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="suffix">The suffix.</param>
        /// <returns>A string.</returns>
        public static string Truncate(this string @this, int maxLength, string suffix = "...")
        {
            if (@this == null || @this.Length <= maxLength) return @this;

            var strLength = maxLength - suffix.Length;
            return @this.Substring(0, strLength) + suffix;
        }

        /// <summary>
        ///     得到字符串长度，一个汉字长度为2
        /// </summary>
        /// <param name="inputString">参数字符串</param>
        /// <returns></returns>
        public static int GetLength(string inputString)
        {
            var ascii = new ASCIIEncoding();
            var tempLen = 0;
            var s = ascii.GetBytes(inputString);
            foreach (var t in s)
                if (t == 63)
                    tempLen += 2;
                else
                    tempLen += 1;

            return tempLen;
        }


        /// <summary>
        ///     获取指定字符之后的所有字符
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="value">The value to search.</param>
        /// <returns>The string after the specified value.</returns>
        public static string GetAfter(this string @this, string value)
        {
            return @this.IndexOf(value, StringComparison.Ordinal) == -1
                ? ""
                : @this.Substring(@this.IndexOf(value, StringComparison.Ordinal) + value.Length);
        }

        /// <summary>
        ///     获取指定字符之前的所有字符
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="value">The value to search.</param>
        /// <returns>The string before the specified value.</returns>
        public static string GetBefore(this string @this, string value)
        {
            return @this.IndexOf(value, StringComparison.Ordinal) == -1
                ? ""
                : @this.Substring(0, @this.IndexOf(value, StringComparison.Ordinal));
        }

        /// <summary>
        ///     获取指定字符之间的字符
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="before">The string before to search.</param>
        /// <param name="after">The string after to search.</param>
        /// <returns>The string between the two specified string.</returns>
        public static string GetBetween(this string @this, string before, string after)
        {
            var beforeStartIndex = @this.IndexOf(before, StringComparison.Ordinal);
            var startIndex = beforeStartIndex + before.Length;
            var afterStartIndex = @this.IndexOf(after, startIndex, StringComparison.Ordinal);

            if (beforeStartIndex == -1 || afterStartIndex == -1) return "";

            return @this.Substring(startIndex, afterStartIndex - startIndex);
        }

        #endregion

        #region 判断

        /// <summary>
        ///     判断字符串是否空字符串
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if a not is empty, false if not.</returns>
        public static bool IsNotEmpty(this string @this)
        {
            return @this != string.Empty;
        }

        /// <summary>
        ///     判断字符串是否不是NULL或者空字符串
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if '@this' is not (null or empty), false if not.</returns>
        public static bool IsNotNullOrEmpty(this string @this)
        {
            return !string.IsNullOrEmpty(@this);
        }

        /// <summary>
        ///     判断字符串是否是NULL或者空字符串
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if '@this' is null or is empty, false if not.</returns>
        public static bool IsNullOrEmpty(this string @this)
        {
            return string.IsNullOrEmpty(@this);
        }


        /// <summary>
        ///     判断指定字符是否是空格
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>true if the character at position  in  is white space; otherwise, false.</returns>
        public static bool IsWhiteSpace(this string s, int index)
        {
            return char.IsWhiteSpace(s, index);
        }

        /// <summary>
        ///     判断字符串是否Like目标字符串
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="pattern">The pattern to use. Use '*' as wildcard string.</param>
        /// <returns>true if '@this' satisfy the specified pattern, false if not.</returns>
        public static bool IsLike(this string @this, string pattern)
        {
            // Turn the pattern into regex pattern, and match the whole string with ^$
            var regexPattern = "^" + Regex.Escape(pattern) + "$";

            // Escape special character ?, #, *, [], and [!]
            regexPattern = regexPattern.Replace(@"\[!", "[^")
                .Replace(@"\[", "[")
                .Replace(@"\]", "]")
                .Replace(@"\?", ".")
                .Replace(@"\*", ".*")
                .Replace(@"\#", @"\d");

            return Regex.IsMatch(@this, regexPattern);
        }

        /// <summary>
        ///     判断字符串是否在目标字符串类包含
        /// </summary>
        /// <param name="this">The object to be compared.</param>
        /// <param name="values">The value list to compare with the object.</param>
        /// <returns>true if the values list contains the object, else false.</returns>
        public static bool IsIn(this string @this, params string[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }

        /// <summary>
        ///     判断指定字符串是否不为Null
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if not null, false if not.</returns>
        public static bool IsNotNull(this string @this)
        {
            return @this != null;
        }

        /// <summary>
        ///     判断指定字符串是否为Null
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if null, false if not.</returns>
        public static bool IsNull(this string @this)
        {
            return @this == null;
        }

        /// <summary>
        ///     判断指定字符是否在字符串内
        /// </summary>
        /// <param name="this">The object to be compared.</param>
        /// <param name="values">The value list to compare with the object.</param>
        /// <returns>true if the values list doesn't contains the object, else false.</returns>
        public static bool IsNotIn(this string @this, params string[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }


        /// <summary>
        ///     判断除去空格字符串是否为Null或者空
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>true if the  parameter is null or , or if  consists exclusively of white-space characters.</returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        ///     判断除去空格字符串是否不为Null或者空
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>true if the  parameter is null or , or if  consists exclusively of white-space characters.</returns>
        public static bool IsNotNullOrWhiteSpace(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }


        /// <summary>
        ///     判断字符串是否满足正则表达式
        /// </summary>
        /// <param name="input">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <returns>true if the regular expression finds a match; otherwise, false.</returns>
        public static bool IsMatch(this string input, string pattern)
        {
            return Regex.IsMatch(input, pattern);
        }

        /// <summary>
        ///     判断字符串是否满足正则表达式
        /// </summary>
        /// <param name="input">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
        /// <returns>true if the regular expression finds a match; otherwise, false.</returns>
        public static bool IsMatch(this string input, string pattern, RegexOptions options)
        {
            return Regex.IsMatch(input, pattern, options);
        }

        #endregion

        #region 字符串操作

        /// <summary>
        ///     按照指定分割符生成字符串数组
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="separator">A string that delimit the substrings in this string.</param>
        /// <param name="option">
        ///     (Optional) Specify RemoveEmptyEntries to omit empty array elements from the array returned,
        ///     or None to include empty array elements in the array returned.
        /// </param>
        /// <returns>
        ///     An array whose elements contain the substrings in this string that are delimited by the separator.
        /// </returns>
        public static string[] Split(this string @this, string separator,
            StringSplitOptions option = StringSplitOptions.None)
        {
            return @this.Split(new[] { separator }, option);
        }

        /// <summary>
        ///     获取右边指定长度所有字符（最大值为字符串长度）
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="length">The length.</param>
        /// <returns>A string.</returns>
        public static string RightSafe(this string @this, int length)
        {
            return @this.Substring(Math.Max(0, @this.Length - length));
        }

        /// <summary>
        ///     获取右边指定长度所有字符
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="length">The length.</param>
        /// <returns>The right part.</returns>
        public static string Right(this string @this, int length)
        {
            return @this.Substring(@this.Length - length);
        }

        /// <summary>
        ///     字符串反排序
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The string reversed.</returns>
        public static string Reverse(this string @this)
        {
            if (@this.Length <= 1) return @this;

            var chars = @this.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        /// <summary>
        ///     替换最后一个指定字符
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns>The string with the last occurence of old value replace by new value.</returns>
        public static string ReplaceLast(this string @this, string oldValue, string newValue)
        {
            var startindex = @this.LastIndexOf(oldValue, StringComparison.Ordinal);

            if (startindex == -1) return @this;

            return @this.Remove(startindex, oldValue.Length).Insert(startindex, newValue);
        }

        /// <summary>
        ///     替换最后若干个指定字符
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="number">Number of.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns>The string with the last numbers occurences of old value replace by new value.</returns>
        public static string ReplaceLast(this string @this, int number, string oldValue, string newValue)
        {
            var list = @this.Split(oldValue).ToList();
            var old = Math.Max(0, list.Count - number - 1);
            var listStart = list.Take(old);
            var listEnd = list.Skip(old);

            return string.Join(oldValue, listStart) +
                   (old > 0 ? oldValue : "") +
                   string.Join(newValue, listEnd);
        }

        /// <summary>
        ///     替换首个字符
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns>The string with the first occurence of old value replace by new value.</returns>
        public static string ReplaceFirst(this string @this, string oldValue, string newValue)
        {
            var startindex = @this.IndexOf(oldValue, StringComparison.Ordinal);

            if (startindex == -1) return @this;

            return @this.Remove(startindex, oldValue.Length).Insert(startindex, newValue);
        }

        /// <summary>
        ///     替换开始若干个字符
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="number">Number of.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns>The string with the numbers of occurences of old value replace by new value.</returns>
        public static string ReplaceFirst(this string @this, int number, string oldValue, string newValue)
        {
            var list = @this.Split(oldValue).ToList();
            var old = number + 1;
            var listStart = list.Take(old);
            var listEnd = list.Skip(old);

            var enumerable = listEnd.ToList();
            return string.Join(newValue, listStart) +
                   (enumerable.Any() ? oldValue : string.Empty) +
                   string.Join(oldValue, enumerable);
        }

        /// <summary>
        ///     替换指定字符为空字符
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>A string with all specified values replaced by an empty string.</returns>
        public static string ReplaceByEmpty(this string @this, params string[] values)
        {
            return values.Aggregate(@this, (current, value) => current.Replace(value, string.Empty));
        }

        /// <summary>
        ///     移除首个空格
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A string with all specified values replaced by an empty string.</returns>
        public static string RemoveFirstWhiteSpace(this string @this)
        {
            if (!@this.IsNotNullOrEmpty()) return @this;
            return char.IsWhiteSpace(@this[0]) ? @this.Right(@this.Length - 1) : @this;
        }

        /// <summary>替换指定位置指定长度的字符为目标字符</summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="length">The length.</param>
        /// <param name="value">The value.</param>
        /// <returns>A string.</returns>
        public static string Replace(this string @this, int startIndex, int length, string value)
        {
            @this = @this.Remove(startIndex, length).Insert(startIndex, value);

            return @this;
        }

        /// <summary>
        ///     重复生成字符
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="repeatCount">Number of repeats.</param>
        /// <returns>The repeated string.</returns>
        public static string Repeat(this string @this, int repeatCount)
        {
            if (@this.Length == 1) return new string(@this[0], repeatCount);

            var sb = new StringBuilder(repeatCount * @this.Length);
            while (repeatCount-- > 0) sb.Append(@this);

            return sb.ToString();
        }

        /// <summary>
        ///     按输入条件移除字符串中的字符
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>A string.</returns>
        public static string RemoveWhere(this string @this, Func<char, bool> predicate)
        {
            return new string(@this.ToCharArray().Where(x => !predicate(x)).ToArray());
        }

        /// <summary>
        ///     移除小写字符
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A string.</returns>
        public static string RemoveLetter(this string @this)
        {
            return new string(@this.ToCharArray().Where(x => !char.IsLetter(x)).ToArray());
        }


        /// <summary>
        ///     A string extension method that return the left part of the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="length">The length.</param>
        /// <returns>The left part.</returns>
        public static string Left(this string @this, int length)
        {
            return @this.Substring(0, length);
        }

        /// <summary>
        ///     从左边获取指定长度的字符串
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="length">The length.</param>
        /// <returns>A string.</returns>
        public static string LeftSafe(this string @this, int length)
        {
            return @this.Substring(0, Math.Min(length, @this.Length));
        }

        /// <summary>
        ///     扩展Trim
        /// </summary>
        /// <param name="s"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Trim(this string s, int start, int length)
        {
            if (s == null) throw new ArgumentNullException();
            if (start < 0) throw new ArgumentOutOfRangeException(nameof(start));
            if (length < 0) throw new ArgumentOutOfRangeException(nameof(length));

            var end = start + length - 1;
            if (end >= s.Length) throw new ArgumentOutOfRangeException(nameof(length));
            for (; start < end; start++)
                if (!char.IsWhiteSpace(s[start]))
                    break;
            for (; end >= start; end--)
                if (!char.IsWhiteSpace(s[end]))
                    break;
            return s.Substring(start, end - start + 1);
        }

        /// <summary>
        ///     去除首尾空格，如果字符为空则返回空字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TrimSafe(this string str)
        {
            var result = string.IsNullOrWhiteSpace(str) ? string.Empty : str.Trim();
            return result;
        }

        /// <summary>
        ///     提取指定字符
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>A string.</returns>
        public static string Extract(this string @this, Func<char, bool> predicate)
        {
            return new string(@this.ToCharArray().Where(predicate).ToArray());
        }

        /// <summary>
        ///     提取小写字符
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The extracted letter.</returns>
        public static string ExtractLetter(this string @this)
        {
            return new string(@this.ToCharArray().Where(char.IsLetter).ToArray());
        }

        /// <summary>
        ///     提取数字
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The extracted number.</returns>
        public static string ExtractNumber(this string @this)
        {
            return new string(@this.ToCharArray().Where(char.IsNumber).ToArray());
        }

        /// <summary>
        ///     串联两个字符串
        /// </summary>
        /// <param name="str0">The first string to concatenate.</param>
        /// <param name="str1">The second string to concatenate.</param>
        /// <returns>The concatenation of  and .</returns>
        public static string ConcatTo(this string str0, string str1)
        {
            return string.Concat(str0, str1);
        }

        /// <summary>
        ///     串联字符串数组
        /// </summary>
        /// <param name="str0">The first string to concatenate.</param>
        /// <param name="strings"></param>
        /// <returns>The concatenation of , , and .</returns>
        public static string ConcatWith(this string str0, string[] strings)
        {
            var result = str0;
            foreach (var concatString in strings) result.ConcatTo(concatString);

            return result;
        }

        /// <summary>
        ///     拷贝字符串
        /// </summary>
        /// <param name="str">The string to copy.</param>
        /// <returns>A new string with the same value as .</returns>
        public static string Copy(this string str)
        {
            return string.Copy(str);
        }


        /// <summary>
        ///     用指定分割付连接若干字符串
        /// </summary>
        /// <param name="separator">
        ///     The string to use as a separator.  is included in the returned string only if  has more
        ///     than one element.
        /// </param>
        /// <param name="value">An array that contains the elements to concatenate.</param>
        /// <returns>
        ///     A string that consists of the elements in  delimited by the  string. If  is an empty array, the method
        ///     returns .
        /// </returns>
        public static string Join(this string separator, string[] value)
        {
            return string.Join(separator, value);
        }


        /// <summary>
        ///     用指定分割付连接若干字符串
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="separator">
        ///     The string to use as a separator.  is included in the returned string only if  has more
        ///     than one element.
        /// </param>
        /// <param name="values">An array that contains the elements to concatenate.</param>
        /// <returns>A String.</returns>
        public static string Join<T>(this string separator, IEnumerable<T> values)
        {
            return string.Join(separator, values);
        }

        /// <summary>
        ///     用指定分割付连接若干字符串
        /// </summary>
        /// <param name="separator">
        ///     The string to use as a separator.  is included in the returned string only if  has more
        ///     than one element.
        /// </param>
        /// <param name="values">An array that contains the elements to concatenate.</param>
        /// <returns>
        ///     A string that consists of the elements in  delimited by the  string. If  is an empty array, the method
        ///     returns .e
        /// </returns>
        public static string Join(this string separator, IEnumerable<string> values)
        {
            return string.Join(separator, values);
        }


        /// <summary>
        ///     删除最后结尾的指定字符后的字符
        /// </summary>
        public static string RemoveLastChar(string str, string strchar)
        {
            return str.Substring(0, str.LastIndexOf(strchar, StringComparison.Ordinal));
        }

        #endregion

        #region 类型转换

        /// <summary>
        ///     转换字符串为byte数组
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a byte[].</returns>
        public static byte[] ToByteArray(this string @this)
        {
            Encoding encoding = Activator.CreateInstance<ASCIIEncoding>();
            return encoding.GetBytes(@this);
        }

        /// <summary>
        ///     转换字符串为文件目录对象
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a DirectoryInfo.</returns>
        public static DirectoryInfo ToDirectoryInfo(this string @this)
        {
            return new DirectoryInfo(@this);
        }

        /// <summary>
        ///     转换字符串为枚举项
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a T.</returns>
        public static T ToEnum<T>(this string @this)
        {
            var enumType = typeof(T);
            return (T)Enum.Parse(enumType, @this);
        }

        /// <summary>
        ///     转换字符串为文件对象
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a FileInfo.</returns>
        public static FileInfo ToFileInfo(this string @this)
        {
            return new FileInfo(@this);
        }

        /// <summary>
        ///     转换字符串为内存流
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="encoding"></param>
        /// <returns>@this as a MemoryStream.</returns>
        public static Stream ToStream(this string @this, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            return new MemoryStream(encoding.GetBytes(@this));
        }

        /// <summary>
        ///     转换字符串为XDocument
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an XDocument.</returns>
        public static XDocument ToXDocument(this string @this)
        {
            Encoding encoding = Activator.CreateInstance<ASCIIEncoding>();
            using (var ms = new MemoryStream(encoding.GetBytes(@this)))
            {
                return XDocument.Load(ms);
            }
        }

        /// <summary>
        ///     转换字符串为XmlDocument
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an XmlDocument.</returns>
        public static XmlDocument ToXmlDocument(this string @this)
        {
            var doc = new XmlDocument();
            doc.LoadXml(@this);
            return doc;
        }

        #endregion
    }
}