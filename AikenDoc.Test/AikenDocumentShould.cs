namespace AikenDoc.Test;

[TestFixture]
internal class AikenDocumentShould{
    private const string FileName = "SAVED.txt";

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
    public void Success_LoadFromFilePath(){
        var doc = new AikenDocument();
        doc.Load(TestService.GetAssetsPath("SingleQuestion.txt"));
        Assert.That(doc.QuestionCount, Is.EqualTo(1));
    }
    
    [Test]
    public void Success_LoadFromText(){
        var doc = new AikenDocument();
        doc.LoadText("What is the capital of France?\nA. Paris\nB. London\nC. Berlin\nD. Madrid\nANSWER: A");
        Assert.That(doc.QuestionCount, Is.EqualTo(1));
    }
    
    [Test]
    public void Success_LoadFromStream(){
        var doc = new AikenDocument();
        doc.LoadText("What is the capital of France?\nA. Paris\nB. London\nC. Berlin\nD. Madrid\nANSWER: A");
        var stream = new MemoryStream(); 
        doc.Save(stream);
        stream.Position = 0;
        
        var newDoc = new AikenDocument();
        newDoc.Load(stream);
        Assert.That(newDoc.QuestionCount, Is.EqualTo(1));
    }
    
    [Test]
    public void Success_MultipleQuestions(){
        var doc = new AikenDocument();
        doc.Load(TestService.GetAssetsPath("MultipleQuestions.txt"));
        Assert.That(doc.QuestionCount, Is.EqualTo(3));
    }

    [Test]
    public void Success_SaveDocumentToFile(){
        var doc = new AikenDocument();
        doc.Load(TestService.GetAssetsPath("SingleQuestion.txt"));
        var question = doc.Questions[0];
        question.Text = "This text has been changed";
        doc.Save($"{TestService.AssetsDir}/{FileName}");
        
        var savedDoc = new AikenDocument();
        savedDoc.Load(TestService.GetAssetsPath(FileName));
        TestService.RemoveAsset(FileName);
        Assert.That(savedDoc.Questions[0].Text, Is.EqualTo("This text has been changed"));
    }
    
    [Test]
    public void Success_SaveToStream(){
        var doc = new AikenDocument();
        doc.Load(TestService.GetAssetsPath("SingleQuestion.txt"));
        var question = doc.Questions[0];
        question.Text = "This text has been changed";
        var stream = new MemoryStream();
        doc.Save(stream);
        stream.Position = 0;
        
        var savedDoc = new AikenDocument();
        savedDoc.Load(stream);
        Assert.That(savedDoc.Questions[0].Text, Is.EqualTo("This text has been changed"));
    }
    
    [Test]
    public void Success_AppendQuestion(){
        var doc = new AikenDocument();
        doc.Load(TestService.GetAssetsPath("SingleQuestion.txt"));
        var question = new AikenQuestion("New question");
        doc.AppendQuestion(question);
        
        Assert.That(doc.QuestionCount, Is.EqualTo(2));
    }
    
    [Test]
    public void Success_CloneDocument(){
        var doc = new AikenDocument();
        doc.Load(TestService.GetAssetsPath("SingleQuestion.txt"));
        var clone = (AikenDocument)doc.Clone();
        
        Assert.That(clone.QuestionCount, Is.EqualTo(doc.QuestionCount));
    }
    
    [Test]
    public void Success_ChangeCorrectAnswer(){
        var doc = new AikenDocument();
        doc.Load(TestService.GetAssetsPath("SingleQuestion.txt"));
        doc.Questions[0].SetCorrectOption("B");
        
        Assert.That(doc.Questions[0].CorrectAnswer, Is.EqualTo("B"));
    }
}