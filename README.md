# Custom Language Compiler
---

## Specification:
---
### Comments
	//This is a valid in line comment

	/{
	This is 
	what a block comment looks like
	}/


### Variable Declarations
	word myString;
	int myInt;
	real my_floating_point;

### Constant declarations
	CONST word myString = “hello string”
	CONST int the_num = 23
	CONST real big_num = 20.23
	CONST real big_num = 20e23

### Variable Assignments / Literal Values

	mySimpleString = “hello earth”;
	myString = “ \”escape\” single backslash: \\”;
	myInt = 13;
	myInt = -4;
	my_floating_point = 3.145;
	my_floating_point = 5.2e-4; 

### Conditional / Branching Statements

	if(flag AND check){
	//do the stuff
	}

	else{
	//do the other stuff
	}

	if(flag OR check){
	//do the backup stuff
	}

	if(NOT flag){
	//abort
	}

### Iterative Control Structure

	while(x > y AND ((a + b) / c * 100) == 23){

	//loop
	}

	while(x >= -2){
	//more loops
	}

### Function Definitions

	myFunct(int xParam, word wordName){
	//body of function
	}

	otherFunct(){
	//body of function
	}

### Main Function / Method
	MAIN(){
	//main function body, parameters are optional
	}

### Example program

	// calculates the circumference of a circle (C = 2*pi*r)

	CONST real pi = 3.14

	calcCircum(real radius){
	real circum;
	circum = 2 * pi * radius;

	}


	MAIN(){
	real radius;
	radius = 20.23;

	calcCircum(radius);
	}
