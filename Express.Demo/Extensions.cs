
namespace Express.Demo {

    public static class Extensions {    

		#region global::Express.Demo.Dog

        public static global::Express.Demo.Dog SetColor(
            this global::Express.Demo.Dog @this, global::System.String value) 
        {
            @this.Color = value;
            return @this;
        }

        public static global::Express.Demo.Dog SetLuckyNumber(
            this global::Express.Demo.Dog @this, global::System.Int32 value) 
        {
            @this.LuckyNumber = value;
            return @this;
        }

        public static global::Express.Demo.Dog SetFriends(
            this global::Express.Demo.Dog @this, global::System.Collections.Generic.List<global::Express.Demo.Dog> value) 
        {
            @this.Friends = value;
            return @this;
        }

		#endregion

		#region global::Express.Demo.BlackBox

        public static global::Express.Demo.BlackBox SetStuff(
            this global::Express.Demo.BlackBox @this, global::System.Collections.Generic.Dictionary<global::System.Nullable<global::System.Int32>, global::System.Collections.Generic.IEnumerable<global::System.Threading.Tasks.Task<global::System.Boolean>>> value) 
        {
            @this.Stuff = value;
            return @this;
        }

        public static global::Express.Demo.BlackBox SetPasscode(
            this global::Express.Demo.BlackBox @this, global::System.Int32 value) 
        {
            @this.Passcode = value;
            return @this;
        }

        public static global::Express.Demo.BlackBox SetItem(
            this global::Express.Demo.BlackBox @this, global::System.String name, global::System.Int32 value) 
        {
            @this[name] = value;
            return @this;
        }

        public static global::Express.Demo.BlackBox DoExplodeInTMinus(
            this global::Express.Demo.BlackBox @this, global::System.Int32 minutes, global::System.Single seconds) 
        {
            @this.ExplodeInTMinus(minutes, seconds);
            return @this;
        }

        public static global::Express.Demo.BlackBox DoSomethingCool<T>(
            this global::Express.Demo.BlackBox @this, T value) 
        {
            @this.SomethingCool<T>(value);
            return @this;
        }

		#endregion

		#region global::Express.Demo.Widget<T>

        public static global::Express.Demo.Widget<T> SetValue<T>(
            this global::Express.Demo.Widget<T> @this, T value) 
        {
            @this.Value = value;
            return @this;
        }

        public static global::Express.Demo.Widget<T> DoSomething<T>(
            this global::Express.Demo.Widget<T> @this, T value) 
        {
            @this.Something(value);
            return @this;
        }

        public static global::Express.Demo.Widget<T> DoDoublyGenericMethod<T, U>(
            this global::Express.Demo.Widget<T> @this, global::System.Int32 n) 
        {
            @this.DoublyGenericMethod<U>(n);
            return @this;
        }

		#endregion


    }
}