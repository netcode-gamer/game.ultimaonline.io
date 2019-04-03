using System;
using System.Collections.Generic;

namespace UltimaOnline
{
    [Parsable]
    public abstract class Poison
    {
        /*public abstract TimeSpan Interval{ get; }
        public abstract TimeSpan Duration{ get; }*/
        public abstract string Name { get; }
        public abstract int Level { get; }
        public abstract Timer ConstructTimer(Mobile m);
        /*public abstract void OnDamage( Mobile m, ref object state );*/

        public override string ToString() => Name;

        public static void Register(Poison reg)
        {
            var regName = reg.Name.ToLower();
            for (var i = 0; i < Poisons.Count; i++)
                if (reg.Level == Poisons[i].Level) throw new Exception("A poison with that level already exists.");
                else if (regName == Poisons[i].Name.ToLower()) throw new Exception("A poison with that name already exists.");
            Poisons.Add(reg);
        }

        public static Poison Lesser => GetPoison("Lesser");
        public static Poison Regular => GetPoison("Regular");
        public static Poison Greater => GetPoison("Greater");
        public static Poison Deadly => GetPoison("Deadly");
        public static Poison Lethal => GetPoison("Lethal");

        public static List<Poison> Poisons { get; } = new List<Poison>();

        public static Poison Parse(string value)
        {
            Poison p = null;
            if (int.TryParse(value, out int plevel))
                p = GetPoison(plevel);
            if (p == null)
                p = GetPoison(value);
            return p;
        }

        public static Poison GetPoison(int level)
        {
            for (var i = 0; i < Poisons.Count; ++i)
            {
                var p = Poisons[i];
                if (p.Level == level)
                    return p;
            }
            return null;
        }

        public static Poison GetPoison(string name)
        {
            for (var i = 0; i < Poisons.Count; ++i)
            {
                var p = Poisons[i];
                if (Utility.InsensitiveCompare(p.Name, name) == 0)
                    return p;
            }
            return null;
        }

        public static void Serialize(Poison p, GenericWriter w)
        {
            if (p == null) w.Write((byte)0);
            else { w.Write((byte)1); w.Write((byte)p.Level); }
        }

        public static Poison Deserialize(GenericReader r)
        {
            switch (r.ReadByte())
            {
                case 1: return GetPoison(r.ReadByte());
                case 2:
                    //no longer used, safe to remove?
                    r.ReadInt();
                    r.ReadDouble();
                    r.ReadInt();
                    r.ReadTimeSpan();
                    break;
            }
            return null;
        }
    }
}
