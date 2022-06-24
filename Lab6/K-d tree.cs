using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Lab6
{
    // float latitude, float longtitude, float radius
    public class K_d_tree
    {
        public Node Root { get; set; }
        // public List<Point> Find(); // todo
        public K_d_tree(List<Point> allPoints)
        {
            Rectangle rootRectangle = new Rectangle(allPoints);
            Root = new Node(rootRectangle);
        }
    }
}
















/*
   
        public bool Add(float value)
        {
            Node previous = null;
            Node next = Root;

            while (next != null)
            {
                previous = next;
                if (value < next.Value)
                {
                    next = next.LeftNode;
                }
                else if (value > next.Value)
                {
                    next = next.RightNode;
                }
                else
                {
                    return false;
                }

            }

            Node newNode = new Node{Value = value};

            if (Root == null)
            {
                Root = newNode;
            }
            else
            {
                if (value < previous.Value)
                {
                    previous.LeftNode = newNode;
                }
                else
                {
                    previous.RightNode = newNode;
                }
            }
            
            return true;
        }
        
        

        public void Remove(float value)
        {
            Root = RemoveNode(Root, value); 
        }

        private Node RemoveNode(Node parent, float value)
        {
            if (parent == null)
            {
                return null;
            }

            if (value < parent.Value)
            {
                parent.LeftNode = RemoveNode(parent.LeftNode, value);
            }
            else if (value > parent.Value)
            {
                parent.RightNode = RemoveNode(parent.RightNode, value);
            }
            else
            {
                if (parent.LeftNode == null)
                {
                    return parent.RightNode;
                }
                if (parent.RightNode == null)
                {
                    return parent.RightNode;
                }
                parent.Value = float.Parse(MinValue(parent.RightNode).ToString(), CultureInfo.InvariantCulture);
                parent.RightNode = RemoveNode(parent.RightNode, parent.Value);
            }

            return parent;
        }
        
        public int GetDepth()
        {
            return GetTreeDepth(this.Root);
        }
        private int GetTreeDepth(Node parent)
        {
            return parent == null ? 0 : Math.Max(GetTreeDepth(parent.LeftNode), GetTreeDepth(parent.RightNode)) + 1;
        }
*/