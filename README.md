# EzPasswordValidator      
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)   
--

A .NET standard library for easy password validation.
This library defines 11 predefined checks and an easy way to implement custom checks.     

## :scroll: Table of contents :scroll:
* [Predefined checks](#Checks)
* [Usage](#Usage)
* [How to contribute](#Contribute)
* [License info](#License)

## Checks
There are 11 predfined checks each representing a password criteria. 

##### Length check 
Checks if the given password is equal to or longer than the required minimum length.

##### Check for numbers
Checks that the password contains at least one digit.

##### Check for letters
Checks that the password contains at least one letter.

##### Check for symbols
Checks that the password contains at least one symbol.

##### Case check
Checks that the password contains at least one upper- and lower-case letter.

##### Check for number sequences
Checks if the password contains a number series 3 or longer such as 123 or 765.

##### Check for number repetition
Checks if the password contains number repetition 3 or longer in length, such as 444 or 222.

##### Check for number location 
Checks that the password does not only have numbers in the front and/or end of the password.

##### Check for letter sequences
Checks if the password contains an alphabetical letter sequence consisting of four or more
characters. With the exception of the common three letter sequences: abc and xyz.
        
##### Check for letter repetition
Checks if the password contains letter repetition 3 or longer in length.
The check is not case sensitive meaning 'aAA' and 'aaa' will both match.

##### Check for symbol repetition
Checks for immediate symbol repetition 3 or longer in sequence.

## Usage
1. Download the .dll file. (See releases)
2. Add the .dll file into your project.
   In Visual Studio go to: <i>Solution explorer > Dependencies > Add reference > browse </i> and select the .dll file.<br/>
3. Code

Example validator with predefined basic checks.
```C#
var validator = new PasswordValidator(CheckTypes.Basic);
```

##### Validate
```C#
bool isValid = validator.Validate(password);
```

<i>Failed validation</i> 
```C#
foreach (Check failedCheck in validator.FailedChecks)
{
    
}
```

##### Add checks
All predefined check types are defined [here.](source/EzPasswordValidator/Checks/CheckTypes.cs)

<i>Add single predefined check</i>
```C#
 validator.AddCheck(CheckTypes.LetterSequence);
```
<i>Add custom check</i><br/>
Custom checks can be added in two ways:
1. Anonymous method
2. Create a class that inherits EzPasswordValidator.Checks.CustomCheck
```C#
validator.AddCheck(nameof(MyCustomCheck), MyCustomCheck);
//or
validator.AddCheck("MyCustomCheckTag", psw => psw.Length > 8);
```

<i>Add multiple checks</i>
```C#
 validator.AddCheck(CheckTypes.Advanced);
 //or
 validator.AddCheck(288); //Number sequence check & letter sequence check
```

##### Remove checks

```C#
validator.RemoveCheck(CheckTypes.Symbols);
validator.RemoveCheck(1); //1 represents the length check
validator.RemoveCheck("MyCustomCheckTag"); //Removes the check with the given tag
```

## Contribute
We welcome all contributions, please see the [contribution guidelines](.github/CONTRIBUTING.md).

## License

This project is licensed under the MIT License - see [LICENSE.md](LICENSE.md) for details.

