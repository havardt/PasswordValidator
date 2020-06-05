
# PasswordValidator      
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

> :point_up: :open_mouth: :mega: :exclamation:   
> It is important to note that these checks don't necessarily mean that your users will create more secure passwords. In some cases using too many checks may also harm security by removing some of the key space. One should also note that one can have very secure passwords even without passing many of these checks. For example, a password of length 32 comprised of random letters can be secure even though it does not pass basic checks such as a check for numbers and symbols. This is because of the strength in length. When using checks such as the letter and number sequence checks, then one should consider what percentage of the password is affected by this sequence. For example, finding a letter sequence of length 4 in a password of length 32 is not a big deal and one may not want to force the user to change the password, but in a password of length 6 it is a significant portion of the password.     
> <i>Summary:</i> Checks should be used to guide users in the right direction, but one must be aware that it is not always benefitial to enforce every check.

#### Length check (CheckTypes.Length)
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

#### Check for numbers (CheckTypes.Numbers)
Checks that the password contains at least one digit.

#### Check for letters (CheckTypes.Letters)
Checks that the password contains at least one letter. This check supports multiple alphabets. For more information about how we classify a letter see [this](https://docs.microsoft.com/en-us/dotnet/api/system.char.isletter?view=netframework-4.8#remarks) refrence.

#### Check for symbols (CheckTypes.Symbols)
Checks that the password contains at least one symbol.

#### Case check (CheckTypes.CaseUpperLower)
Checks that the password contains at least one upper- and lower-case letter. This check supports multiple alphabets. For more information about how we classify a letter see [this](https://docs.microsoft.com/en-us/dotnet/api/system.char.isletter?view=netframework-4.8#remarks) refrence.

#### Check for number sequences (CheckTypes.NumberSequence)
Checks if the password contains a number series/sequence equal to or longer than the set length. This length can be updated by setting the          ```EzPasswordValidator.Validators.PasswordValidator.NumberSequenceLength``` property (from v2.0.0). By default this has the following values: 

Default number sequence length (version < 2.0.0): 3     
Default number sequence length (version >= 2.0.0): 4    

Both increasing sequences and decreasing sequences are checked.

```
Example number sequence: 12345  or  987654321
```

#### Check for number repetition (CheckTypes.NumberRepetition)  
<b> This type has been replaced with digit repetition from v2.0.0</b>  

Checks if the password contains number repetition equal to or longer than 3 in a row.

```
Example number repetition: 444  or  222
```

#### Check for digit repetition (CheckTypes.DigitRepetition) - New in v2.0.0
Checks if the password contains digit repetition equal to or longer than the set length. This length can be updated by setting the          ```EzPasswordValidator.Validators.PasswordValidator.DigitRepetitionLength``` property. By default this has the following values: 
   
Default digit repetition length: 4    
```
Example digit repetition: 4444  or  2222
```

#### Check for number location (CheckTypes.NumberMixed)
Checks that the password does not only have numbers in the front and/or end of the password. To pass this check the password must have a non-digit character before and after a digit character, only one digit must match this pattern.
```
Example invalid password: 2password   |  password2
Example valid   password: 2pass9word  |  p6ssword
```

#### Check for letter sequences (CheckTypes.LetterSequence)
Checks if the password contains an alphabetical letter sequence consisting of a set amount of letters or more. This length can be updated by setting the  ```EzPasswordValidator.Validators.PasswordValidator.LetterSequenceLength``` property (from v2.0.0). By default this has the following values: 
   
Default letter sequence length: 4    

<b>Note:</b> this check currently only supports ISO basic latin alphabet (A-Z a-z).
```
Example letter sequence: abcd or bcde
```

For versions prior to v2.0.0 two three letter sequences where also checked for: ```abc``` and ```xyz```.
 
#### Check for letter repetition (CheckTypes.LetterRepetition)
Checks if the password contains letter repetition of a set length or longer. This length can be updated by setting the  ```EzPasswordValidator.Validators.PasswordValidator.LetterRepetitionLength``` property (from v2.0.0). Prior to v2.0.0 this check had hardcoded a repetition of 3 or more letters.

<b>Note:</b>    
- This check supports multiple alphabets. For more information about how we classify a letter see [this](https://docs.microsoft.com/en-us/dotnet/api/system.char.isletter?view=netframework-4.8#remarks) refrence.    
- This check is not case sensitive meaning 'aAA' and 'aaa' are both classified as letter repetition of length 3.   
```
Example letter repetition: aAAA  or  bbbb
```

#### Check for symbol repetition (CheckTypes.SymbolRepetition)
Checks if the password contains symbol repetition of a set length or longer. This length can be updated by setting the  ```EzPasswordValidator.Validators.PasswordValidator.SymbolRepetitionLength``` property (from v2.0.0). Prior to v2.0.0 this check had hardcoded a repetition of exactly 3 symbols.

For more information about how we classify a letter see [this](https://docs.microsoft.com/en-us/dotnet/api/system.char.issymbol?view=netframework-4.8#remarks) refrence.  
```
Example symbol repetiton of length 4: ////  or  @@@@
```

## Install
There are three main ways to install EzPasswordValidator:
- [NuGet](https://www.nuget.org/packages/EzPasswordValidator/) (Recommended)
- Download .dll from [releases](https://github.com/havardt/EzPasswordValidator/releases)
- Manually build .dll from source


## Usage

First create a validator. The constructor is overloaded and can take ```CheckTypes```.

```C#
var validator = new PasswordValidator(CheckTypes.Letters | CheckTypes.Numbers | CheckTypes.Length);
```

This example shows the creation of a validator that checks that a password contains letters, numbers and is within the set length bounds(default length bounds, since it is not explicitly set).

#### Validate
The ```Validate``` method runs through all the set checks and returns ```true``` if the password is valid according to the set criteria and ```false``` otherwise.

```C#
bool isValid = validator.Validate(password);
```

<i>Failed checks</i> 
One can iterate over the checks that failed by doing the following:
```C#
foreach (Check failedCheck in validator.FailedChecks)
{
    
}
```

<i>Passed checks</i>
One can iterate over the checks that passed by doing the following:
```C#
foreach (Check passedCheck in validator.PassedChecks)
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
Multiple checks can be added at once as the ```CheckTypes``` are bit flags. See [CheckTypes](source/EzPasswordValidator/Checks/CheckTypes.cs) for a reference.

Add multiple checks by using bitwise OR:
```C#
 validator.AddCheck(CheckTypes.NumberSequence | CheckTypes.LetterSequenceCheck);
```
This adds both the number sequence check and the letter sequence check.

Add multiple checks by using a integer value:
```C#
 validator.AddCheck(288);
```
Here the number sequence (binary: 100000) and letter sequence (binary: 100000000) checks are added as the combined binary value is ‭100100000‬ which is the same as 288 in base 10.

There are also two predefined combinations: basic and advanced.
Basic contains length check, numbers check, letters check, symbols check, and upper-lower-case check.
Advanced contains all checks. These can be added by doing either of the following:

```C#
 validator.AddCheck(CheckTypes.Basic);
 validator.AddCheck(CheckTypes.Advanced);
```
#### Remove checks

```C#
validator.RemoveCheck(CheckTypes.Symbols);
validator.RemoveCheck(1); // 1 represents the length check
validator.RemoveCheck("MyCustomCheckTag"); // Removes the custom check with the given tag
```

## Contribute
We welcome all contributions, please see the [contribution guidelines](.github/CONTRIBUTING.md).

## License

This project is licensed under the MIT License - see [LICENSE.md](LICENSE.md) for details.

