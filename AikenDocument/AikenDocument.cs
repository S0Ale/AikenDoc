using System.Text.RegularExpressions;

namespace AikenDocument;

/// <summary>
/// Represents a document containing a list of Aiken questions
/// </summary>
public class AikenDocument{
    private const string AnswerPattern = @"^(?i)ANSWER:\s*[A-E]$";

    private List<AikenQuestion> Questions { get; } = [];
    
    public int QuestionCount => Questions.Count;

    /// <summary>
    /// Loads an Aiken file from the specified path
    /// </summary>
    public void Load(string filePath){
        if (!File.Exists(filePath))
            throw new FileNotFoundException("The specified file does not exist", filePath);

        var lines = File.ReadAllLines(filePath);
        ParseLines(lines);
    }

    /// <summary>
    /// Analyses the lines of the Aiken file and populates the list of questions
    /// </summary>
    private void ParseLines(string[] lines){
        AikenQuestion? currentQuestion = null;

        foreach (var line in lines){
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Check if the line is an option
            if (Regex.IsMatch(line, AikenOption.Pattern) && currentQuestion != null){
                var option = new AikenOption{
                    Text = line[3..].Trim(),
                    IsCorrect = false
                };
                currentQuestion.Options.Add(option);
            }
            // If the line is an answer, set the correct answer
            else if (Regex.IsMatch(line, AnswerPattern) && currentQuestion != null){
                if (!string.IsNullOrEmpty(currentQuestion.CorrectAnswer))
                    throw new FormatException($"No answer found for the question: \"{currentQuestion.Text}\"");
                
                // Get and set the correct answer
                currentQuestion.CorrectAnswer = line.Split(':')[1].Trim();
                currentQuestion.SetCorrectOption(currentQuestion.CorrectAnswer);
                
                // Check if the answer is valid
                var index = currentQuestion.CorrectAnswer[0] - 'A';
                if (index < 0 || index >= currentQuestion.Options.Count)
                    throw new FormatException($"Answer \"{currentQuestion.CorrectAnswer}\" does not match any valid option for the question: \"{currentQuestion.Text}\"");
            }else{
                // else create a new question
                currentQuestion = new AikenQuestion { Text = line.Trim() };
                Questions.Add(currentQuestion);
            }
        }
        
        // Check if all questions have a correct answer
        foreach (var question in Questions.Where(question => string.IsNullOrEmpty(question.CorrectAnswer))){
            throw new FormatException($"No answer found for the question: \"{question.Text}\"");
        }
    }
}