using System.Collections.Generic;

namespace Express.Demo {

    public class Dog {

        public string Color { get; set; }

        public string Name { get; private set; }

        public int LuckyNumber { get; set; }

        public List<Dog> Friends { get; set; }
    }
}
