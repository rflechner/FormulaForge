
# Language

This document describes the domain specific language implemented in the project.

## Overview

This project is a __proof of concept__.

This small language tries to be natural for mathematical expressions.

Goal is to provide a simple and intuitive way to describe domain computations formulas.

Immutability is a principle of this language.

## Grammar

### Literals

There are the following types of literals:
- Integers
- Decimals
- Booleans

### Comments

Comments are denoted by `#`

### Variables

We can declare variables using the following syntax:

```bash
x = 123 # x is an integer
y = 12.345 # y is a decimal
z = true # z is a boolean
```

### Operators

Arithmetics operators supported:
- `+`: addition
- `-`: subtraction
- `*`: multiplication
- `/`: division
- `%`: modulo

#### Behavior of division

When dividing two integers, the result is not an integer.
The result is a decimal.

Example:

```bash
x = 12 / 3 # x is a decimal = 4.333333333333333
```


### Functions

#### Declaration

With this DSL, functions without parameters are supported.

Parameters are not explicitly typed.
Their type is inferred from the context.

Example with a square function:

```bash
square(x) = x * x
```

You can call this function like this:

```bash
x = square(123) # inference of the type is integer
y = square(12.345) # inference of the type is decimal
```

We can also declare functions with multiple parameters:

```bash
# example with two parameters
add(x, y) = x + y

# volume of a cuboid
volume(l, w, h) = l * w * h
```

#### Calling functions

A variable can be initialized with a function call:

```bash
x = add(1, 3) # f is an integer = 4
```

You can also call functions inside other functions:
```bash
x = square(add(1,2)) # x is an integer = 9 = (1 + 2)^2
```

