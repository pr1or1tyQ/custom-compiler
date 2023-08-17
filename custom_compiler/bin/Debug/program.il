.assembly extern mscorlib {}
.assembly myProgram
{
	.ver 1:0:1:0
}

.method static void Test_one() cil managed
{
	.maxstack 128

	ldstr "Variable Declaration Test"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	// Declaring Variable i 
	.locals init (int32 i)

	ldc.i4 1
	stloc i

	// Declaring Variable f_one 
	.locals init (float32 f_one)

	ldc.r8 2.3
	neg
	stloc f_one

	// Declaring Variable f_two 
	.locals init (float32 f_two)

	ldc.r8 3.4e5
	stloc f_two

	// Declaring Variable s 
	.locals init (string s)

	ldstr "hello world"
	stloc s

	ldstr "i = "
	call void [mscorlib]System.Console::Write(string)
	ldloc i
	call void [mscorlib]System.Console::Write(int32)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "float_one = "
	call void [mscorlib]System.Console::Write(string)
	ldloc f_one
	call void [mscorlib]System.Console::Write(float32)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "float_two = "
	call void [mscorlib]System.Console::Write(string)
	ldloc f_two
	call void [mscorlib]System.Console::Write(float32)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "s = "
	call void [mscorlib]System.Console::Write(string)
	ldloc s
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)

	ret
}
.method static void Test_two() cil managed
{
	.maxstack 128

	ldstr "Variable Assignment Test"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	// Declaring Variable x 
	.locals init (int32 x)

	ldc.i4 0
	stloc x

	// Declaring Variable y 
	.locals init (float32 y)

	ldc.r8 1.2
	stloc y

	// Declaring Variable s 
	.locals init (string s)

	ldstr "original string"
	stloc s

	ldstr "x original: "
	call void [mscorlib]System.Console::Write(string)
	ldloc x
	call void [mscorlib]System.Console::Write(int32)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "y original: "
	call void [mscorlib]System.Console::Write(string)
	ldloc y
	call void [mscorlib]System.Console::Write(float32)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "s original: "
	call void [mscorlib]System.Console::Write(string)
	ldloc s
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldc.i4 12345
	stloc x

	ldc.r8 6.789
	neg
	stloc y

	ldstr "dog goes \"woof\", cat goes \"meow\", but what does the fox say \\ sarcasm"
	stloc s

	ldstr "x: final "
	call void [mscorlib]System.Console::Write(string)
	ldloc x
	call void [mscorlib]System.Console::Write(int32)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "y: final "
	call void [mscorlib]System.Console::Write(string)
	ldloc y
	call void [mscorlib]System.Console::Write(float32)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "s: final "
	call void [mscorlib]System.Console::Write(string)
	ldloc s
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)

	ret
}
.method static void Test_three() cil managed
{
	.maxstack 128

	ldstr "Math Operation Test"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	// Declaring Variable x 
	.locals init (int32 x)

	ldc.i4 5
	ldc.i4 4
	sub
	ldc.i4 4
	add
	stloc x

	ldloc x
	ldc.i4 2
	mul
	ldc.i4 2
	div
	stloc x

	ldloc x
	ldloc x
	mul
	ldc.i4 5
	ldc.i4 5
	sub
	mul
	stloc x

	ldstr "x = "
	call void [mscorlib]System.Console::Write(string)
	ldloc x
	call void [mscorlib]System.Console::Write(int32)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	// Declaring Variable y 
	.locals init (float32 y)

	ldc.r8 10.44
	ldc.r8 9.79
	add
	ldc.r8 10.115
	sub
	stloc y

	ldc.r8 1.0
	neg
	ldloc y
	ldc.r8 2.0
	ldc.r8 1.0
	div
	mul
	mul
	stloc y

	ldstr "y = "
	call void [mscorlib]System.Console::Write(string)
	ldloc y
	call void [mscorlib]System.Console::Write(float32)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)

	ret
}
.method static void Test_four() cil managed
{
	.maxstack 128

	ldstr "Relational Operator Test"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldc.i4 5
	ldc.i4 2
	bgt great_branch0
		ldc.i4 0
		br great_continue0
	great_branch0:
		ldc.i4 1
	great_continue0:
brtrue if_branch_11
br if_branch_21
if_branch_11:
	ldstr "Greater than works"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
br if_branch_31
if_branch_21:
if_branch_31:
	ldc.i4 2
	ldc.i4 5
	blt less_branch0
		ldc.i4 0
		br less_continue0
	less_branch0:
		ldc.i4 1
	less_continue0:
brtrue if_branch_12
br if_branch_22
if_branch_12:
	ldstr "Less than works"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
br if_branch_32
if_branch_22:
if_branch_32:
	ldc.i4 4
	ldc.i4 4
	bge great_eq_branch0
		ldc.i4 0
		br great_eq_continue0
	great_eq_branch0:
		ldc.i4 1
	great_eq_continue0:
brtrue if_branch_13
br if_branch_23
if_branch_13:
	ldstr "Greater than or equal works"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
br if_branch_33
if_branch_23:
if_branch_33:
	ldc.i4 4
	ldc.i4 4
	ble less_eq_branch0
		ldc.i4 0
		br less_eq_continue0
	less_eq_branch0:
		ldc.i4 1
	less_eq_continue0:
brtrue if_branch_14
br if_branch_24
if_branch_14:
	ldstr "Less than or equal works"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
br if_branch_34
if_branch_24:
if_branch_34:
	ldc.r8 5.0
	ldc.r8 5.0
	beq equal_branch0
		ldc.i4 0
		br equal_continue0
	equal_branch0:
		ldc.i4 1
	equal_continue0:
brtrue if_branch_15
br if_branch_25
if_branch_15:
	ldstr "Equals works"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
br if_branch_35
if_branch_25:
if_branch_35:
	ldc.r8 2.5
	ldc.r8 3.0
	bne.un not_equal_branch0
		ldc.i4 0
		br not_equal_continue0
	not_equal_branch0:
		ldc.i4 1
	not_equal_continue0:
brtrue if_branch_16
br if_branch_26
if_branch_16:
	ldstr "Not Equals works"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
br if_branch_36
if_branch_26:
if_branch_36:
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)

	ret
}
.method static void Test_five() cil managed
{
	.maxstack 128

	ldstr "Boolean Operator Test"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldc.i4 1
	ldc.i4 2
	blt less_branch1
		ldc.i4 0
		br less_continue1
	less_branch1:
		ldc.i4 1
	less_continue1:
	ldc.i4 2
	ldc.i4 3
	blt less_branch2
		ldc.i4 0
		br less_continue2
	less_branch2:
		ldc.i4 1
	less_continue2:
and
brtrue if_branch_17
br if_branch_27
if_branch_17:
	ldstr "And works"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
br if_branch_37
if_branch_27:
if_branch_37:
	ldc.i4 2
	ldc.i4 1
	blt less_branch3
		ldc.i4 0
		br less_continue3
	less_branch3:
		ldc.i4 1
	less_continue3:
	ldc.i4 2
	ldc.i4 2
	blt less_branch4
		ldc.i4 0
		br less_continue4
	less_branch4:
		ldc.i4 1
	less_continue4:
or
	ldc.i4 3
	neg
	ldc.i4 10
	neg
	bgt great_branch1
		ldc.i4 0
		br great_continue1
	great_branch1:
		ldc.i4 1
	great_continue1:
or
brtrue if_branch_18
br if_branch_28
if_branch_18:
	ldstr "Or works"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
br if_branch_38
if_branch_28:
if_branch_38:
	ldc.i4 2
	ldc.i4 1
	blt less_branch5
		ldc.i4 0
		br less_continue5
	less_branch5:
		ldc.i4 1
	less_continue5:
	ldc.i4 0
ceq
brtrue if_branch_19
br if_branch_29
if_branch_19:
	ldstr "Not works"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
br if_branch_39
if_branch_29:
if_branch_39:
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)

	ret
}
.method static void Test_six() cil managed
{
	.maxstack 128

	ldstr "If Statement Test"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldc.r8 1.0
	ldc.r8 1.0
	add
	ldc.r8 3.0
	blt less_branch6
		ldc.i4 0
		br less_continue6
	less_branch6:
		ldc.i4 1
	less_continue6:
brtrue if_branch_110
br if_branch_210
if_branch_110:
	ldstr "If (true) Statement Passes"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
br if_branch_310
if_branch_210:
	ldstr "If (true) Statement Fails"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
if_branch_310:
	ldc.r8 5.0
	ldc.r8 3.0
	mul
	ldc.i4 1
	blt less_branch7
		ldc.i4 0
		br less_continue7
	less_branch7:
		ldc.i4 1
	less_continue7:
brtrue if_branch_111
br if_branch_211
if_branch_111:
	ldstr "If (false) Statement Fails"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
br if_branch_311
if_branch_211:
	ldstr "If (false) Statement Passes"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
if_branch_311:
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	 call void Test_seven()

	ret
}
.method static void Test_seven() cil managed
{
	.maxstack 128

	ldstr "Nested If Statement Test"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldc.i4 3
	ldc.i4 2
	bgt great_branch2
		ldc.i4 0
		br great_continue2
	great_branch2:
		ldc.i4 1
	great_continue2:
brtrue if_branch_112
br if_branch_212
if_branch_112:
	ldc.i4 5
	ldc.i4 4
	blt less_branch8
		ldc.i4 0
		br less_continue8
	less_branch8:
		ldc.i4 1
	less_continue8:
brtrue if_branch_113
br if_branch_213
if_branch_113:
	ldstr "Nested If Statement Fails"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
br if_branch_313
if_branch_213:
	ldstr "Nested If Statement Passes"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
if_branch_313:
br if_branch_312
if_branch_212:
if_branch_312:
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)

	ret
}
.method static void Test_eight() cil managed
{
	.maxstack 128

	ldstr "While Loop Test"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	// Declaring Variable i 
	.locals init (int32 i)

	ldc.i4 0
	stloc i

while_label_11:
	ldloc i
	ldc.i4 10
	blt less_branch9
		ldc.i4 0
		br less_continue9
	less_branch9:
		ldc.i4 1
	less_continue9:
brzero while_label_21
	ldloc i
	call void [mscorlib]System.Console::Write(int32)
	ldstr ", "
	call void [mscorlib]System.Console::Write(string)
	ldloc i
	ldc.i4 1
	add
	stloc i

br while_label_11
while_label_21:
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "Loop Complete"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	 call void Test_nine()

	ret
}
.method static void Test_nine() cil managed
{
	.maxstack 128

	ldstr "Nested While Loop Test"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	// Declaring Variable x 
	.locals init (int32 x)

	ldc.i4 0
	stloc x

	// Declaring Variable y 
	.locals init (int32 y)

	ldc.i4 0
	stloc y

while_label_12:
	ldloc y
	ldc.i4 3
	blt less_branch10
		ldc.i4 0
		br less_continue10
	less_branch10:
		ldc.i4 1
	less_continue10:
brzero while_label_22
while_label_13:
	ldloc x
	ldc.i4 3
	blt less_branch11
		ldc.i4 0
		br less_continue11
	less_branch11:
		ldc.i4 1
	less_continue11:
brzero while_label_23
	ldstr "x: "
	call void [mscorlib]System.Console::Write(string)
	ldloc x
	call void [mscorlib]System.Console::Write(int32)
	ldstr " ,y: "
	call void [mscorlib]System.Console::Write(string)
	ldloc y
	call void [mscorlib]System.Console::Write(int32)
	ldstr "; "
	call void [mscorlib]System.Console::Write(string)
	ldloc x
	ldc.i4 1
	add
	stloc x

br while_label_13
while_label_23:
	ldc.i4 0
	stloc x

	ldloc y
	ldc.i4 1
	add
	stloc y

br while_label_12
while_label_22:
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "Loop Complete"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)

	ret
}
.method static void main() cil managed
{
	.maxstack 128
	.entrypoint

	ldstr "Begin Main Program Test:"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	 call void Test_one()
	 call void Test_two()
	 call void Test_three()
	 call void Test_four()
	 call void Test_five()
	 call void Test_six()
	 call void Test_eight()
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "Main Program Test Complete"
	call void [mscorlib]System.Console::Write(string)

	ret
}
