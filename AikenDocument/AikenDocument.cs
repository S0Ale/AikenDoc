using System.Text.RegularExpressions;

namespace AikenDocument;

/// <summary>
/// Represents a document containing a list of Aiken questions.
/// </summary>
public class AikenDocument{
    /// <summary>
    /// The pattern that an Aiken answer must match.
    /// </summary>
    private const string AnswerPattern = @"^(?i)ANSWER:\s*[A-E]$";

    /// <summary>
    /// The list of questions in the document.
    /// </summary>
    private List<AikenQuestion> Questions { get; } = [];
    
    /// <summary>
    /// The number of questions in the document.
    /// </summary>
    public int QuestionCount => Questions.Count;

    /// <summary>
    /// Loads an Aiken file from the specified path.
    /// </summary>
    public void Load(string filePath){
        if (!File.Exists(filePath))
            throw new FileNotFoundException("The specified file does not exist", filePath);
        if (Path.GetExtension(filePath) != ".txt")
            throw new FormatException("The specified file is not a text file");

        var lines = File.ReadAllLines(filePath);
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
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Check if the line is an option
            if (Regex.IsMatch(line, AikenOption.Pattern) && currentQuestion != null){
                var option = new AikenOption(line[3..].Trim(), line[0].ToString());
                currentQuestion.Options.Add(option);
            }
            // If the line is an answer, set the correct answer
            else if (Regex.IsMatch(line, AnswerPattern) && currentQuestion != null){
                if (!string.IsNullOrEmpty(currentQuestion.CorrectAnswer))
                    throw new FormatException($"No answer found for the question: \"{currentQuestion.Text}\"");
                
                // Get and set the correct answer
                //currentQuestion.CorrectAnswer = line.Split(':')[1].Trim();
                currentQuestion.SetCorrectOption(line.Split(':')[1].Trim());
                
                // Check if the answer is valid
                //var index = currentQuestion.CorrectAnswer[0] - 'A';
                //if (index < 0 || index >= currentQuestion.Options.Count)
                    //throw new FormatException($"Answer \"{currentQuestion.CorrectAnswer}\" does not match any valid option for the question: \"{currentQuestion.Text}\"");
            }else{
                // else create a new question
                currentQuestion = new AikenQuestion(line.Trim());
                Questions.Add(currentQuestion);
            }
        }
        
        // Check if all questions have a correct answer
        foreach (var question in Questions.Where(question => string.IsNullOrEmpty(question.CorrectAnswer))){
            throw new FormatException($"No answer found for the question: \"{question.Text}\"");
        }
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
}