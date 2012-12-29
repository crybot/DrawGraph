using System;
using System.CodeDom.Compiler;
using System.Linq;
using System.Reflection;
using Microsoft.VisualBasic;

namespace DrawGraph
{
    internal class Function
    {
        private MethodInfo _evaluateFunction;
        private string _expression;

        public Function(string f)
        {
            Expression = f;
        }

        public Function()
            : this("0")
        {
        }

        public string Expression
        {
            get
            { 
                return _expression; 
            }

            set
            {
                _expression = ConvertExpression(value);
                CreateEvaluator();
            }
        }

        public float Apply(float x)
        {
            return (float) _evaluateFunction.Invoke(null, new object[] {x});
        }

        private string ConvertExpression(string expr)
        {
            string converted = expr.Replace(" ", string.Empty);

            for (int i = 0; i < converted.Length; i++)
            {
                if (char.IsNumber(converted[i]) && i < converted.Length - 1)
                {
                    if (!IsNumberOrOperator(converted[i + 1]))
                    {
                        converted.Insert(i + 1, "*");
                        i++;
                    }
                }

                else if (converted[i] == 'x' && i < converted.Length - 1)
                {
                    if (!IsOperator(converted[i + 1]))
                    {
                        converted.Insert(i + 1, "*");
                        i++;
                    }
                }

                else if (converted[i] == ')' && i < converted.Length - 1)
                {
                    if (!IsOperator(converted[i + 1]))
                    {
                        converted.Insert(i + 1, "*");
                        i++;
                    }
                }
            }

            converted = converted.Replace("sin", "Math.Sin")
                                 .Replace("cos", "Math.Cos")
                                 .Replace("tan", "Math.Tan")
                                 .Replace("abs", "Math.Abs")
                                 .Replace("log", "Math.Log")
                                 .Replace("sqrt", "Math.Sqrt");

            return converted;
        }

        private bool IsNumberOrOperator(char c)
        {
            const string operators = "+-/*^.,)";
            return (char.IsNumber(c) || operators.Contains(c));
        }

        private bool IsOperator(char c)
        {
            const string operators = "+-/*^.,)";
            return (operators.Contains(c));
        }

        private void CreateEvaluator()
        {
            // il codice compilato e` in vb.net poiche` semplifica il parsing dell'espressione
            string code = string.Format(
                @"Imports System.Collections.Generic
                 Imports System.Text
                 Imports System
                 Imports Microsoft.VisualBasic
                 Class Evaluator
                     Const e as Double = math.E
	                  Public Shared Function Evaluate(x As Single) As Single
		                   Return {0}
	                  End Function
                 End Class", Expression);

            var parameters = new CompilerParameters();
            var provider = new VBCodeProvider();

            parameters.GenerateExecutable = false;
            parameters.TreatWarningsAsErrors = true;
            parameters.TempFiles.KeepFiles = false;
            parameters.GenerateInMemory = true;

            var results = provider.CompileAssemblyFromSource(parameters, code);

            if (results.Errors.Count > 0)
                throw new Exception("Espressione non valida!");

            var asm = results.CompiledAssembly;
            _evaluateFunction = asm.GetType("Evaluator").GetMethod("Evaluate");
        }
    }
}