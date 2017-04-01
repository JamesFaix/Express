using System.Collections.Generic;
using System.Threading.Tasks;

namespace Express.Demo {

    public class BlackBox {

        //Complex generic property
        public Dictionary<int?, IEnumerable<Task<bool>>> Stuff { get; set; }

        //Write-only property
        private int passcode;
        public int Passcode { set { passcode = value; } }

        //Indexer
        public int this[string name] { get { return 1; } set { } }
    }
}