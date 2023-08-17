using CS426.node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CS426.analysis
{
    internal class SemanticAnalyzer : DepthFirstAdapter     //internal class vs just class ???
    {
        // TODO  PEX code here

        // Global Symbol Table: Keep track of global stuff
        // Example: Function defintiions, data types, constants (anything with global scope
        Dictionary<string, Definition> globalSymbolTable = new Dictionary<string, Definition>();

        // Local Symbol Table: Keep track of local stuff
        // Examples: Local Variables
        Dictionary<string, Definition> localsymbolTable = new Dictionary<string, Definition>();

        // This is our decorated parse tree
        Dictionary<Node, Definition> decoratedParseTree = new Dictionary<Node, Definition>();

        List<VariableDefinition> parameters = new List<VariableDefinition>();
        List<VariableDefinition> procCallParameters = new List<VariableDefinition>();

        public void PrintWarning(Token t, String message)
        {
            Console.WriteLine("Line " + t.Line + ", Col " + t.Pos + ": " + message);
        }

        public override void  InAProgram(AProgram node)
        {
            // Create a Definition for Integers
            Definition intDefinition = new NumberDefinition();
            intDefinition.name = "int";

            Definition strDefinition = new StringDefinition();
            //strDefinition.name = "string";
            strDefinition.name = "word";

            Definition floatDefinition = new NumberDefinition();
            //floatDefinition.name = "float";
            floatDefinition.name = "real";

            Definition floatExpDefinition = new NumberDefinition();
            floatExpDefinition.name = "float_exp";

            // Register the definition with the global symbol table
            globalSymbolTable.Add("int", intDefinition);
            //globalSymbolTable.Add("string", strDefinition);
            globalSymbolTable.Add("word", strDefinition);
            //globalSymbolTable.Add("float", floatDefinition);
            globalSymbolTable.Add("real", floatDefinition);
            globalSymbolTable.Add("float_exp", floatExpDefinition);
        }

        // ---------------------------------------------------------
        // Operand
        // ---------------------------------------------------------
        public override void OutAIntOperand(AIntOperand node)
        {
            // Create the definition
            Definition intDefinition = new NumberDefinition();
            intDefinition.name = "int";

            decoratedParseTree.Add(node, intDefinition);
            VariableDefinition newVarDef = new VariableDefinition();
            NumberDefinition newNumDef = new NumberDefinition();
            //TypeDefinition newNumDef = new (NumberDefinition)TypeDefinition();
            newVarDef.name = "doesn't matter";
            newVarDef.variableType = newNumDef;
            procCallParameters.Add(newVarDef);
        }

        public override void OutAStringOperand(AStringOperand node)
        {
            //Create the definition
            Definition strDefinition = new StringDefinition();
            //strDefinition.name = "string";
            strDefinition.name = "word";

            decoratedParseTree.Add(node, strDefinition);
            VariableDefinition newVarDef = new VariableDefinition();
            StringDefinition newStringDefinition = new StringDefinition();
            newVarDef.name = "doesn't matter";
            newVarDef.variableType = newStringDefinition;
            procCallParameters.Add(newVarDef);
        }

        public override void OutAFloatOperand(AFloatOperand node)
        {
            Definition floatDefinition = new NumberDefinition(); // do I need to make IntDefinition and FloatDefinition to distinguish???
            //floatDefinition.name = "float";
            floatDefinition.name = "real";

            decoratedParseTree.Add(node, floatDefinition);
        }
        public override void OutAFloatExpOperand(AFloatExpOperand node)
        {
            Definition floatExpOperand = new NumberDefinition();
            floatExpOperand.name = "float_exp";

            decoratedParseTree.Add(node, floatExpOperand);
            VariableDefinition newVarDef = new VariableDefinition();
            NumberDefinition newNumDef = new NumberDefinition();
            newVarDef.name = "doesn't matter";
            newVarDef.variableType = newNumDef;
            procCallParameters.Add(newVarDef);
        }

        public override void OutAVariableOperand(AVariableOperand node)
        {
            // Get the Name of the ID
            String varName = node.GetId().Text;

            Definition varDefinition;

            // Test to see if the varName is in the symbol table
            if(globalSymbolTable.TryGetValue(varName, out varDefinition))
            {
                if(!(varDefinition is ConstantDefinition))
                {
                    if (!localsymbolTable.TryGetValue(varName, out varDefinition))
                    {
                        PrintWarning(node.GetId(), varName + " does not exist!");
                    }
                }
            }
            else if (!localsymbolTable.TryGetValue(varName, out varDefinition))
            {
                PrintWarning(node.GetId(), varName + " does not exist!");
            }
            // Test to see if varDefinition is actually a variable
            else if(!(varDefinition is VariableDefinition))
            {
                PrintWarning(node.GetId(), varName + " is not a variable");
            }
            else
            {
                VariableDefinition v = (VariableDefinition)varDefinition;

                // Decorating the node with the type of the variable
                decoratedParseTree.Add(node, v.variableType);

                VariableDefinition newVarDef = new VariableDefinition();
     
                newVarDef.name = "doesn't matter";
                newVarDef.variableType = v.variableType;
                procCallParameters.Add(newVarDef);
            }
        }

        public override void OutAConstantDeclareStatement(AConstantDeclareStatement node)
        {
            // Get the name of the ID
            //string constName = node.GetId().Text; // having trouble with this, possible error with naming
            // Get the value assigned
            //compare to global symbol table*******
        }

        // ---------------------------------------------------------
        // Operand1
        // ---------------------------------------------------------
        public override void OutAPassOperand1(APassOperand1 node)
        {
            Definition operandDefinition;

            if(!decoratedParseTree.TryGetValue(node.GetOperand(), out operandDefinition)) //if operand is there pass down
            {
                //if it is not indecorated parse tree, then the error will be printed at a lower level in the tree
            }
            else
            {
                decoratedParseTree.Add(node, operandDefinition);
            }
        }

        public override void OutAParenthesisOperand1(AParenthesisOperand1 node) //*******need to make sure this is correct
        {
            Definition expressionDefinition;

            if (!decoratedParseTree.TryGetValue(node.GetExpression(), out expressionDefinition)) //if operand is there pass down
            {
                //if it is not indecorated parse tree, then the error will be printed at a lower level in the tree
            }
            else if( (expressionDefinition is FunctionDefinition) || (expressionDefinition is ConstantDefinition)){
                //PrintWarning(node.GetExpression(), "Only an expression can go inside of parenthesis");
            }
            else
            {
                decoratedParseTree.Add(node, expressionDefinition);
            }

        }

        // ---------------------------------------------------------
        // Expression7
        // ---------------------------------------------------------
        public override void OutAPassExpression7(APassExpression7 node)
        {
            Definition operand1Definition;

            if (!decoratedParseTree.TryGetValue(node.GetOperand1(), out operand1Definition)) //if operand is there pass down
            {
                //if it is not indecorated parse tree, then the error will be printed at a lower level in the tree
            }
            else
            {
                decoratedParseTree.Add(node, operand1Definition);
            }
        }

        public override void OutANegativeExpression7(ANegativeExpression7 node)
        {
            Definition operandDefinition;

            if (!decoratedParseTree.TryGetValue(node.GetOperand(), out operandDefinition)){
                //error will be printed at lower level
            }
            else if (!(operandDefinition is NumberDefinition))
            {
                PrintWarning(node.GetSub(), "Only a number can be negated");
            }
            else
            {
                decoratedParseTree.Add(node, operandDefinition);
            }
        }

        // ---------------------------------------------------------
        // Expression6
        // ---------------------------------------------------------
        public override void OutAPassExpression6(APassExpression6 node)
        {
            Definition expression6Definition;

            if (!decoratedParseTree.TryGetValue(node.GetExpression7(), out expression6Definition)) //if operand is there pass down
            {
                //if it is not indecorated parse tree, then the error will be printed at a lower level in the tree
            }
            else
            {
                decoratedParseTree.Add(node, expression6Definition);
            }
        }

        public override void OutAMultiplyExpression6(AMultiplyExpression6 node)
        {
            Definition expression6Def;
            Definition expression7Def;

            if (!decoratedParseTree.TryGetValue(node.GetExpression6(), out expression6Def))
            {
                // Error occured but we don't have to print it
            }
            else if (!decoratedParseTree.TryGetValue(node.GetExpression7(), out expression7Def))
            {
                // Error occured but we don't have to print it
            }
            else if (expression6Def.name != expression7Def.name)
            {
                PrintWarning(node.GetMult(), "Type Mismatch: " + expression6Def.name + " and " + expression7Def.name);
            }
            else if(expression6Def.GetType() != expression7Def.GetType())
            {
                PrintWarning(node.GetMult(), "Cannot multiply " + expression6Def.name + " by " + expression7Def.name);
            }
            else if(!(expression6Def is NumberDefinition))
            {
                PrintWarning(node.GetMult(), "You can only multiply numbers");
            }
            else
            {
                decoratedParseTree.Add(node, expression6Def);
            }
        }

        public override void OutADivideExpression6(ADivideExpression6 node)
        {
            Definition expression6Def;
            Definition expression7Def;

            if (!decoratedParseTree.TryGetValue(node.GetExpression6(), out expression6Def))
            {
                // Error occured but we don't have to print it
            }
            else if (!decoratedParseTree.TryGetValue(node.GetExpression7(), out expression7Def))
            {
                // Error occured but we don't have to print it
            }
            else if (expression6Def.name != expression7Def.name)
            {
                PrintWarning(node.GetDiv(), "Type Mismatch: " + expression6Def.name + " and " + expression7Def.name);
            }
            else if (expression6Def.GetType() != expression7Def.GetType())
            {
                PrintWarning(node.GetDiv(), "Cannot divide " + expression6Def.name + " by " + expression7Def.name);
            }
            else if (!(expression6Def is NumberDefinition))
            {
                PrintWarning(node.GetDiv(), "You can only divide numbers");
            }
            else
            {
                decoratedParseTree.Add(node, expression6Def);
            }
        }


        // ---------------------------------------------------------
        // Expression5
        // ---------------------------------------------------------
        public override void OutAPassExpression5(APassExpression5 node)
        {
            Definition expression5Definition;

            if (!decoratedParseTree.TryGetValue(node.GetExpression6(), out expression5Definition)) //if operand is there pass down
            {
                //if it is not indecorated parse tree, then the error will be printed at a lower level in the tree
            }
            else
            {
                decoratedParseTree.Add(node, expression5Definition);
            }
        }

        public override void OutAAddExpression5(AAddExpression5 node)
        {
            Definition expression5Type;
            Definition expression6Type;

            if(!decoratedParseTree.TryGetValue(node.GetExpression5(), out expression5Type))
            {
                //there is an error here but we don't need to print it
            }
            else if (!decoratedParseTree.TryGetValue(node.GetExpression6(), out expression6Type))
            {
                //there is an error here but we don't need to print it
            }
            else if(expression5Type.name != expression6Type.name)
            {
                PrintWarning(node.GetPlus(), "Could nod add " + expression5Type.name + " and " + expression6Type.name);
            }
            else if(!(expression5Type is NumberDefinition))
            {
                PrintWarning(node.GetPlus(), "Cannot add non numbers");
            }
            else
            {
                decoratedParseTree.Add(node, expression5Type);
            }
        }

        public override void OutASubExpression5(ASubExpression5 node)
        {
            Definition expression5Type;
            Definition expression6Type;

            if (!decoratedParseTree.TryGetValue(node.GetExpression5(), out expression5Type))
            {
                //there is an error here but we don't need to print it
            }
            else if (!decoratedParseTree.TryGetValue(node.GetExpression6(), out expression6Type))
            {
                //there is an error here but we don't need to print it
            }
            else if (expression5Type.name != expression6Type.name)
            {
                PrintWarning(node.GetSub(), "Could nod subtract " + expression5Type.name + " and " + expression6Type.name);
            }
            else if (!(expression5Type is NumberDefinition))
            {
                PrintWarning(node.GetSub(), "Cannot subtract non numbers");
            }
            else
            {
                decoratedParseTree.Add(node, expression5Type);
            }
        }

        // ---------------------------------------------------------
        // Expression4
        // ---------------------------------------------------------
        public override void OutAPassExpression4(APassExpression4 node)
        {
            Definition expression4Definition;

            if (!decoratedParseTree.TryGetValue(node.GetExpression5(), out expression4Definition)) //if operand is there pass down
            {
                //if it is not indecorated parse tree, then the error will be printed at a lower level in the tree
            }
            else
            {
                decoratedParseTree.Add(node, expression4Definition);
            }
        }

        public override void OutALessequalExpression4(ALessequalExpression4 node)
        {
            Definition expression5Def_one;
            Definition expression5Def_two;

            if (!decoratedParseTree.TryGetValue(node.GetOne(), out expression5Def_one))
            {
                // Error occured but we don't have to print it
            }
            else if (!decoratedParseTree.TryGetValue(node.GetTwo(), out expression5Def_two))
            {
                // Error occured but we don't have to print it
            }
            else if (expression5Def_one.name != expression5Def_two.name)
            {
                PrintWarning(node.GetLogLessEq(), "Type Mismatch: " + expression5Def_one.name + " and " + expression5Def_two.name);
            }
            else if (expression5Def_one.GetType() != expression5Def_two.GetType())
            {
                PrintWarning(node.GetLogLessEq(), "Cannot evaluate " + expression5Def_one.name + " with " + expression5Def_two.name);
            }
            else if (!(expression5Def_one is NumberDefinition))
            {
                PrintWarning(node.GetLogLessEq(), "You can only evaluate numbers");
            }
            else
            {
                //decoratedParseTree.Add(node, expression5Def_one);
                Definition newBool = new BooleanDefinition();
                decoratedParseTree.Add(node, newBool);
            }
        }

        public override void OutAGreatequalExpression4(AGreatequalExpression4 node)
        {
            Definition expression5Def_one;
            Definition expression5Def_two;

            if (!decoratedParseTree.TryGetValue(node.GetOne(), out expression5Def_one))
            {
                // Error occured but we don't have to print it
            }
            else if (!decoratedParseTree.TryGetValue(node.GetTwo(), out expression5Def_two))
            {
                // Error occured but we don't have to print it
            }
            else if (expression5Def_one.name != expression5Def_two.name)
            {
                PrintWarning(node.GetLogGreatEq(), "Type Mismatch: " + expression5Def_one.name + " and " + expression5Def_two.name);
            }
            else if (expression5Def_one.GetType() != expression5Def_two.GetType())
            {
                PrintWarning(node.GetLogGreatEq(), "Cannot evaluate " + expression5Def_one.name + " with " + expression5Def_two.name);
            }
            else if (!(expression5Def_one is NumberDefinition))
            {
                PrintWarning(node.GetLogGreatEq(), "You can only evaluate numbers");
            }
            else
            {
                //decoratedParseTree.Add(node, expression5Def_one);
                Definition newBool = new BooleanDefinition();
                decoratedParseTree.Add(node, newBool);
            }
        }

        public override void OutALessExpression4(ALessExpression4 node)
        {
            Definition expression5Def_one;
            Definition expression5Def_two;

            if (!decoratedParseTree.TryGetValue(node.GetOne(), out expression5Def_one))
            {
                // Error occured but we don't have to print it
            }
            else if (!decoratedParseTree.TryGetValue(node.GetTwo(), out expression5Def_two))
            {
                // Error occured but we don't have to print it
            }
            else if (expression5Def_one.name != expression5Def_two.name)
            {
                PrintWarning(node.GetLogLess(), "Type Mismatch: " + expression5Def_one.name + " and " + expression5Def_two.name);
            }
            else if (expression5Def_one.GetType() != expression5Def_two.GetType())
            {
                PrintWarning(node.GetLogLess(), "Cannot evaluate " + expression5Def_one.name + " with " + expression5Def_two.name);
            }
            else if (!(expression5Def_one is NumberDefinition))
            {
                PrintWarning(node.GetLogLess(), "You can only evaluate numbers");
            }
            else
            {
                //decoratedParseTree.Add(node, expression5Def_one);
                Definition newBool = new BooleanDefinition();
                decoratedParseTree.Add(node, newBool);
            }
        }

        public override void OutAGreaterExpression4(AGreaterExpression4 node)
        {
            Definition expression5Def_one;
            Definition expression5Def_two;

            if (!decoratedParseTree.TryGetValue(node.GetOne(), out expression5Def_one))
            {
                // Error occured but we don't have to print it
            }
            else if (!decoratedParseTree.TryGetValue(node.GetTwo(), out expression5Def_two))
            {
                // Error occured but we don't have to print it
            }
            else if (expression5Def_one.GetType() != expression5Def_two.GetType())
            {
                PrintWarning(node.GetLogGreat(), "Cannot evaluate " + expression5Def_one.name + " with " + expression5Def_two.name);
            }
            else if (expression5Def_one.name != expression5Def_two.name)
            {
                PrintWarning(node.GetLogGreat(), "Type Mismatch: " + expression5Def_one.name + " and " + expression5Def_two.name);
            }
            else if (!(expression5Def_one is NumberDefinition))
            {
                PrintWarning(node.GetLogGreat(), "You can only evaluate numbers");
            }
            else
            {
                Definition newBool = new BooleanDefinition();
                decoratedParseTree.Add(node, newBool);
            }
        }

        // ---------------------------------------------------------
        // Expression3
        // ---------------------------------------------------------
        public override void OutAPassExpression3(APassExpression3 node)
        {
            Definition expression3Definition;

            if (!decoratedParseTree.TryGetValue(node.GetExpression4(), out expression3Definition)) //if operand is there pass down
            {
                //if it is not indecorated parse tree, then the error will be printed at a lower level in the tree
            }
            else
            {
                decoratedParseTree.Add(node, expression3Definition);
            }
        }

        public override void OutAEqualExpression3(AEqualExpression3 node)
        {
            Definition expression5Def_one;
            Definition expression5Def_two;

            if (!decoratedParseTree.TryGetValue(node.GetOne(), out expression5Def_one))
            {
                // Error occured but we don't have to print it
            }
            else if (!decoratedParseTree.TryGetValue(node.GetTwo(), out expression5Def_two))
            {
                // Error occured but we don't have to print it
            }
            else if (expression5Def_one.name != expression5Def_two.name)
            {
                PrintWarning(node.GetLogEquiv(), "Type Mismatch: " + expression5Def_one.name + " and " + expression5Def_two.name);
            }
            else if (expression5Def_one.GetType() != expression5Def_two.GetType())
            {
                PrintWarning(node.GetLogEquiv(), "Cannot evaluate " + expression5Def_one.name + " with " + expression5Def_two.name);
            }
            else if (!(expression5Def_one is NumberDefinition))
            {
                PrintWarning(node.GetLogEquiv(), "You can only evaluate numbers");
            }
            else
            {
                //decoratedParseTree.Add(node, expression5Def_one);
                Definition newBool = new BooleanDefinition();
                decoratedParseTree.Add(node, newBool);
            }
        }

        public override void OutANotExpression3(ANotExpression3 node)
        {
            Definition expression5Def_one;

            if (!decoratedParseTree.TryGetValue(node.GetExpression5(), out expression5Def_one))
            {
                // Error occured but we don't have to print it
            }
            else if (!(expression5Def_one is BooleanDefinition))
            {
                PrintWarning(node.GetLogNot(), "You can only evaluate booleans");
            }
            else
            {
                decoratedParseTree.Add(node, expression5Def_one);
            }

        }

        // ---------------------------------------------------------
        // Expression2
        // ---------------------------------------------------------
        public override void OutAPassExpression2(APassExpression2 node)
        {
            Definition expression2Definition;

            if (!decoratedParseTree.TryGetValue(node.GetExpression3(), out expression2Definition)) //if operand is there pass down
            {
                //if it is not indecorated parse tree, then the error will be printed at a lower level in the tree
            }
            else
            {
                decoratedParseTree.Add(node, expression2Definition);
            }
        }

        public override void OutAAndExpression2(AAndExpression2 node)
        {
                Definition expression5Def_one;
                Definition expression5Def_two;

                if (!decoratedParseTree.TryGetValue(node.GetOne(), out expression5Def_one))
                {
                    // Error occured but we don't have to print it
                }
                else if (!decoratedParseTree.TryGetValue(node.GetTwo(), out expression5Def_two))
                {
                    // Error occured but we don't have to print it
                }
                else if (expression5Def_one.GetType() != expression5Def_two.GetType())
                {
                    PrintWarning(node.GetLogAnd(), "Cannot evaluate " + expression5Def_one.name + " with " + expression5Def_two.name);
                }
                else if (!(expression5Def_one is BooleanDefinition))
                {
                    PrintWarning(node.GetLogAnd(), "You can only evaluate booleans");
                }
                else
                {
                    decoratedParseTree.Add(node, expression5Def_one);
                }
        }

        // ---------------------------------------------------------
        // Expression
        // --------------------------------------------------------
        public override void OutAPassExpression(APassExpression node)
        {
            Definition expressionDefinition;

            if (!decoratedParseTree.TryGetValue(node.GetExpression2(), out expressionDefinition)) //if operand is there pass down
            {
                //if it is not indecorated parse tree, then the error will be printed at a lower level in the tree
            }
            else
            {
                decoratedParseTree.Add(node, expressionDefinition);
            }
        }

        public override void OutAOrExpression(AOrExpression node)
        {
            Definition expression5Def_one;
            Definition expression5Def_two;

            if (!decoratedParseTree.TryGetValue(node.GetOne(), out expression5Def_one))
            {
                // Error occured but we don't have to print it
            }
            else if (!decoratedParseTree.TryGetValue(node.GetTwo(), out expression5Def_two))
            {
                // Error occured but we don't have to print it
            }
            else if (expression5Def_one.GetType() != expression5Def_two.GetType())
            {
                PrintWarning(node.GetLogOr(), "Types don't mathch: Cannot evaluate " + expression5Def_one.name + " with " + expression5Def_two.name);
            }
            else if (!(expression5Def_one is BooleanDefinition))
            {
                PrintWarning(node.GetLogOr(), "You can only evaluate booleans");
            }
            else
            {
                decoratedParseTree.Add(node, expression5Def_one);
            }
        }

        // ---------------------------------------------------------
        // While_Stat
        // ---------------------------------------------------------
        public override void OutAWhileStat(AWhileStat node)
        {
            //***need to check that while and if are working properly
            Definition expressionDef;

            if(!decoratedParseTree.TryGetValue(node.GetExpression(), out expressionDef))
            {
                //error printed elsewhere
            }
            else if(!(expressionDef is BooleanDefinition))
            {
                Console.WriteLine("You can only evaluate booleans in the expression");
            }
        }

        // ---------------------------------------------------------
        // If_Stat
        // ---------------------------------------------------------
        public override void OutAIfIfStat(AIfIfStat node)
        {
            Definition expressionDef;

            if (!decoratedParseTree.TryGetValue(node.GetExpression(), out expressionDef))
            {
                //error printed elsewhere
            }
            else if (!(expressionDef is BooleanDefinition))
            {
                Console.WriteLine("You can only evaluate booleans in the expression");
            }
        }

        // ---------------------------------------------------------
        // Declare Statements
        // ---------------------------------------------------------

        //Variable Declaration
        public override void OutAVariableDeclareStatement(AVariableDeclareStatement node)
        {
            Definition typeDef;
            Definition idDef;
            Definition constCheck;
            //check to make sure the type definition (stored in global table) exists
            //if it exists store it in typeDef
            if (!globalSymbolTable.TryGetValue(node.GetType().Text, out typeDef)) 
            {
                PrintWarning(node.GetType(), "Type " + node.GetType().Text + " does not exist"); //
            }
            //make sure the grabbed typeDef is a TypeDefinition
            else if(!(typeDef is TypeDefinition))
            {
                PrintWarning(node.GetType(), "Identifier " + node.GetType().Text + " is not a recognized data type");
            }
            //make sure the identifier we got is not already being used in our local symbol table
            else if (localsymbolTable.TryGetValue(node.GetVarname().Text, out idDef))
            {
                PrintWarning(node.GetVarname(), "Identifier " + node.GetVarname().Text + " is already being used");
            }
            else if (globalSymbolTable.TryGetValue(node.GetVarname().Text, out constCheck))
            {
                //PrintWarning(node.GetId(), node.GetId().Text + " is a constant variable - cannot be changed");
                if (constCheck is ConstantDefinition)
                {
                    PrintWarning(node.GetVarname(), node.GetVarname().Text + " is a constant variable - cannot be changed");
                }
            }
            //all test passed - valid variable definition
            else
            {
                VariableDefinition newVariableDefinition = new VariableDefinition();
                newVariableDefinition.name = node.GetVarname().Text;
                newVariableDefinition.variableType = (TypeDefinition)typeDef;

                localsymbolTable.Add(node.GetVarname().Text, newVariableDefinition);
            }
        }

        // ---------------------------------------------------------
        // Assignment
        // ---------------------------------------------------------

        //Variable Assign Statement
        public override void OutAVariableAssignStatement(AVariableAssignStatement node)
        {
            Definition idDef;
            Definition expressionDef;

            Definition constCheck;

            //does the id exist in the local symbol table
            if (!localsymbolTable.TryGetValue(node.GetId().Text, out idDef))
            {
                PrintWarning(node.GetId(), "Identifier " + node.GetId().Text + " does not exist");
            }
            else if(!(idDef is VariableDefinition))
            {
                PrintWarning(node.GetId(), "Identifier " + node.GetId().Text + " is not a variable");
            }
            else if(!decoratedParseTree.TryGetValue(node.GetExpression(),out expressionDef)){
                //error message printed further down in tree
            }
            else if(((VariableDefinition)idDef).variableType.name != expressionDef.name)
            {
                PrintWarning(node.GetId(), "Types don't match");
            }
            else if(globalSymbolTable.TryGetValue(node.GetId().Text, out constCheck))
            {
                //PrintWarning(node.GetId(), node.GetId().Text + " is a constant variable - cannot be changed");
                if (constCheck is ConstantDefinition)
                {
                    PrintWarning(node.GetId(), node.GetId().Text + " is a constant variable - cannot be changed");
                }
            }
            else
            {
                //nothing is required, don't need to decorate this node if all tests pass
            }
        }

        //Constant Assign Statement
        public override void OutAConstAssignStatement(AConstAssignStatement node)
        {
            Definition constType;
            Definition constVarName;
            Definition expressionDef;

            //***need to test this, not complete...
            if (!globalSymbolTable.TryGetValue(node.GetType().Text, out constType))
            {
                PrintWarning(node.GetType(), node.GetType().Text + " is not a valid type");
            }
            else if (!(constType is TypeDefinition))
            {
                PrintWarning(node.GetType(), node.GetType().Text + " is not a recognized data type");
            }
            else if (globalSymbolTable.TryGetValue(node.GetVarname().Text, out constVarName))
            {
                PrintWarning(node.GetVarname(), "Identifier " + node.GetVarname().Text + " already exists");
            }
            else if (!decoratedParseTree.TryGetValue(node.GetExpression(), out expressionDef))
            {
                //error message printed further down
            }
            else if ( node.GetType().Text != expressionDef.name)
            {
                PrintWarning(node.GetVarname(), "Types don't match");
            }
            //else if ( ((ConstantDefinition)constVarName).variableType.name != expressionDef.name){
            //   PrintWarning(node.GetVarname(), "Types don't match");
             //}
            else
            {
                ConstantDefinition newConstantDefinition = new ConstantDefinition();
                newConstantDefinition.name = node.GetVarname().Text;
                newConstantDefinition.variableType = (TypeDefinition)constType;
                globalSymbolTable.Add(node.GetVarname().Text, newConstantDefinition);
            }
        }

        // ---------------------------------------------------------
        // Procedure Call
        // ---------------------------------------------------------
        public override void InAParamCall(AParamCall node)
        {
            procCallParameters = new List<VariableDefinition>();

        }

        public override void OutAParamCall(AParamCall node)
        {
            Definition idDef;
            Definition funcDef;
            FunctionDefinition func = new FunctionDefinition();
            
            if (!globalSymbolTable.TryGetValue(node.GetId().Text, out idDef)){
                PrintWarning(node.GetId(), "ID " + node.GetId().Text + " not found");
            }
            else if(!(idDef is FunctionDefinition))
            {
                PrintWarning(node.GetId(), "ID " + node.GetId().Text + " is not a function");
            }

            for (int i = 0; i < parameters.Count; i++)
            {
                if((String.Compare(parameters[i].variableType.ToString(), procCallParameters[i].variableType.ToString())) != 0 )
                {
                    Console.WriteLine("Fromal paramaters: " + parameters[i].variableType + " and Actual Parameters: " + procCallParameters[i].variableType + " do not match.");
                }
            }


        }

        public override void OutANoParams(ANoParams node)
        {
            Definition idDef;

            if (!globalSymbolTable.TryGetValue(node.GetId().Text, out idDef))
            {
                PrintWarning(node.GetId(), "ID " + node.GetId().Text + " not found");
            }
            else if(!(idDef is FunctionDefinition))
            {
                PrintWarning(node.GetId(), "ID " + node.GetId().Text + " is not a function");
            }
            else
            {
                //***do I need to do anything here
            }
        }

        // ---------------------------------------------------------
        // Function Definition
        // ---------------------------------------------------------

        //func_def
        public override void InAParamsFuncDef(AParamsFuncDef node)
        {
            Definition idDef;
         

            if(globalSymbolTable.TryGetValue(node.GetId().Text, out idDef))
            {
                PrintWarning(node.GetId(), "Identifier " + node.GetId().Text + " is already being used");
            }
            else
            {
                localsymbolTable = new Dictionary<string, Definition>();
                parameters = new List<VariableDefinition>();

                //Register the new function definition in the global table
                FunctionDefinition newFunctionDefinition = new FunctionDefinition();
                newFunctionDefinition.name = node.GetId().Text;

                newFunctionDefinition.parameters = new List<VariableDefinition>();

                globalSymbolTable.Add(node.GetId().Text, newFunctionDefinition);
            }
        }

        public override void OutAParamsFuncDef(AParamsFuncDef node)
        {
            //clear the local symbol table
            localsymbolTable = new Dictionary<string, Definition>();
        }


        public override void InANoparamsFuncDef(ANoparamsFuncDef node)
        {
            Definition idDef;

            if (globalSymbolTable.TryGetValue(node.GetId().Text, out idDef))
            {
                PrintWarning(node.GetId(), "Identifier " + node.GetId().Text + " is already being used");
            }
            else
            {
                localsymbolTable = new Dictionary<string, Definition>();

                //Register the new function definition in the global table
                FunctionDefinition newFunctionDefinition = new FunctionDefinition();
                newFunctionDefinition.name = node.GetId().Text;

                newFunctionDefinition.parameters = new List<VariableDefinition>();

                globalSymbolTable.Add(node.GetId().Text, newFunctionDefinition);
            }
        }

        public override void OutANoparamsFuncDef(ANoparamsFuncDef node)
        {
            //clear the local symbol table
            localsymbolTable = new Dictionary<string, Definition>();
        }

        public override void OutADefParam(ADefParam node)
        {
            Definition idDef1; //type
            Definition idDef2; //variable name
            Boolean addMe = true;

            if (!globalSymbolTable.TryGetValue(node.GetType().Text, out idDef1))
            {
                PrintWarning(node.GetType(), "ID " + node.GetType().Text + " not found");
            }
            else if(!(idDef1 is TypeDefinition))
            {
                PrintWarning(node.GetType(), "ID " + node.GetType().Text + " is not a valid type");
            }

            VariableDefinition newDef = new VariableDefinition();
            newDef.variableType = (TypeDefinition)idDef1;
            newDef.name = node.GetVarname().Text;

            for(int i = 0; i < parameters.Count; i++)
            {
                if (string.Equals(parameters[i].name,newDef.name))
                {
                    PrintWarning(node.GetVarname(), "Variable " + node.GetVarname().Text + " already exists");
                    addMe = false;
                }
            }
            if(addMe)
            {
                localsymbolTable.Add(node.GetVarname().Text, newDef);
                parameters.Add(newDef);
            
            }
        }
    }

}
