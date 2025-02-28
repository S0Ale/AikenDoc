using System.Text;
using System.Text.RegularExpressions;

namespace AikenDoc;

/// <summary>
/// Represents a document containing a list of Aiken questions.
/// </summary>
public class AikenDocument : ICloneable{
    /// <summary>
    /// The pattern that an Aiken answer must match.
    /// </summary>
    private const string AnswerPattern = @"^(?i)ANSWER:\s*[A-E]$";

    /// <summary>
    /// The list of questions in the document.
    /// </summary>
    public List<AikenQuestion> Questions { get; } = [];
    
    /// <summary>
    /// The number of questions in the document.
    /// </summary>
    public int QuestionCount => Questions.Count;

    /// <summary>
    /// Loads an Aiken file from the specified path.
    /// </summary>
    /// <param name="filePath"> The path to the file.</param>
    /// <exception cref="FileNotFoundException">Thrown when the file does not exist.</exception>
    /// <exception cref="FormatException">Thrown when the file is not a text file.</exception>
    public void Load(string filePath){
        if (!File.Exists(filePath))
            throw new FileNotFoundException("The specified file does not exist", filePath);
        if (Path.GetExtension(filePath) != ".txt")
            throw new FormatException("The specified file is not a text file");

        var lines = File.ReadAllLines(filePath);
        ParseLines(lines);
    }

    /// <summary>
    /// Loads an Aiken file from the specified stream.
    /// </summary>
    /// <param name="stream"> The stream containing the file.</param>
    public void Load(Stream stream){
        var reader = new StreamReader(stream, Encoding.UTF8);
        var lines = reader.ReadToEnd().Split('\n');
        ParseLines(lines);
    }
    
    /// <summary>
    /// Loads an Aiken file from the specified text.
    /// </summary>
    /// <param name="text"> The text of the file.</param>
    public void LoadText(string text){
        var lines = text.Split('\n');
        ParseLines(lines);
    }
    
    /// <summary>
    /// Analyses the lines of the Aiken file and populates the list of questions.
    /// </summary>
    /// <param name="lines">The lines of the document.</param>
    /// <exception cref="FormatException">Thrown when the file has no answer or the answer does not match any option.</exception>
    private void ParseLines(string[] lines){
        AikenQuestion? currentQuestion = null;

        foreach (var line in lines){
            var text = line.Replace("\r", "");
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Check if the line is an option
            if (Regex.IsMatch(text, AikenOption.Pattern) && currentQuestion != null){
                var option = new AikenOption(text[3..].Trim(), text[0].ToString());
                currentQuestion.Options.Add(option);
            }
            // If the line is an answer, set the correct answer
            else if (Regex.IsMatch(text, AnswerPattern) && currentQuestion != null){
                if (!string.IsNullOrEmpty(currentQuestion.CorrectAnswer))
                    throw new FormatException($"Question has multiple answers: \"{currentQuestion.Text}\"");
                
                // Get and set the correct answer
                currentQuestion.SetCorrectOption(text.Split(':')[1].Trim());
            }else{
                // else create a new question
                currentQuestion = new AikenQuestion(text.Trim());
                Questions.Add(currentQuestion);
            }
        }
        
        // Check if all questions have a correct answer
        foreach (var question in Questions.Where(question => string.IsNullOrEmpty(question.CorrectAnswer))){
            throw new FormatException($"No answer found for the question: \"{question.Text}\"");
        }
    }
    
    /// <summary>
    /// Appends a question to the document.
    /// </summary>
    /// <param name="question">The question to be appended.</param>
    public void AppendQuestion(AikenQuestion question){
        Questions.Add(question);
    }

    /// <summary>
    /// Returns a list of Aiken elements present in the document.
    /// </summary>
    /// <returns></returns>
    public List<AikenElement> GetElements(){
        List<AikenElement> elements = [];
        foreach (var question in Questions){
            elements.Add(question);
            elements.AddRange(question.Options);
        }

        return elements;
    }
    
    /// <summary>
    /// Saves the document to the specified file path.
    /// </summary>
    /// <param name="filePath">The location where the document will be saved.</param>
    public void Save(string filePath){
        using StreamWriter writer = new StreamWriter(filePath);
        foreach (var question in Questions)
        {
            // Question text
            writer.WriteLine(question.Text);

            // Options
            foreach (var option in question.Options)
                writer.WriteLine($"{option.Letter}) {option.Text}");

            // Correct answer
            writer.WriteLine($"ANSWER: {question.CorrectAnswer}");
            writer.WriteLine();
        }
    }
    
    /// <summary>
    /// Saves the document to the specified stream.
    /// </summary>
    /// <param name="stream"> The stream where the document will be saved.</param>
    public void Save(Stream stream){
        var writer = new StreamWriter(stream);
        foreach (var question in Questions){
            // Question text
            writer.WriteLine(question.Text);

            // Options
            foreach (var option in question.Options)
                writer.WriteLine($"{option.Letter}) {option.Text}");

            // Correct answer
            writer.WriteLine($"ANSWER: {question.CorrectAnswer}");
            writer.WriteLine();
        }
        writer.Flush();
    }

    /// <summary>
    /// Creates a new instance of the <see cref="AikenDocument"/> class.
    /// </summary>
    /// <returns>a new instance of <see cref="AikenDocument"/>.</returns>
    public object Clone(){
        var document = new AikenDocument();
        foreach (var question in Questions) document.Questions.Add((AikenQuestion)question.Clone());
        return document;
    }
}