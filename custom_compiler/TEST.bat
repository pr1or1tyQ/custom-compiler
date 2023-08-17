:: Creates a Variable for the Output File
@SET file="pex_test_results.txt"

:: Erases Everything Currently In the Output File
type NUL>%file%

:: ----------------------------------------
:: TITLE
:: ----------------------------------------
echo PEX TEST CASES (C1C Henry & C1C Dickerman) >> %file%

:: Required Test Cases:
::Strings, Identifiers/reserved words, integers, floats, operators, comments

:: ----------------------------------------
:: GOOD EXAMPLES
:: ----------------------------------------
::echo Testing Identifiers >> %file%
::bin\Debug\ConsoleApplication.exe testcases\pex1\test1.txt >> %file%
::echo. >> %file%
echo Begin Correct Testing >> %file%
echo. >> %file%
::*************************************************************************************

echo Testing Variable Declarations >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\declaration_pass.txt >> %file%
echo. >> %file%

echo Testing Contant Declarations >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\constants_pass.txt >> %file%
echo. >> %file%

echo Testing If Statements >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\if_pass.txt >> %file%
echo. >> %file%

echo Testing While Statements >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\while_pass.txt >> %file%
echo. >> %file%

echo Testing Assignment Statements >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\assign_pass.txt >> %file%
echo. >> %file%

echo Testing Boolean Expressions >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\boolean_pass.txt >> %file%
echo. >> %file%

echo Testing Boolean Comparison Expressions >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\comparison_pass.txt >> %file%
echo. >> %file%

echo Testing Arithmetic Expressions >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\arithmetic_pass.txt >> %file%
echo. >> %file%

echo Testing Other Expressions >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\expression_pass.txt >> %file%
echo. >> %file%

echo Testing Procedure Declarations >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\procedure_pass.txt >> %file%
echo. >> %file%

echo Testing Procedure Calls >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\procedureCall_pass.txt >> %file%
echo. >> %file%

::*****************************************************************************************
echo. >> %file%
echo Correct Testing Complete >> %file%
echo. >> %file%

:: ----------------------------------------
:: BAD EXAMPLES
:: ----------------------------------------
::echo Running Incorrect Test Cases >> %file%
::echo. >> %file%
echo Running Incorrect Test Cases >> %file%
echo. >> %file%
::*********************************************************************************

echo Incorrect Test: Variable Declarations >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\declaration_fail_alreadyDeclared.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\declaration_fail_notDeclared.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\declaration_fail_notType.txt >> %file%
echo. >> %file%

echo Incorrect Test: Constant Declarations >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\constants_fail_alreadyDeclared.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\constants_fail_mismatchTypes.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\constants_fail_notDeclared.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\constants_fail_notType.txt >> %file%
echo. >> %file%

echo Incorrect Test: If Statements >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\if_fail_notBoolean.txt >> %file%
echo. >> %file%

echo Incorrect Test: While Statements >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\while_fail_notBoolean.txt >> %file%
echo. >> %file%

echo Incorrect Test: Assignment Statements >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\assign_fail_constant.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\assign_fail_mismatchTypes.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\assign_fail_notDeclared.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\assign_fail_notVariable.txt >> %file%
echo. >> %file%

echo Incorrect Test: Boolean Expressions >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\boolean_fail_mismatchTypes_and.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\boolean_fail_mismatchTypes_or.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\boolean_fail_notBoolean_and.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\boolean_fail_notBoolean_or.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\boolean_fail_notBoolean_not.txt >> %file%
echo. >> %file%

echo Incorrect Test: Boolean Comparison Expressions >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\comparison_fail_mismatchTypes_equal.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\comparison_fail_mismatchTypes_great.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\comparison_fail_mismatchTypes_greatequal.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\comparison_fail_mismatchTypes_less.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\comparison_fail_mismatchTypes_lessequal.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\comparison_fail_notNumber_equal.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\comparison_fail_notNumber_great.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\comparison_fail_notNumber_greatequal.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\comparison_fail_notNumber_less.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\comparison_fail_notNumber_lessequal.txt >> %file%
echo. >> %file%

echo Incorrect Test: Arithmetic Expressions >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\arithmetic_fail_mismatchTypes_add.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\arithmetic_fail_mismatchTypes_sub.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\arithmetic_fail_mismatchTypes_mult.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\arithmetic_fail_mismatchTypes_div.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\arithmetic_fail_notNumber_add.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\arithmetic_fail_notNumber_sub.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\arithmetic_fail_notNumber_mult.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\arithmetic_fail_notNumber_div.txt >> %file%
echo. >> %file%

echo Incorrect Test: Other Expressions >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\expression_fail_notDeclared.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\expression_fail_typeNameUsed.txt >> %file%
echo. >> %file%

echo Incorrect Test: Procedure Calls >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\procedureCall_fail_mismatchTypeParams.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\procedureCall_fail_notDeclared.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\procedureCall_fail_notProcedure.txt >> %file%
echo. >> %file%

echo Incorrect Test: Procedure Declarations >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\procedure_fail_doubleParameter.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\procedure_fail_nameAlreadyUsed.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\procedure_fail_parameterDeclaredWrong.txt >> %file%
echo. >> %file%

::**************************************************************************************
echo. >> %file%
echo Incorrect Test Cases Complete >> %file%

pause
