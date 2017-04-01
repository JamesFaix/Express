
namespace Express.Demo {

    public static class Extensions {    

		#region Express.Demo.Dog

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

		#region Express.Demo.BlackBox

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

		#endregion


    }
}