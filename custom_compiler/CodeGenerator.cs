using CS426.analysis;
using CS426.node;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CS436.analysis
{
    internal class CodeGenerator : DepthFirstAdapter
    {
        StreamWriter _output;

        int equal_label = 0;
        int not_equal_label = 0;
        int less_label = 0;
        int less_eq_label = 0;
        int greater_label = 0;
        int greater_eq_label = 0;

        int if_label_1 = 0;
        int if_label_2 = 0;
        int if_label_3 = 0;

        int while_label_1 = 0;
        int while_label_2 = 0;

        public CodeGenerator (String outputFilename)
        {
            _output = new StreamWriter (outputFilename);
        }

        private void Write(String textToWrite)
        {
            Console.Write(textToWrite);
            _output.Write(textToWrite);
        }
        private void WriteLine(String textToWrite)
        {
            Console.WriteLine(textToWrite);
            _output.WriteLine(textToWrite);
        }

        public override void InAProgram(AProgram node)
        {
            WriteLine(".assembly extern mscorlib {}");
            WriteLine(".assembly myProgram");
            WriteLine("{\n\t.ver 1:0:1:0\n}\n");
        }
        public override void OutAProgram(AProgram node)
        {
            _output.Close();

            Console.WriteLine("\n\n");
        }

        //----------------------------------------------------------------------------------------------
        public override void InAMainFunction(AMainFunction node)
        {
            WriteLine(".method static void main() cil managed");
            WriteLine("{\n\t.maxstack 128\n\t.entrypoint\n");
            
        }

        public override void InANoparamsFuncDef(ANoparamsFuncDef node)
        {
            WriteLine(".method static void " + node.GetId().Text + "() cil managed");
            WriteLine("{\n\t.maxstack 128\n");
        }

        public override void InAParamsFuncDef(AParamsFuncDef node)
        {
            WriteLine(".method static void " + node.GetId().Text + "() cil managed");
            WriteLine("{\n\t.maxstack 128\n");
        }

        public override void OutAMainFunction(AMainFunction node)
        {
            WriteLine("\n\tret\n}");
        }

        public override void OutANoparamsFuncDef(ANoparamsFuncDef node)
        {
            WriteLine("\n\tret\n}");
        }

        public override void OutAParamsFuncDef(AParamsFuncDef node)
        {
            WriteLine("\n\tret\n}");
        }

        //----------------------------------------------------------------------------------------------
        public override void OutAVariableDeclareStatement(AVariableDeclareStatement node)
        {
            WriteLine("\t// Declaring Variable " + node.GetVarname().ToString());
            Write("\t.locals init (");

            if(node.GetType().Text == "int") //int
            {
                Write("int32 ");
            }
            else if(node.GetType().Text == "float") //float
            {
                Write("float32 ");
            }
            else if(node.GetType().Text == "string ") //string
            {
                Write("string");
            }
            else
            {
                Write(node.GetType().Text + " ");
            }

            Write(node.GetVarname().Text + ")\n\n");
        }

        public override void OutAIntOperand(AIntOperand node)
        {
            //WriteLine("\t// Writing Integer onto Stack");
            WriteLine("\tldc.i4 " + node.GetInteger().Text);
        }

        public override void OutAFloatOperand(AFloatOperand node)
        {
            //WriteLine("\t// Writing Float onto Stack");
            WriteLine("\tldc.r8 " + node.GetFloat().Text);
        }

        public override void OutAFloatExpOperand(AFloatExpOperand node)
        {
            //WriteLine("\t// Writing Float onto Stack");
            WriteLine("\tldc.r8 " + node.GetFloatExp().Text);
        }

        public override void OutAStringOperand(AStringOperand node)
        {
            //WriteLine("\t// Writing String onto Stack");
            WriteLine("\tldstr " + node.GetString().Text);
        }

        public override void OutAVariableOperand(AVariableOperand node)
        {
            //WriteLine("\t// Writing Variable onto Stack");
            WriteLine("\tldloc " + node.GetId().Text);
        }
        //----------------------------------------------------------------------------------------------

        public override void OutAVariableAssignStatement(AVariableAssignStatement node)
        {
            WriteLine("\tstloc " + node.GetId().Text + "\n");
        }

        public override void OutAAddExpression5(AAddExpression5 node)
        {
            WriteLine("\tadd");
        }

        public override void OutASubExpression5(ASubExpression5 node)
        {
            WriteLine("\tsub");
        }

        public override void OutAMultiplyExpression6(AMultiplyExpression6 node)
        {
            WriteLine("\tmul");
        }

        public override void OutADivideExpression6(ADivideExpression6 node)
        {
            WriteLine("\tdiv");
        }

        public override void OutANegativeExpression7(ANegativeExpression7 node)
        {
            WriteLine("\tneg");
        }
        //----------------------------------------------------------------------------------------------

        public override void OutAParamCall(AParamCall node) //****don't need to be able to call params for PEX4
        {
            if(node.GetId().Text == "printInt")
            {
                WriteLine("\tcall void [mscorlib]System.Console::Write(int32)");
            }
            else if (node.GetId().Text == "printFloat")
            {
                WriteLine("\tcall void [mscorlib]System.Console::Write(float32)");
            }

            else if (node.GetId().Text == "printString")
            {
                WriteLine("\tcall void [mscorlib]System.Console::Write(string)");
            }else if (node.GetId().Text == "printLine")
            {
                WriteLine("\tldstr \"\\n\"");
                WriteLine("\tcall void [mscorlib]System.Console::Write(string)");
            }
            else
            {
                WriteLine("\t call void " + node.GetId().Text + "()");
            }
        }


        public override void OutANoParams(ANoParams node)
        {
            if (node.GetId().Text == "printLine")
            {
                WriteLine("\tldstr \"\\n\"");
                WriteLine("\tcall void [mscorlib]System.Console::Write(string)");
            }
            else
            {
                WriteLine("\t call void " + node.GetId().Text + "()");
            }
        }

        //----------------------------------------------------------------------------------------------

        public override void OutANotExpression3(ANotExpression3 node)
        {
            //WriteLine("\te");
            WriteLine("\tldc.i4 0");
            WriteLine("ceq");
        }
        public override void OutAAndExpression2(AAndExpression2 node)
        {
            WriteLine("and");
        }

        public override void OutAOrExpression(AOrExpression node)
        {
            WriteLine("or");
        }

        //----------------------------------------------------------------------------------------------
        public override void OutAEqualExpression3(AEqualExpression3 node)
        {
            string equal_branch = "equal_branch" + equal_label;
            string equal_continue = "equal_continue" + equal_label; 
            WriteLine("\tbeq " + equal_branch);
            WriteLine("\t\tldc.i4 0");
            WriteLine("\t\tbr " + equal_continue);
            WriteLine("\t" + equal_branch + ":");
            WriteLine("\t\tldc.i4 1");
            WriteLine("\t" + equal_continue + ":");

            equal_label += 1;
        }

        public override void OutANotequalExpression4(ANotequalExpression4 node)
        {
            string not_equal_branch = "not_equal_branch" + not_equal_label;
            string not_equal_continue = "not_equal_continue" + not_equal_label;
            WriteLine("\tbne.un " + not_equal_branch);
            WriteLine("\t\tldc.i4 0");
            WriteLine("\t\tbr " + not_equal_continue);
            WriteLine("\t" + not_equal_branch + ":");
            WriteLine("\t\tldc.i4 1");
            WriteLine("\t" + not_equal_continue + ":");

            not_equal_label += 1;
        }

        public override void OutALessExpression4(ALessExpression4 node)
        {
            string less_branch = "less_branch" + less_label;
            string less_continue = "less_continue" + less_label;
            WriteLine("\tblt " + less_branch);
            WriteLine("\t\tldc.i4 0");
            WriteLine("\t\tbr " + less_continue);
            WriteLine("\t" + less_branch + ":");
            WriteLine("\t\tldc.i4 1");
            WriteLine("\t" + less_continue + ":");

            less_label += 1;
        }

        public override void OutALessequalExpression4(ALessequalExpression4 node)
        {
            string less_eq_branch = "less_eq_branch" + less_eq_label;
            string less_eq_continue = "less_eq_continue" + less_eq_label;
            WriteLine("\tble " + less_eq_branch);
            WriteLine("\t\tldc.i4 0");
            WriteLine("\t\tbr " + less_eq_continue);
            WriteLine("\t" + less_eq_branch + ":");
            WriteLine("\t\tldc.i4 1");
            WriteLine("\t" + less_eq_continue + ":");

            less_eq_label += 1;
        }

        public override void OutAGreaterExpression4(AGreaterExpression4 node)
        {
            string great_branch = "great_branch" + greater_label;
            string great_continue = "great_continue" + greater_label;
            WriteLine("\tbgt " + great_branch);
            WriteLine("\t\tldc.i4 0");
            WriteLine("\t\tbr " + great_continue);
            WriteLine("\t" + great_branch + ":");
            WriteLine("\t\tldc.i4 1");
            WriteLine("\t" + great_continue + ":");

            greater_label += 1;
        }

        public override void OutAGreatequalExpression4(AGreatequalExpression4 node)
        {
            string great_eq_branch = "great_eq_branch" + greater_eq_label;
            string great_eq_continue = "great_eq_continue" + greater_eq_label;
            WriteLine("\tbge " + great_eq_branch);
            WriteLine("\t\tldc.i4 0");
            WriteLine("\t\tbr " + great_eq_continue);
            WriteLine("\t" + great_eq_branch + ":");
            WriteLine("\t\tldc.i4 1");
            WriteLine("\t" + great_eq_continue + ":");

            greater_eq_label += 1;
        }
        //----------------------------------------------------------------------------------------------
        public override void CaseAIfIfStat(AIfIfStat node)
        {
            InAIfIfStat(node); //in a stuff
            if_label_1 += 1;
            if_label_2 += 1;
            if_label_3 += 1;

            if (node.GetExpression() != null)
            {
                node.GetExpression().Apply(this);
            }
            string if_branch_1 = "if_branch_1" + if_label_1;
            string if_branch_2 = "if_branch_2" + if_label_2;
            string if_branch_3 = "if_branch_3" + if_label_3;
            WriteLine("brtrue " + if_branch_1); //comparison
            WriteLine("br " + if_branch_2);

            WriteLine(if_branch_1 + ":");
            if (node.GetStatements() != null)
            {
                node.GetStatements().Apply(this);
            }
            WriteLine("br " + if_branch_3);

            OutAIfIfStat(node); //out a stuff

            //string if_branch_2 = "if_branch_2" + if_label_2;
            WriteLine(if_branch_2 + ":");

            if (node.GetElseStat() != null)
            {
                node.GetElseStat().Apply(this);
                WriteLine(if_branch_3 + ":");
            }
            //string if_branch_3 = "if_branch_3" + if_label_3;
            //WriteLine(if_branch_3 + ":"); //*****label 3

        }

        //----------------------------------------------------------------------------------------------

        public override void CaseAWhileStat(AWhileStat node)
        {
            InAWhileStat(node);
            while_label_1 += 1;
            while_label_2 += 1;

            string while_branch_1 = "while_label_1" + while_label_1;
            string while_branch_2 = "while_label_2" + while_label_2;

            WriteLine(while_branch_1 + ":");
            if (node.GetExpression() != null)
            {
                node.GetExpression().Apply(this);
            }
            WriteLine("brzero " + while_branch_2);

            if (node.GetStatements() != null)
            {
                node.GetStatements().Apply(this);
            }
            WriteLine("br " + while_branch_1);

            OutAWhileStat(node);
            WriteLine(while_branch_2 + ":");
        }
    }

}
