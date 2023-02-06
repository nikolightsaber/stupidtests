using System;

namespace cs_tests
{
    class GrandParent
    {
        public virtual void toz()
        {
            Console.WriteLine("Grandparent");
        }
    }

    class Parent: GrandParent
    {
    }


    class Child: Parent
    {
        public override void toz()
        {
            Console.WriteLine("Child");
            base.toz();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Child child = new Child();
            child.toz();
        }
    }
}
