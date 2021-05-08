using System;

namespace Musix.Core.Attributes
{
    public sealed class Weight : Attribute
    {
        public readonly int ClassWeight;

        public Weight(int weight)
        {
            ClassWeight = weight;
        }

        public static int GetWeight(Type t)
        {
            if (IsDefined(t, typeof(Weight)))
            {
                if (GetCustomAttribute(t, typeof(Weight)) is Weight w) 
                    return w.ClassWeight;
            }
            return 50;
        }
    }
}