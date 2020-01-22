using System;

namespace SubLottery
{
    [Serializable()]
    public class Subscriber
    {
        public string Name { get; set; }
        public int Subs { get; set; }
        public double Chance { get; set; }

        public Subscriber(string name, int subs, double chance)
        {
            Name = name;
            Subs = subs;
            Chance = chance;
        }
    }
}
