using System.Collections.Generic;

namespace Express.Demo
{
    public class Widget<T>
    {
        public T Value { get; set; }

        public void Something(T value)
        {

        }

        public void DoublyGenericMethod<U>(int n) {

        }
    }
}
