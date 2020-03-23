>  :warning: Deprecated  :warning:   
> This package is no longer maintained. Bugs will not be fixed. Use with caution.
# EzPasswordValidator      
[![NuGet version (EzPasswordValidator)](https://img.shields.io/nuget/v/EzPasswordValidator.svg)](https://www.nuget.org/packages/EzPasswordValidator/)
[![Downloads](https://img.shields.io/nuget/dt/EzPasswordValidator)](https://www.nuget.org/packages/EzPasswordValidator/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)    

A .NET standard library for easy password validation.
This library defines 11 predefined checks and an easy way to implement custom checks.     

## :scroll: Table of contents :scroll:
* [Predefined checks](#Checks)
* [Install](#Install)
* [Usage](#Usage)
* [How to contribute](#Contribute)
* [License info](#License)

## Checks
There are 11 predfined checks each representing a password criteria. Each check type is defined as a bit flag. A combination of checks can thus be simply refrenced using a single integer. All predefined check types are defined [here.](source/EzPasswordValidator/Checks/CheckTypes.cs)

#### Length check 
Checks if the given password is equal to or longer than the required minimum length
and equal to or shorter than the maximum allowed length.     
```
Default minimum length: 8     
Default maximum length: 128
```

Changing length bounds example:

```C#
validator.MinLength = 10;
validator.MaxLength = 256;

//OR

validator.SetLengthBounds(10, 256);
```

#### Check for numbers
Checks that the password contains at least one digit.

#### Check for letters
Checks that the password contains at least one letter. This check supports multiple alphabets. For more information about how we classify a letter see [this](https://docs.microsoft.com/en-us/dotnet/api/system.char.isletter?view=netframework-4.8#remarks) refrence.

#### Check for symbols
Checks that the password contains at least one symbol.

#### Case check
Checks that the password contains at least one upper- and lower-case letter. This check supports multiple alphabets. For more information about how we classify a letter see [this](https://docs.microsoft.com/en-us/dotnet/api/system.char.isletter?view=netframework-4.8#remarks) refrence.

#### Check for number sequences
Checks if the password contains a number series 3 or longer. Both increasing sequences and decreasing sequences are checked.    
```
Example number sequence: 123  |  765
```

#### Check for number repetition
Checks if the password contains number repetition 3 or longer in length.
```
Example number repetition: 444  |  222
```

#### Check for number location 
Checks that the password does not only have numbers in the front and/or end of the password. To pass this check the password must have a non-digit character before and after a digit character, only one digit must match this pattern.
```
Example invalid password: 2password   |  password2
Example valid   password: 2pass9word  |  p6ssword
```

#### Check for letter sequences
Checks if the password contains an alphabetical letter sequence consisting of four or more
characters. With the exception of the common three letter sequences: abc and xyz.     
Note: this check currently only supports ISO basic latin alphabet (A-Z a-z).
```
Example letter sequence: abc  |  xyz  |  bcde
```
        
#### Check for letter repetition
Checks if the password contains letter repetition 3 or longer in length.
This check supports multiple alphabets. For more information about how we classify a letter see [this](https://docs.microsoft.com/en-us/dotnet/api/system.char.isletter?view=netframework-4.8#remarks) refrence.    
Note: This check is not case sensitive meaning 'aAA' and 'aaa' will both match.
```
Example letter repetition: aAA  |  bbb
```

#### Check for symbol repetition
Checks for immediate symbol repetition 3 or longer in sequence.
```
Example symbol repetiton: ///  |  @@@
```

## Install
There are three main ways to install EzPasswordValidator:
- [NuGet](https://www.nuget.org/packages/EzPasswordValidator/) (Recommended)
- Download .dll from [releases](https://github.com/havardt/EzPasswordValidator/releases)
- Manually build .dll from source


## Usage

Example validator with predefined basic checks.
```C#
var validator = new PasswordValidator(CheckTypes.Basic);
```

#### Validate
```C#
bool isValid = validator.Validate(password);
```

<i>Failed validation</i> 
```C#
foreach (Check failedCheck in validator.FailedChecks)
{
    
}
```

#### Add checks

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

#### Remove checks

```C#
validator.RemoveCheck(CheckTypes.Symbols);
validator.RemoveCheck(1); //1 represents the length check
validator.RemoveCheck("MyCustomCheckTag"); //Removes the check with the given tag
```

## Contribute
We welcome all contributions, please see the [contribution guidelines](.github/CONTRIBUTING.md).

## License

This project is licensed under the MIT License - see [LICENSE.md](LICENSE.md) for details.

