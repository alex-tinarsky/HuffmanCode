using System;
using System.Collections.Generic;
using System.Linq;

namespace HuffmanCode
{
    class Node: IComparable<Node>
    {
        public char? Character { get; }
        public int NumberOfChar { get; }
        public Node Left { get; }
        public Node Right { get; }
        public Node(char? c, int num_of_char, Node left, Node right)
        {
            NumberOfChar = num_of_char;
            Left = left;
            Right = right;
            Character = c;
        }

        public int CompareTo(Node other)
        {
            if (other == null) 
                throw new NotImplementedException();

            return NumberOfChar.CompareTo(other.NumberOfChar);
        }
    }
}
