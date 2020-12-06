using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode1
{
    class Program
    {
        public static List<int> Input = new List<int> {
            1228,
            1584,
        };

        static void Main(string[] args)
        {
            var inputsChecked = new List<int>();

            foreach (var num1 in Input)
            {
                var secondInputsChecked = new List<int>();
                if (inputsChecked.Contains(num1))
                {
                    continue;
                }

                var hasDuplicates = Input.Count(i => i == num1) > 1;
                foreach (var num2 in Input.Where(i => hasDuplicates || i != num1))
                {
                    var thirdInputsChecked = new List<int>();

                    if (secondInputsChecked.Contains(num2))
                    {
                        continue;
                    }

                    var hasSecondDuplicates = Input.Count(i => i == num2) > 1;
                    foreach(var num3 in Input.Where(i => (hasDuplicates || i != num1) && (hasSecondDuplicates || i != num2)))
                    {
                        if (thirdInputsChecked.Contains(num3))
                        {
                            continue;
                        }

                        if (num1 + num2 + num3 == 2020)
                        {
                            Console.WriteLine($"Answer: {num1 * num2 * num3}");
                            break;
                        }

                        thirdInputsChecked.Add(num3);
                    }

                    secondInputsChecked.Add(num2);
                }

                inputsChecked.Add(num1);
            }
        }
    }
}
