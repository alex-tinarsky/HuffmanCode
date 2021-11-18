using System;
using System.Collections.Generic;
using System.Linq;
namespace HuffmanCode
{
    class Program
    {
        static List<Node> Input(string Str)
        {
            var str = new string(Str.Where(c =>
            (!char.IsPunctuation(c) && !char.IsWhiteSpace(c) && !char.IsNumber(c))).ToArray()).ToLower();
            Console.WriteLine(str);
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

        static void GetCodeToNode(Node node, string code, Dictionary<char, string> dict)
        {
            if (node.Character != null)
            {
                dict.Add((char)node.Character, code);
                return;
            }

            GetCodeToNode(node.Left, code + "0", dict);
            GetCodeToNode(node.Right, code + "1", dict);
        }

        static Dictionary<char, string> GetCharCodes(Node root)
        {
            Dictionary<char, string> dict = new();
            GetCodeToNode(root, "", dict);
            return dict;
        }

        static void PrintTree(Node node, int depth)
        {
            if (node.Left == null && node.Right == null)
            {
                Console.WriteLine($"{String.Concat(Enumerable.Repeat("   |", depth))}--" +
                    $"{node.Character}({node.NumberOfChar})");
                return;
            }
            Console.WriteLine($"{String.Concat(Enumerable.Repeat("   |", depth))}  " +
                    $"({node.NumberOfChar})");

            PrintTree(node.Left, depth + 1);
            PrintTree(node.Right, depth + 1);
        }

        static void Main(string[] args)
        {
            var Str = "Алексей Докажите, что энтропия монетки принимает" +
                " наибольшее значение для правильной монетки. Тинарский";

            var Str2 = "AHFBHCEHEHCEAHDCEEHHHCHHHDEGHGGEHCHH";

            var list_of_nodes = Input(Str);

            list_of_nodes.Sort();
            foreach (Node c in list_of_nodes)
            {
                Console.WriteLine($"{c.Character}: {c.NumberOfChar}");
            }
            Console.WriteLine("------");

            var tree = CreateTree(list_of_nodes)[0];

            var dict = GetCharCodes(tree);

            var dict_keys = dict.Keys.OrderBy<char, int>(key => dict[key].Length);
            foreach (char key in dict_keys)
            {
                Console.WriteLine($"{key}: {dict[key]}");
            }
            Console.WriteLine();

            PrintTree(tree, 0);
        }
    }
}
