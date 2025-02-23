# AikenDoc
[![Test](https://github.com/S0Ale/AikenDocument/actions/workflows/test.yml/badge.svg)](https://github.com/S0Ale/AikenDocument/actions/workflows/test.yml) ![NuGet Version](https://img.shields.io/nuget/v/AikenDocument)

A library to navigate and edit text files written in the Aiken format.

## Installation

This library is available as a NuGet package. You can install it using the following command:

```bash
dotnet add package AikenDoc # .NET CLI
```
Or by adding the following line to your `.csproj` file:
```xml
<PackageReference Include="AikenDoc" Version="0.6.0" />
```
[More options](https://www.nuget.org/packages/AikenDocument/)

## Usage

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
doc.Questions[0].SetCorrectOption("B");

// Save the document to a new file path
doc.Save("path/to/new_file.txt");
