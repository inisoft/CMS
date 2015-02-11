using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inisoft.Core
{
    /// <summary>
    /// bazowa klasa pozwalająca na zwracanie wyników działania metod
    /// </summary>
    public class MethodResult
    {
        public static readonly MethodResult TRUE = new MethodResult() { Success = true };
        public static readonly MethodResult FALSE = new MethodResult() { Success = false };

        public MethodResult()
        {
            Success = true;
            ChildResults = new List<MethodResult>();
        }

        public MethodResult(string message)
            : this()
        {
            this.Message = message;
        }

        public MethodResult(System.Exception exception)
            : this()
        {
            this.Exception = exception;
            this.Success = false;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public System.Exception Exception { get; set; }
        public IList<MethodResult> ChildResults { get; private set; }

        /// <summary>
        /// Metoda zwraca sformatowany komunikat zawierający wszystkie możliwe dane wraz z danymi wszystkich dzieci
        /// </summary>
        /// <returns></returns>
        public string GetMessage()
        {
            StringBuilder concatenatedMessage = new StringBuilder();
            if (!string.IsNullOrEmpty(Message))
            {
                concatenatedMessage.AppendLine(Message);
            }
            if (Exception != null)
            {
                concatenatedMessage.AppendLine(Exception.Message);
                concatenatedMessage.AppendLine(Exception.Source);
                if (Exception.InnerException != null)
                {
                    concatenatedMessage.AppendLine(Exception.InnerException.Message);
                }
            }
            foreach (MethodResult loopMethodResult in ChildResults)
            {
                concatenatedMessage.AppendLine(System.Environment.NewLine);
                concatenatedMessage.Append(loopMethodResult.GetMessage());
            }
            return concatenatedMessage.ToString();
        }

        // Explicit conversion from DBBool to bool. Throws an 
        // exception if the given DBBool is dbNull, otherwise returns
        // true or false:
        public static explicit operator bool(MethodResult methodResult)
        {
            return methodResult.Success;
        }

        // Definitely true operator. Returns true if the operand is 
        // dbTrue, false otherwise:
        public static bool operator true(MethodResult methodResult)
        {
            return methodResult.Success;
        }

        // Definitely false operator. Returns true if the operand is 
        // dbFalse, false otherwise:
        public static bool operator false(MethodResult methodResult)
        {
            return !methodResult.Success;
        }

        // Logical AND operator. Returns dbFalse if either operand is 
        // dbFalse, dbNull if either operand is dbNull, otherwise dbTrue:
        public static MethodResult operator &(MethodResult x, MethodResult y)
        {
            MethodResult result = new MethodResult();
            result.ChildResults.Add(x);
            result.ChildResults.Add(y);
            result.Success = x.Success && y.Success;
            result.Exception = x.Exception != null ? x.Exception : y.Exception;
            result.Message = !string.IsNullOrEmpty(x.Message) ? x.Message : y.Message;
            return result;
        }

        // Logical OR operator. Returns dbTrue if either operand is 
        // dbTrue, dbNull if either operand is dbNull, otherwise dbFalse:
        public static MethodResult operator |(MethodResult x, MethodResult y)
        {
            MethodResult result = new MethodResult();
            result.ChildResults.Add(x);
            result.ChildResults.Add(y);
            result.Success = x.Success || y.Success;
            result.Exception = x.Exception != null ? x.Exception : y.Exception;
            result.Message = !string.IsNullOrEmpty(x.Message) ? x.Message : y.Message;
            return result;
        }
    }

    /// <summary>
    /// Bazowy obiekt wyniku działania metody która zwraca dane
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MethodResult<T> : MethodResult
    {
        public MethodResult(): base()
        {
        }

        public MethodResult(string message)
            : base(message)
        {
        }

        public MethodResult(System.Exception exception)
            : base(exception)
        {
        }

        public T Data { get; set; }
    }
}