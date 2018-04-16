using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.CodeAnalysis.Emit;
using System.Reflection;

namespace Lab_4
{
    class Calculator
    {

        delegate double ExecMethod(double x, double y);

        private ExecMethod delegat;

        private SyntaxTree tree;

        public Calculator(string exp)
        {
            this.tree = CSharpSyntaxTree.ParseText(@"using System;

namespace Lab_4
{
    class First
    {
        public double exec(double x, double y)
        {
            return " + exp + @";
        }
    }
}
");

            var compilation = CSharpCompilation.Create("first", 
                new[] { tree }, 
                new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) },
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            var stream = new MemoryStream();
            EmitResult result = compilation.Emit(stream);

            if (!result.Success)
            {
                foreach (Diagnostic item in result.Diagnostics)
                {
                    Console.Error.WriteLine($"{item.Id}: {item.GetMessage()}");
                }
                return;
            }

            stream.Seek(0, SeekOrigin.Begin);
            Assembly assembly = Assembly.Load(stream.ToArray());
            Type type = assembly.GetType("Lab_4.First");
            ConstructorInfo constructorInfo = type.GetConstructors().First();
            var instance = constructorInfo.Invoke(null);
            MethodInfo method = type.GetMethod("exec");
            delegat = (ExecMethod)method.CreateDelegate(typeof(ExecMethod), instance);
        }

        public double Calc(double x, double y)
        {
            return delegat(x, y);
        }
    }
}
