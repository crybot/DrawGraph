using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.VisualBasic;
using System.CodeDom.Compiler;
using System.Text.RegularExpressions;

namespace DrawGraph
{
    class Function
    {
        private string expression;
        private MethodInfo evaluateFunction;

        public Function() { Expression = "0"; }
        public Function(string f) { Expression = f; }

        public string Expression
        {
            get
            {
                return this.expression;
            }

            set
            {
                this.expression = convertExpression(value);
                createEvaluator();
            }
        }
        public float Apply(float x)
        {
            return (float)this.evaluateFunction.Invoke(null, new object[] { x });
        }

        private string convertExpression(string expr)
        {
            string pattern;
            StringBuilder converted = new StringBuilder(expr.Replace(" ", string.Empty));

            for (int i = 0; i < converted.Length; i++)
            {
                if (char.IsNumber(converted[i]) && i < converted.Length - 1)
                {
                    if (!IsNumberOrOperator(converted[i + 1]))
                    {
                        converted.Insert(i + 1, '*');
                        i++;
                    }
                }

                else if (converted[i] == 'x' && i < converted.Length - 1)
                {
                    if (!IsOperator(converted[i + 1]))
                    {
                        converted.Insert(i + 1, '*');
                        i++;
                    }
                }

                else if (converted[i] == ')' && i < converted.Length - 1)
                {
                    if (!IsOperator(converted[i + 1]))
                    {
                        converted.Insert(i + 1, '*');
                        i++;
                    }
                }
            }

            pattern = "sin";
            converted = new StringBuilder(Regex.Replace(converted.ToString(), pattern, "Math.Sin"));
            pattern = "cos";
            converted = new StringBuilder(Regex.Replace(converted.ToString(), pattern, "Math.Cos"));
            pattern = "tan";
            converted = new StringBuilder(Regex.Replace(converted.ToString(), pattern, "Math.Tan"));
            pattern = "abs";
            converted = new StringBuilder(Regex.Replace(converted.ToString(), pattern, "Math.Abs"));
            return converted.ToString();
        }
        private bool IsNumberOrOperator(char c)
        {
            string operators = "+-/*^.,)";
            return (char.IsNumber(c) || operators.Contains(c));
        }
        private bool IsOperator(char c)
        {
            string operators = "+-/*^.,)";
            return (operators.Contains(c));
        }
        private void createEvaluator()
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

            CompilerParameters parameters = new CompilerParameters();
            CodeDomProvider provider = new VBCodeProvider();

            parameters.GenerateExecutable = false;
            parameters.TreatWarningsAsErrors = true;
            parameters.TempFiles.KeepFiles = false;
            parameters.GenerateInMemory = true;

            CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);

            if (results.Errors.Count > 0)
            {
                throw new Exception("Espressione non valida!");
            }
            else
            {
                Assembly asm = results.CompiledAssembly;
                this.evaluateFunction = asm.GetType("Evaluator").GetMethod("Evaluate");
            }
        }
    }
}
