using System.Collections;
using System.Collections.Generic;

namespace Domain.Utils
{
    public class Result
    {
        public IEnumerable<string> Messages { get; private set; }
        public bool Error { get; private set; }
        public bool Success { get; private set; }
        public void AddError(string message)
        {
            if (this.Messages == null)
                this.Messages = new List<string>();

            (this.Messages as IList).Add(message);

            this.Error = true;
        }
    }

    public sealed class Result<T> : Result
    {
        public T Content { get; set; }
    }
}