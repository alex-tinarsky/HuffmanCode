using System;
using System.Collections.Generic;
using System.Linq;
namespace HuffmanCode
{
    class Program
    {
        static List<Node> Input()
        {
            var Str = "Алексей Докажите, что энтропия монетки принимает" +
                " наибольшее значение для правильной монетки. Тинарский";
            var str = new string(Str.Where(c =>
            (!char.IsPunctuation(c) && !char.IsWhiteSpace(c) && !char.IsNumber(c))).ToArray()).ToLower();
            List<Node> list_of_nodes = new();
            HashSet<char> chars = new(str);
            foreach (char ch in chars)
            {
                list_of_nodes.Add(new Node(ch, str.Count(c => c == ch), null, null));
            }
            return list_of_nodes;
        }

        static List<Node> CreateTree(List<Node> list_of_nodes)
        {
            if (list_of_nodes.Count == 1)
                return list_of_nodes;

            Node first_node = list_of_nodes.Min<Node>();
            list_of_nodes.Remove(first_node);

            Node second_node = list_of_nodes.Min<Node>();
            list_of_nodes.Remove(second_node);

            list_of_nodes.Add(new(null, first_node.NumberOfChar + second_node.NumberOfChar,
                first_node, second_node));
            return CreateTree(list_of_nodes);
        }

        static void Main(string[] args)
        {
            var list_of_nodes = Input();
            foreach (Node c in list_of_nodes)
            {
                Console.WriteLine($"{c.Character}: {c.NumberOfChar}");
            }

            var tree = CreateTree(Input());
            foreach (Node c in tree)
            {
                Console.WriteLine($"{c.Character}: {c.NumberOfChar}");
            }
        }
    }
}
