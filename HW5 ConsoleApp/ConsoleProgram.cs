// Cammi Smith
// 11366085
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CptS322;

namespace HW5_ConsoleApp
{
    class ConsoleProgram
    {
        static void Main(string[] args)
        {
            ExpTree tree = new ExpTree();

            while (true)
            {
                Console.WriteLine("Menu (current expression = \"" + tree.expression + "\")");
                Console.WriteLine("  1 = Enter a new expression");
                Console.WriteLine("  2 = Set a variable value");
                Console.WriteLine("  3 = Evaluate Tree");
                Console.WriteLine("  4 = Quit");
                string choice = Console.ReadLine();

                if (choice.Length != 1)
                    continue;

                switch(Convert.ToInt32(choice))
                {
                    case 1:
                        Console.WriteLine("Enter new expression: ");
                        string exp = Console.ReadLine();
                        tree = new ExpTree(exp);
                        continue;
                    case 2:
                        Console.WriteLine("Enter variable name: ");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter variable value: ");
                        string value = Console.ReadLine();
                        tree.SetVar(name, Convert.ToDouble(value));
                        continue;
                    case 3:
                        Console.WriteLine(tree.Eval());
                        continue;
                    case 4:
                        Console.WriteLine("Done");
                        goto exit;
                    default:
                        goto continueLoop;
                }

                exit:
                    break;

                continueLoop:
                    continue;
            }
        }
    }
}
