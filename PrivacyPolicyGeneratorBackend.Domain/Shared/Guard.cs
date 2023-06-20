using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivacyPolicyGeneratorBackend.Domain.Shared
{

    public class Guard
    {
        public static void ForLessEqualZero(int value, string parameterName)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        public static void ForLessEqualZero(decimal value, string parameterName)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }
        public static void ForPrecedesDate(DateTime value, DateTime dateToPrecede, string parameterName)
        {
            if (value >= dateToPrecede)
            {
                throw new ArgumentOutOfRangeException(parameterName, $"The new Dead line {dateToPrecede.ToShortDateString()} should be greater than the previous {value.ToShortDateString()}");
            }
        }
        public static void ForNullOrEmpty(string value, string parameterName, string? message = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException(message ?? $"Required value {parameterName} was empty");
            }
        }

        public static void ForNullOrEmpty(Guid value, string parameterName, string? message = null)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException(message ?? $"Required value {parameterName} was empty");
            }
        }

        public static void ForNullOrWhiteSpace(string value, string parameterName, string? message = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                if (string.IsNullOrEmpty(message))
                {
                    throw new ArgumentException($"Required value {parameterName} was empty");
                }
                throw new ArgumentNullException(parameterName, message);
            }
        }

        public static void ForNameLength(string value, int length, string parameterName, string? message = null)
        {
            if (value.Length > length)
            {
                if (string.IsNullOrEmpty(message))
                {
                    throw new ArgumentException($"Value {parameterName} was too long");
                }
                throw new ArgumentNullException(parameterName, message);
            }
        }

        public static DateTime OutOfRange(DateTime value, DateTime rangeFrom, DateTime rangeTo, string parameterName, string? message = null)
        {
            return OutOfRange<DateTime>(value, rangeFrom, rangeTo, parameterName, message);
        }
        public static int OutOfRange(int value, int rangeFrom, int rangeTo, string parameterName, string? message = null)
        {
            return OutOfRange<int>(value, rangeFrom, rangeTo, parameterName, message);
        }
        public static T OutOfRange<T>(T value, T rangeFrom, T rangeTo, string parameterName, string? message = null)
        {
            Comparer<T> comparer = Comparer<T>.Default;
            if (comparer.Compare(rangeFrom, rangeTo) > 0)
            {
                throw new ArgumentException(message ?? $"{nameof(rangeFrom)} should be less than or equal to {nameof(rangeTo)}");
            }
            if (comparer.Compare(value, rangeFrom) < 0 || comparer.Compare(value, rangeTo) > 0)
            {
                if (string.IsNullOrEmpty(message))
                {
                    throw new ArgumentOutOfRangeException(parameterName, $"{parameterName} was out of range");
                }
                throw new ArgumentOutOfRangeException(message, (Exception?)null);
            }
            return value;
        }

    }
}
