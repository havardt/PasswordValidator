# EzPasswordValidator
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)    
A .NET standard library for easy password validation.

This library defines 11 predefined checks and an easy way to implement custom checks.

## Usage
1. Download the .dll file. (See releases)
2. Add the .dll file into your project.
   In Visual Studio go to: <i>Solution explorer > Dependencies > Add reference > browse </i> and select the .dll file.<br/>
3. Code

Example validator with predefined basic checks.
```C#
var validator = new PasswordValidator(CheckTypes.Basic);
validator.RequiredLength = 10; //Minimum required password length
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
All predefined check types can be found at EzPasswordValidator.Checks.CheckTypes.cs

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

## License

This project is licensed under the MIT License - see [LICENSE.md](LICENSE.md) for details.

