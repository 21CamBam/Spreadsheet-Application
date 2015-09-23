// Cammi Smith
// 11366085
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CptS322
{
    public class ExpTree
    {
        public string expression;
        private Node root;
        public ExpTree() // Implement this constructor to construct the tree from the default expression
        {
            root = null;
            expression = "A1-12-C1";
            BuildTree();
        }

        public ExpTree(string exp) // Implement this constructor to construct the tree from the specific expression
        {
            root = null;
            expression = exp;
            BuildTree();
        }

        public void SetVar(string varName, double varValue)
        {
            Search(root, varName, varValue);
        }

        public double Eval()
        {
            return rEval(root);
        }

        internal double rEval(Node node)
        {
            if ((node.GetType().Equals(typeof(VariableNode))))
            {
                return (node as VariableNode).Value;
            }
            else if ((node.GetType().Equals(typeof(NumNode))))
            {
                return (node as NumNode).Value;
            }
            else if ((node.GetType().Equals(typeof(OperatorNode))))
            {
                if ((node as OperatorNode).value == "+")
                    return rEval((node as OperatorNode).left) + rEval((node as OperatorNode).right);
                else if ((node as OperatorNode).value == "-")
                    return rEval((node as OperatorNode).left) - rEval((node as OperatorNode).right);
                else if ((node as OperatorNode).value == "/")
                    return rEval((node as OperatorNode).left) / rEval((node as OperatorNode).right);
                else if ((node as OperatorNode).value == "*")
                    return rEval((node as OperatorNode).left) * rEval((node as OperatorNode).right);
                else
                    return 0;
            }
            else
                return 0;
        }

        private void BuildTree()
        {
            string[] infix = Regex.Split(expression, @"([a-zA-Z][a-zA-Z0-9_]*)|([0-9]+)|([+-/*])|[^\s*]$"); // split expression into parts
            List<string> postfix = postfixer(infix);
            Stack<Node> nodeStack = new Stack<Node>();

            for (int i = 0; i < postfix.Count(); i++)
            {
                double value;
                if (Double.TryParse(postfix[i], out value)) // if is double
                {
                    NumNode newNode = new NumNode(postfix[i]);
                    nodeStack.Push(newNode);
                }
                else if (is_operator(postfix[i])) // Operator
                {
                    OperatorNode newNode = new OperatorNode(postfix[i]);
                    newNode.right = nodeStack.Pop();
                    newNode.left = nodeStack.Pop();
                    nodeStack.Push(newNode);
                }
                else // Variable
                {
                    VariableNode newNode = new VariableNode(postfix[i]);
                    nodeStack.Push(newNode);
                }
            }
            root = nodeStack.Pop();
        }

        private List<string> postfixer(string[] infix)
        {
            Stack<string> expStack = new Stack<string>();
            List<string> exp = new List<string>();


            for (int i = 0; i < infix.Length; i++)
            {
                if (infix[i] == "") // Gets rid of empty strings
                {
                    continue;
                }
                else if (is_operator(infix[i])) //Operator
                {
                    if (expStack.Count == 0)
                    {
                        expStack.Push(infix[i]);
                        continue;
                    }
                    else
                    {
                        exp.Add(expStack.Pop());
                        expStack.Push(infix[i]);
                        continue;
                    }
                }
                else // Variable
                {
                    exp.Add(infix[i]);
                    continue;
                }
            }
            exp.Add(expStack.Pop());
            return exp;
        }

        private bool is_operator(string value)
        {
            if (value.Length == 1 && (value[0] == '+' || value[0] == '-' || value[0] == '/' || value[0] == '*'))
                return true;
            return false;
        }

        private bool Search(Node next, string varName, double varValue)
        {
            if ((next.GetType().Equals(typeof(VariableNode))) && (next as VariableNode).value == varName)
            {
                (next as VariableNode).Value = varValue;
                return true;
            }
            else if ((next.GetType().Equals(typeof(OperatorNode))))
            {
                Search((next as OperatorNode).left, varName, varValue);
                Search((next as OperatorNode).right, varName, varValue);
            }
            else
            {
                return false;
            }
            return false;
        }
    }

    internal class Node
    {
        internal string value;

        internal Node(string data)
        {
            value = data;
        }
    }

    internal class OperatorNode : Node
    {
        internal Node left;
        internal Node right;

        internal OperatorNode(string data) 
            : base(data) 
        {
            left = null;
            right = null;
        }
    }

    internal class VariableNode : Node
    {
        double varValue;

        internal VariableNode(string data) 
            : base(data) 
        {
            varValue = 0;
        }
        internal double Value
        {
            get { return varValue; }
            set { varValue = value; }
        }
    }

    internal class NumNode : Node
    {
        double varValue;

        internal NumNode(string data)
            : base(data)
        {
            varValue = Convert.ToDouble(data);
        }
        internal double Value
        {
            get { return varValue; }
            set { varValue = value; }
        }
    }
}
