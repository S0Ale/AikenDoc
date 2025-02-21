# AikenDocument

A library to navigate and edit text files written in the Aiken format.

## Installation

This library will be available on NuGet.

## Usage

```csharp
using AikenDocument;

// Load a document
var doc = new AikenDocument();
doc.Load("path/to/file.txt");

// Print all questions with their answers
foreach (var question in doc.Questions){
    Console.WriteLine($"Domanda: {question.Text}");
    
    foreach (var answer in question.Answers){
        Console.WriteLine($"- {answer.Text} (Corretto: {answer.IsCorrect})");
    }
}

// Set the correct answer of the first question to B
doc.Questions[0].SetCorrectOption("B");

// Save the document to a new file path
doc.Save("path/to/new_file.txt");
