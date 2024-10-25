using System.Collections.Immutable;

namespace JackHouse
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = ImmutableList.Create<string>();
            
            Part1 part1 = new Part1();
            Part2 part2 = new Part2();
            Part3 part3 = new Part3();
            Part4 part4 = new Part4();
            Part5 part5 = new Part5();
            Part6 part6 = new Part6();
            Part7 part7 = new Part7();
            Part8 part8 = new Part8();
            Part9 part9 = new Part9();

            part1.AddPart(list);
            part2.AddPart(part1.Poem);
            part3.AddPart(part2.Poem);
            part4.AddPart(part3.Poem);
            part5.AddPart(part4.Poem);
            part6.AddPart(part5.Poem);
            part7.AddPart(part6.Poem);
            part8.AddPart(part7.Poem);
            part9.AddPart(part8.Poem);

            Console.WriteLine(part1.Poem.Count);
            Console.WriteLine(part2.Poem.Count);
            Console.WriteLine(part3.Poem.Count);
            Console.WriteLine(part4.Poem.Count);
            Console.WriteLine(part5.Poem.Count);
            Console.WriteLine(part6.Poem.Count);
            Console.WriteLine(part7.Poem.Count);
            Console.WriteLine(part8.Poem.Count);
            Console.WriteLine(part9.Poem.Count);
        }
    }
}