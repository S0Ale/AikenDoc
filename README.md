# AikenDoc
[![Test](https://github.com/S0Ale/AikenDocument/actions/workflows/test.yml/badge.svg)](https://github.com/S0Ale/AikenDocument/actions/workflows/test.yml) ![NuGet Version](https://img.shields.io/nuget/v/AikenDoc)

A library to navigate and edit text files written in the Aiken format.

## Installation

This library is available as a NuGet package. You can install it using the following command:

```bash
dotnet add package AikenDoc # .NET CLI
```
Or by adding the following line to your `.csproj` file:
```xml
<PackageReference Include="AikenDoc" Version="x.y.z" />
```
[More options](https://www.nuget.org/packages/AikenDoc/)

---
## Usage

### Example

```csharp
using AikenDoc;

// Load a document
var doc = new AikenDocument();
doc.Load("path/to/file.txt");

// Print all questions with their answers
foreach (var question in doc.Questions){
    Console.WriteLine($"Question: {question.Text}");
    
    foreach (var answer in question.Answers){
        Console.WriteLine($"- {answer.Text} (Correct: {answer.IsCorrect})");
    }
}

// Set the correct answer of the first question to B
doc.Questions[0].CorrectAnswer = "B";

// Save the document to a new file path
doc.Save("path/to/new_file.txt");
```
---
### Loading documents

Load from a file path:
```csharp
var doc = new AikenDocument();
doc.Load("path/to/file.txt");
```

Load from a stream:
```csharp
var doc = new AikenDocument();
using (var stream = File.OpenRead("path/to/file.txt")){
    doc.Load(stream);
}
```

Load from a string:
```csharp
var doc = new AikenDocument();
doc.LoadText("Question 1\nA. Answer 1\nB. Answer 2\nC. Answer 3\nD. Answer 4\nANSWER: A\n\nQuestion 2\nA. Answer 1\nB. Answer 2\nC. Answer 3\nD. Answer 4\nANSWER: B");
```
---

### Saving documents

Save to a file path:
```csharp
doc.Save("path/to/file.txt");
```

Save to a stream:
```csharp
using (var stream = File.OpenWrite("path/to/file.txt")){
    doc.Save(stream);
}
```