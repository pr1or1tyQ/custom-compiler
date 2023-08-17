using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CS426.analysis
{
    public abstract class Definition
    {
        public string name;

        public string toString()
        {
            return name;
        }
    }

    public abstract class TypeDefinition : Definition { }

    public class NumberDefinition : TypeDefinition { } // float number definition includes exponential notation

    public class StringDefinition : TypeDefinition { }

    public class BooleanDefinition : TypeDefinition { }

    public class VariableDefinition : Definition {
        public TypeDefinition variableType;
    }

    public class ConstantDefinition : Definition
    {
        public TypeDefinition variableType;
        //also has assigned value
        //*****do I need to check for constatn key word?
    }

    public class FunctionDefinition : Definition {
        public List<VariableDefinition> parameters;
    }


}
