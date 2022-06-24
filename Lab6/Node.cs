namespace Lab6
{
    public class Node
    {
        public Node LeftNode { get; set; }
        public Node RightNode { get; set; }
        public Rectangle Value { get; }

        public Node(Rectangle value)
        {
            Value = value;
        }
    }
}