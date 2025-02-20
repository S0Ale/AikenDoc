using System.Data.Common;

namespace AikenDocument.Test;

[TestFixture]
internal class AikenDocumentShould{
    private string _fileName = "SAVED.txt";
    
    [SetUp]
    public void Setup(){ }
    
    [Test]
    public void Throw_FileDoesNotExist(){
        var doc = new AikenDocument();
        Assert.Throws<FileNotFoundException>(() => doc.Load("InvalidPath"));
    }
    
    [Test]
    public void Throw_NoTxtFile(){
        var doc = new AikenDocument();
        Assert.Throws<FormatException>(() => doc.Load(TestService.GetAssetsPath("NoTxt.csv")));
    }
    
    [Test]
    public void Throw_NoAnswerFound(){
        var doc = new AikenDocument();
        Assert.Throws<FormatException>(() => doc.Load(TestService.GetAssetsPath("NoAnswer.txt")));
    }
    
    [Test]
    public void Throw_InvalidAnswer(){
        var doc = new AikenDocument();
        Assert.Throws<FormatException>(() => doc.Load(TestService.GetAssetsPath("InvalidAnswer.txt")));
    }
    
    [Test]
    public void Throw_MultipleAnswers(){
        var doc = new AikenDocument();
        Assert.Throws<FormatException>(() => doc.Load(TestService.GetAssetsPath("MultipleAnswers.txt")));
    }
    
    [Test]
    public void Throw_InvalidOption(){
        var doc = new AikenDocument();
        Assert.Throws<FormatException>(() => doc.Load(TestService.GetAssetsPath("InvalidOption.txt")));
    }
    
    [Test]
    public void Success_SingleQuestion(){
        var doc = new AikenDocument();
        doc.Load(TestService.GetAssetsPath("SingleQuestion.txt"));
        Assert.That(doc.QuestionCount, Is.EqualTo(1));
    }
    
    [Test]
    public void Success_MultipleQuestions(){
        var doc = new AikenDocument();
        doc.Load(TestService.GetAssetsPath("MultipleQuestions.txt"));
        Assert.That(doc.QuestionCount, Is.EqualTo(3));
    }

    [Test]
    public void Success_SaveDocument(){
        var doc = new AikenDocument();
        doc.Load(TestService.GetAssetsPath("SingleQuestion.txt"));
        var question = doc.Questions[0];
        question.Text = "This text has been changed";
        doc.Save($"{TestService.AssetsDir}/{_fileName}");
        
        var savedDoc = new AikenDocument();
        savedDoc.Load(TestService.GetAssetsPath(_fileName));
        TestService.RemoveAsset(_fileName);
        
        Assert.That(savedDoc.Questions[0].Text, Is.EqualTo("This text has been changed"));
    }
}