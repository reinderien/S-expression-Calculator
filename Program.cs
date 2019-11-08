using System;

namespace S_expression_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                string inputExpression = Console.ReadLine();

                if (inputExpression.Length == 0)
                {
                    ShowErrorMessage("No expression entered. Please try again!");
                }
                else
                {
                    if (inputExpression.StartsWith("\"") && !inputExpression.EndsWith("\""))
                    {
                        ShowErrorMessage("Can't evaluate expression. Please try again!");
                        continue;
                    }

                    if (!inputExpression.StartsWith("\"") && inputExpression.EndsWith("\""))
                    {
                        ShowErrorMessage("Can't evaluate expression. Please try again!");
                    }

                    //for string-expressions
                    if (inputExpression.IndexOf("(") == 1 && inputExpression.LastIndexOf(")") == (inputExpression.Length - 2))
                    {
                        for (int i = 0; i <= inputExpression.Length - 1; i++)
                        {
                            int startIndex = inputExpression.LastIndexOf("(");
                            int endIndex = inputExpression.IndexOf(")");
                            int length = endIndex - startIndex;

                            if (length > 0)
                            {
                                string functionCall = inputExpression.Substring(startIndex + 1, length - 1);
                                string[] functionNameAndArguments = functionCall.Split(' ');

                                if (functionNameAndArguments.Length >= 3)
                                {
                                    bool isValidFunctionName = IsValidFunctionName(functionNameAndArguments[0].ToLower());
                                    bool isFirstArgumentNumeric = int.TryParse(functionNameAndArguments[1], out int num1);
                                    bool isSecondArgumentNumeric = int.TryParse(functionNameAndArguments[2], out int num2);

                                    if (isValidFunctionName && isFirstArgumentNumeric && isSecondArgumentNumeric)
                                    {
                                        int integerResult = InvokeFunctionCall(functionNameAndArguments[0].ToLower(), num1, num2);
                                        inputExpression = inputExpression.Replace("(" + functionCall + ")", integerResult.ToString());
                                        continue;
                                    }
                                    else
                                    {
                                        ShowErrorMessage("Can't evaluate expression. Please try again!");
                                        break;
                                    }
                                }
                                else
                                {
                                    ShowErrorMessage("Can't evaluate expression. Please try again!");
                                    break;
                                }
                            }

                            inputExpression = inputExpression.Replace("\"", "");
                            Console.WriteLine(inputExpression + "\n");
                            break;
                        }
                    }
                    else if (!inputExpression.Contains("(") && !inputExpression.Contains(")"))
                    {
                        //for integers
                        inputExpression = inputExpression.Replace("\"", "");
                        bool isInteger = int.TryParse(inputExpression, out int integerOutput);

                        if (isInteger)
                        {
                            Console.WriteLine(integerOutput + "\n");
                        }
                        else
                        {
                            ShowErrorMessage("Can't evaluate integer. Please try again!");
                        }
                    }
                }
            };
        }

        #region Helpers
        /// <summary>
        /// Add two numbers together.
        /// </summary>
        /// <param name="num1">The first number.</param>
        /// <param name="num2">The second number.</param>
        /// <returns>The sum of the two numbers.</returns>
        public static int Add(int num1, int num2)
        {
            return num1 + num2;
        }

        /// <summary>
        /// Multiply two numbers together. 
        /// </summary>
        /// <param name="num1">The first number.</param>
        /// <param name="num2">The second number.</param>
        /// <returns>The product of the two numbers.</returns>
        public static int Multiply(int num1, int num2)
        {
            return num1 * num2;
        }

        /// <summary>
        /// Show custom error message
        /// </summary>
        public static void ShowErrorMessage(string errorMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorMessage + "\n");
        }

        /// <summary>
        /// Validates function name
        /// </summary>
        /// <param name="function"></param>
        /// <returns>True if it's a valid function; otherwise false</returns>
        public static bool IsValidFunctionName(string functionName)
        {
            bool isValidFunction;

            switch (functionName)
            {
                case "add":
                    isValidFunction = true;
                    break;
                case "multiply":
                    isValidFunction = true;
                    break;
                default:
                    isValidFunction = false;
                    break;
            }

            return isValidFunction;
        }

        /// <summary>
        /// Invokes a function call.
        /// </summary>
        /// <param name="function">The name of the function.</param>
        /// <param name="num1">The first argument.</param>
        /// <param name="num2">The second argument.</param>
        /// <returns>An integer value.</returns>
        public static int InvokeFunctionCall(string functionName, int num1, int num2)
        {
            int result = 0;
            switch (functionName)
            {
                case "add":
                    result = Add(num1, num2);
                    break;
                case "multiply":
                    result = Multiply(num1, num2);
                    break;
                default:
                    break;
            }

            return result;
        }
        #endregion
    }
}
