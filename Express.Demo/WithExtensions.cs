
namespace Express.Demo {

    public static class WithExtensions {    

		#region WithIt.Demo.Dog

        public static global::Express.Demo.Dog WithColor(
            this global::Express.Demo.Dog @this, global::System.String value) 
        {
            @this.Color = value;
            return @this;
        }

        public static global::Express.Demo.Dog WithLuckyNumber(
            this global::Express.Demo.Dog @this, global::System.Int32 value) 
        {
            @this.LuckyNumber = value;
            return @this;
        }

        public static global::Express.Demo.Dog WithFriends(
            this global::Express.Demo.Dog @this, global::System.Collections.Generic.List<global::Express.Demo.Dog> value) 
        {
            @this.Friends = value;
            return @this;
        }

		#endregion

		#region WithIt.Demo.BlackBox

        public static global::Express.Demo.BlackBox WithStuff(
            this global::Express.Demo.BlackBox @this, global::System.Collections.Generic.Dictionary<global::System.Nullable<global::System.Int32>, global::System.Collections.Generic.IEnumerable<global::System.Threading.Tasks.Task<global::System.Boolean>>> value) 
        {
            @this.Stuff = value;
            return @this;
        }

        public static global::Express.Demo.BlackBox WithPasscode(
            this global::Express.Demo.BlackBox @this, global::System.Int32 value) 
        {
            @this.Passcode = value;
            return @this;
        }

		#endregion


    }
}